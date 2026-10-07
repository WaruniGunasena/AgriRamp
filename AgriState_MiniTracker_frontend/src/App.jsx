import React, { useEffect, useState } from 'react';
import { useLoading } from './context/LoadingContext';
import { useAuth } from './context/AuthContext';
import {
    fetchInventoryItems,
    deleteInventoryItem,
    addInventoryItem,
    fetchInventoryStats,
    fetchLocations,
    fetchCategories
} from './api/inventoryApi';
import { InventoryFilter } from './features/inventory/InventoryFilter';
import { InventoryList } from './features/inventory/InventoryList';
import { InventoryStats } from './features/inventory/InventoryStats';
import { InventoryFormModel } from './features/inventory/InventoryFormModel';
import { AuthScreen } from './features/auth/AuthScreen';

export default function App() {
    const [items, setItems] = useState([]);
    const [stats, setStats] = useState(null);
    const [locations, setLocations] = useState([]);
    const [categories, setCategories] = useState([]);
    const [selectedLocation, setSelectedLocation] = useState('');
    const [selectedCategory, setSelectedCategory] = useState('');
    const [searchQuery, setSearchQuery] = useState('');
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [apiError, setApiError] = useState(null);

    const { startLoading, stopLoading } = useLoading();
    const { user, isAuthenticated, isLoading: isAuthLoading, logout } = useAuth();

    // Load static dropdown metadata (Locations & Categories) from API
    const loadMetadata = async () => {
        try {
            const [locs, cats] = await Promise.all([
                fetchLocations(),
                fetchCategories()
            ]);
            setLocations(locs);
            setCategories(cats);
        } catch (err) {
            console.error('Failed to load locations/categories metadata:', err);
            setApiError('Unable to connect to .NET Backend API. Please ensure the backend server is running.');
        }
    };

    // Load inventory table items and dashboard stats
    const loadData = async () => {
        if (!isAuthenticated) return;
        startLoading('Fetching Estate Inventory...');
        setApiError(null);
        try {
            const [fetchedItems, fetchedStats] = await Promise.all([
                fetchInventoryItems(selectedLocation, selectedCategory, searchQuery),
                fetchInventoryStats()
            ]);
            setItems(fetchedItems);
            setStats(fetchedStats);
        } catch (err) {
            console.error('Failed to load inventory data:', err);
            setApiError(err.message || 'Error connecting to backend API');
        } finally {
            stopLoading();
        }
    };

    useEffect(() => {
        if (isAuthenticated) {
            loadMetadata();
        }
    }, [isAuthenticated]);

    useEffect(() => {
        if (isAuthenticated) {
            loadData();
        }
    }, [isAuthenticated, selectedLocation, selectedCategory, searchQuery]);

    const handleOpenAddModal = () => {
        setIsModalOpen(true);
    };

    const handleAddItem = async (newItemData) => {
        startLoading('Adding new inventory item...');
        try {
            await addInventoryItem(newItemData);
            await loadData();
        } catch (err) {
            console.error('Failed to add item:', err);
            alert(`Failed to add item: ${err.message}`);
        } finally {
            stopLoading();
        }
    };

    const handleDelete = async (id) => {
        if (!window.confirm('Are you sure you want to delete this inventory item?')) return;
        startLoading('Deleting item...');
        try {
            await deleteInventoryItem(id);
            await loadData();
        } catch (err) {
            console.error('Failed to delete item:', err);
            alert(`Failed to delete item: ${err.message}`);
        } finally {
            stopLoading();
        }
    };

    const handleClearFilters = () => {
        setSelectedLocation('');
        setSelectedCategory('');
        setSearchQuery('');
    };

    if (isAuthLoading) {
        return (
            <div className="auth-screen-container">
                <div className="loading-card">
                    <div className="loading-spinner"></div>
                    <div className="loading-text">Initializing AgriState Mini-Tracker...</div>
                </div>
            </div>
        );
    }

    // Render Initial Login / Register screen if user is not authenticated
    if (!isAuthenticated) {
        return <AuthScreen />;
    }

    // Render Main Estate Inventory Dashboard when authenticated
    return (
        <div className="app-container">
            {/* Header */}
            <header className="app-header">
                <div className="header-brand">
                    <div className="brand-logo">🌾</div>
                    <div>
                        <h1 className="brand-title">AgriState Mini-Tracker</h1>
                        <p className="brand-subtitle">Real-time estate inventory management & cascading stock controls</p>
                    </div>
                </div>

                <div className="header-right">
                    <div className="user-profile-badge">
                        <span className="user-avatar">
                            {user?.username ? user.username[0].toUpperCase() : 'U'}
                        </span>
                        <span style={{ fontWeight: 600 }}>{user?.username}</span>
                        <span className={`role-tag role-${user?.role?.toLowerCase() || 'user'}`}>
                            {user?.role || 'User'}
                        </span>
                        <button
                            className="btn btn-secondary"
                            style={{ padding: '4px 10px', fontSize: '12px', marginLeft: '6px' }}
                            onClick={logout}
                            title="Sign Out"
                        >
                            🚪 Logout
                        </button>
                    </div>
                </div>
            </header>
            {apiError && (
                <div style={{
                    background: '#fef2f2',
                    border: '1px solid #fca5a5',
                    borderRadius: '8px',
                    padding: '12px 16px',
                    margin: '16px auto 0',
                    maxWidth: '1200px',
                    color: '#991b1b',
                    display: 'flex',
                    alignItems: 'center',
                    justifyContent: 'space-between',
                    fontWeight: 500
                }}>
                    <span>⚠️ {apiError}</span>
                    <button 
                        className="btn btn-secondary" 
                        style={{ padding: '4px 12px', fontSize: '13px' }}
                        onClick={() => { loadMetadata(); loadData(); }}
                    >
                        🔄 Retry Connection
                    </button>
                </div>
            )}

            <main className="main-content">
                <InventoryStats stats={stats} />

                {/* Cascading Filter & Controls */}
                <InventoryFilter
                    locations={locations}
                    categories={categories}
                    selectedLocation={selectedLocation}
                    selectedCategory={selectedCategory}
                    searchQuery={searchQuery}
                    onLocationChange={setSelectedLocation}
                    onCategoryChange={setSelectedCategory}
                    onSearchChange={setSearchQuery}
                    onOpenModal={handleOpenAddModal}
                    onClearFilters={handleClearFilters}
                />

                {/* Inventory List Table */}
                <InventoryList
                    items={items}
                    onDelete={handleDelete}
                    onResetFilters={handleClearFilters}
                />
            </main>

            {/* Add Item Modal Form */}
            <InventoryFormModel
                isOpen={isModalOpen}
                onClose={() => setIsModalOpen(false)}
                onSubmit={handleAddItem}
                locations={locations}
                categories={categories}
            />
        </div>
    );
}