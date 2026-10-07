import React, { createContext, useContext, useState, useEffect } from 'react';
import { loginUser, registerUser, getCurrentUser } from '../api/authApi';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const [token, setToken] = useState(() => localStorage.getItem('auth_token') || null);
    const [isLoading, setIsLoading] = useState(true);
    const [authError, setAuthError] = useState(null);

    useEffect(() => {
        const verifyToken = async () => {
            const storedToken = localStorage.getItem('auth_token');
            if (storedToken) {
                try {
                    const userData = await getCurrentUser();
                    console.log('User data:', userData);
                    setUser(userData);
                    setToken(storedToken);
                } catch (err) {
                    console.warn('Invalid or expired token, logging out:', err);
                    logout();
                }
            }
            setIsLoading(false);
        };

        verifyToken();
    }, []);

    const login = async (username, password) => {
        setAuthError(null);
        try {
            const res = await loginUser({ username, password });
            const { token: jwtToken, ...userData } = res;
            localStorage.setItem('auth_token', jwtToken);
            setToken(jwtToken);
            setUser(userData);
            return userData;
        } catch (err) {
            const message = err.message || 'Login failed. Please check your credentials.';
            setAuthError(message);
            throw new Error(message);
        }
    };

    const register = async (username, email, password, role = 'User') => {
        setAuthError(null);
        try {
            const res = await registerUser({ username, email, password, role });
            const { token: jwtToken, ...userData } = res;
            localStorage.setItem('auth_token', jwtToken);
            setToken(jwtToken);
            setUser(userData);
            return userData;
        } catch (err) {
            const message = err.message || 'Registration failed. Please try again.';
            setAuthError(message);
            throw new Error(message);
        }
    };

    const logout = () => {
        localStorage.removeItem('auth_token');
        setToken(null);
        setUser(null);
        setAuthError(null);
    };

    return (
        <AuthContext.Provider
            value={{
                user,
                token,
                isAuthenticated: !!user,
                isLoading,
                authError,
                login,
                register,
                logout,
                clearError: () => setAuthError(null)
            }}
        >
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => {
    const context = useContext(AuthContext);
    if (!context) {
        throw new Error('useAuth must be used within an AuthProvider');
    }
    return context;
};
