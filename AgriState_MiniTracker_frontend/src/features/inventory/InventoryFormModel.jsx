import React, { useState } from 'react';

export const InventoryFormModel = ({ isOpen, onClose, onSubmit, locations, categories }) => {
  const [formData, setFormData] = useState({
    name: '',
    locationId: '',
    categoryId: '',
    quantity: '',
    unit: 'Units',
    minThreshold: '10'
  });

  if (!isOpen) return null;

  const availableCategories = formData.locationId
    ? categories.filter(cat => cat.locationId === Number(formData.locationId))
    : [];

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => {
      const nextState = { ...prev, [name]: value };
      if (name === 'locationId') {
        nextState.categoryId = '';
      }
      return nextState;
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!formData.name || !formData.locationId || !formData.categoryId || !formData.quantity) {
      alert('Please fill in all required fields.');
      return;
    }

    const selectedLoc = locations.find(l => l.id === Number(formData.locationId));
    const selectedCat = categories.find(c => c.id === Number(formData.categoryId));

    onSubmit({
      ...formData,
      locationName: selectedLoc ? selectedLoc.name : '',
      categoryName: selectedCat ? selectedCat.name : ''
    });

    setFormData({
      name: '',
      locationId: '',
      categoryId: '',
      quantity: '',
      unit: 'Units',
      minThreshold: '10'
    });
    onClose();
  };

  return (
    <div className="modal-backdrop" onClick={onClose}>
      <div className="modal-content" onClick={(e) => e.stopPropagation()}>
        <div className="modal-header">
          <h2>➕ Add New Inventory Item</h2>
          <button className="modal-close" onClick={onClose}>&times;</button>
        </div>

        <form onSubmit={handleSubmit} className="modal-form">
          <div className="form-group">
            <label>Item Name *</label>
            <input 
              type="text"
              name="name"
              placeholder="e.g. Organic Compost, NPK Fertilizer"
              value={formData.name}
              onChange={handleChange}
              required
            />
          </div>

          <div className="form-row">
            <div className="form-group">
              <label>Location *</label>
              <select 
                name="locationId" 
                value={formData.locationId}
                onChange={handleChange}
                required
              >
                <option value="">Select Location</option>
                {locations.map(loc => (
                  <option key={loc.id} value={loc.id}>{loc.name}</option>
                ))}
              </select>
            </div>

            <div className="form-group">
              <label>Category *</label>
              <select 
                name="categoryId" 
                value={formData.categoryId}
                onChange={handleChange}
                disabled={!formData.locationId}
                required
              >
                <option value="">{formData.locationId ? 'Select Category' : 'Select Location First'}</option>
                {availableCategories.map(cat => (
                  <option key={cat.id} value={cat.id}>{cat.name}</option>
                ))}
              </select>
            </div>
          </div>

          <div className="form-row">
            <div className="form-group">
              <label>Quantity *</label>
              <input 
                type="number"
                name="quantity"
                min="0"
                placeholder="0"
                value={formData.quantity}
                onChange={handleChange}
                required
              />
            </div>

            <div className="form-group">
              <label>Unit</label>
              <select name="unit" value={formData.unit} onChange={handleChange}>
                <option value="Units">Units</option>
                <option value="Bags">Bags</option>
                <option value="Tons">Tons</option>
                <option value="Packets">Packets</option>
                <option value="Liters">Liters</option>
                <option value="Bottles">Bottles</option>
                <option value="Pcs">Pcs</option>
              </select>
            </div>

            <div className="form-group">
              <label>Min Threshold</label>
              <input 
                type="number"
                name="minThreshold"
                min="1"
                value={formData.minThreshold}
                onChange={handleChange}
              />
            </div>
          </div>

          <div className="modal-actions">
            <button type="button" className="btn btn-secondary" onClick={onClose}>
              Cancel
            </button>
            <button type="submit" className="btn btn-primary">
              Save Inventory Item
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};
