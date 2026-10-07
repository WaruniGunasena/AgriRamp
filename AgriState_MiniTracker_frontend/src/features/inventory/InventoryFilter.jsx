export const InventoryFilter = ({ 
  locations, 
  categories, 
  selectedLocation, 
  selectedCategory, 
  searchQuery,
  onLocationChange, 
  onCategoryChange,
  onSearchChange,
  onOpenModal,
  onClearFilters
}) => {
  const filteredCategories = selectedLocation
    ? categories.filter(cat => cat.locationId === Number(selectedLocation))
    : categories;

  const hasActiveFilters = selectedLocation || selectedCategory || searchQuery;

  return (
    <div className="filter-card">
      <div className="filter-row">
        <div className="filter-group search-group">
          <label className="filter-label">Search Items</label>
          <div className="input-icon-wrapper">
            <span className="search-icon">🔍</span>
            <input
              type="text"
              className="filter-input"
              placeholder="Search by name, location, category..."
              value={searchQuery}
              onChange={(e) => onSearchChange(e.target.value)}
            />
          </div>
        </div>

        <div className="filter-group">
          <label className="filter-label">Location</label>
          <select 
            className="filter-select"
            value={selectedLocation} 
            onChange={(e) => {
              onLocationChange(e.target.value);
              onCategoryChange(''); 
            }}
          >
            <option value="">All Locations ({locations.length})</option>
            {locations.map(loc => (
              <option key={loc.id} value={loc.id}>{loc.name}</option>
            ))}
          </select>
        </div>

        <div className="filter-group">
          <label className="filter-label">Category</label>
          <select 
            className="filter-select"
            value={selectedCategory} 
            onChange={(e) => onCategoryChange(e.target.value)}
            disabled={!selectedLocation && filteredCategories.length === 0}
          >
            <option value="">All Categories</option>
            {filteredCategories.map(cat => (
              <option key={cat.id} value={cat.id}>{cat.name}</option>
            ))}
          </select>
        </div>

        <div className="filter-actions">
          {hasActiveFilters && (
            <button className="btn btn-ghost" onClick={onClearFilters}>
              Reset Filters
            </button>
          )}
          <button className="btn btn-primary" onClick={onOpenModal}>
            <span>+</span> Add New Item
          </button>
        </div>
      </div>
    </div>
  );
};