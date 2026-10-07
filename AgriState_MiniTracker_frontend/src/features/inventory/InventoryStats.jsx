import React from 'react';

export const InventoryStats = ({ stats }) => {
  if (!stats) return null;

  return (
    <div className="stats-grid">
      <div className="stat-card">
        <div className="stat-header">
          <span className="stat-icon icon-emerald">📦</span>
          <span className="stat-title">Total Items</span>
        </div>
        <div className="stat-value">{stats.totalItems || 0}</div>
        <div className="stat-subtitle">Tracked SKU categories</div>
      </div>

      <div className={`stat-card ${stats.lowStockCount > 0 ? 'warning-border' : ''}`}>
        <div className="stat-header">
          <span className="stat-icon icon-amber">⚠️</span>
          <span className="stat-title">Low Stock Alerts</span>
        </div>
        <div className="stat-value warning-text">{stats.lowStockCount || 0}</div>
        <div className="stat-subtitle">Requires replenishment</div>
      </div>

      <div className="stat-card">
        <div className="stat-header">
          <span className="stat-icon icon-blue">📊</span>
          <span className="stat-title">Total Quantity</span>
        </div>
        <div className="stat-value">{stats.totalQuantity || 0}</div>
        <div className="stat-subtitle">Combined stock units</div>
      </div>

      <div className="stat-card">
        <div className="stat-header">
          <span className="stat-icon icon-purple">🏡</span>
          <span className="stat-title">Active Locations</span>
        </div>
        <div className="stat-value">{stats.locationsCount || 0}</div>
        <div className="stat-subtitle">Barns & Greenhouses</div>
      </div>
    </div>
  );
};
