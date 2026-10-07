import axiosClient from './axiosClient';

export const fetchInventoryItems = async (locationId = '', categoryId = '', searchQuery = '') => {
  const params = {};
  if (locationId) params.locationId = locationId;
  if (categoryId) params.categoryId = categoryId;
  if (searchQuery) params.searchQuery = searchQuery;

  return await axiosClient.get('/inventory', { params });
};

export const fetchInventoryStats = async () => {
  return await axiosClient.get('/inventory/stats');
};

export const addInventoryItem = async (itemData) => {
  const payload = {
    name: itemData.name,
    locationId: Number(itemData.locationId),
    categoryId: Number(itemData.categoryId),
    quantity: Number(itemData.quantity),
    unit: itemData.unit || 'Units',
    minThreshold: Number(itemData.minThreshold) || 10
  };
  return await axiosClient.post('/inventory', payload);
};

export const deleteInventoryItem = async (id) => {
  return await axiosClient.delete(`/inventory/${id}`);
};

export const fetchLocations = async () => {
  return await axiosClient.get('/inventory/locations');
};

export const fetchCategories = async (locationId = '') => {
  const params = locationId ? { locationId } : {};
  return await axiosClient.get('/inventory/categories', { params });
};
