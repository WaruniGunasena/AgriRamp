import axios from 'axios';

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'https://localhost:7086/api';

const axiosClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
    'Accept': 'application/json',
  },
  timeout: 10000, 
});

axiosClient.interceptors.request.use(
  (config) => {
    console.log(`[API ${config.method?.toUpperCase()}] -> ${config.baseURL}${config.url}`, config.params || '');
    const token = localStorage.getItem('auth_token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
  },
  (error) => {
    console.error('[API Request Error]', error);
    return Promise.reject(error);
  }
);

axiosClient.interceptors.response.use(
  (response) => {
    console.log(`[API Response ${response.status}] <- ${response.config.url}`);
    return response.data;
  },
  (error) => {
    if (error.response) {
      console.error(`[API Error ${error.response.status}]`, error.response.data);
      const serverMsg = error.response.data?.title || error.response.data?.message || `HTTP ${error.response.status} Error`;
      return Promise.reject(new Error(serverMsg));
    } else if (error.request) {
      console.error('[Network Error] Backend .NET API is unreachable.');
      return Promise.reject(new Error('Backend server is offline or unreachable'));
    } else {
      console.error('[Client Error]', error.message);
      return Promise.reject(error);
    }
  }
);

export default axiosClient;
