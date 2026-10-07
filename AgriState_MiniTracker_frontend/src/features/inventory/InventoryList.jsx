export const InventoryList = ({ items, onDelete, onResetFilters }) => {
  if (items.length === 0) {
    return (
      <div className="empty-state">
        <div className="empty-icon">🌱</div>
        <h3>No Inventory Items Found</h3>
        <p>There are no items matching your current filters or search criteria.</p>
        {onResetFilters && (
          <button className="btn btn-secondary" onClick={onResetFilters}>
            Clear All Filters
          </button>
        )}
      </div>
    );
  }

  return (
    <div className="table-wrapper">
      <table className="inventory-table">
        <thead>
          <tr>
            <th>Item Name</th>
            <th>Location</th>
            <th>Category</th>
            <th>Quantity</th>
            <th>Unit</th>
            <th>Status</th>
            <th style={{ textAlign: 'right' }}>Actions</th>
          </tr>
        </thead>
        <tbody>
          {items.map((item) => {
            const isLowStock = item.status === 'Low Stock' || item.quantity <= (item.minThreshold || 10);
            return (
              <tr key={item.id} className={isLowStock ? 'row-warning' : ''}>
                <td className="font-semibold text-main">
                  {item.name}
                </td>
                <td>
                  <span className="location-tag">{item.locationName}</span>
                </td>
                <td>
                  <span className="category-tag">{item.categoryName}</span>
                </td>
                <td>
                  <span className="quantity-value">{item.quantity}</span>
                </td>
                <td>
                  <span className="unit-label"> {item.unit}</span>
                </td>
                <td>
                  <span className={`badge ${isLowStock ? 'badge-danger' : 'badge-success'}`}>
                    {isLowStock ? 'Low Stock' : 'In Stock'}
                  </span>
                </td>
                <td style={{ textAlign: 'right' }}> 
                  <button 
                    className="btn-danger-outline" 
                    onClick={() => onDelete(item.id)}
                    title="Delete item"
                  >
                    🗑️ Delete
                  </button>
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
};