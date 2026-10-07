import React, { useState } from 'react';
import { useAuth } from '../../context/AuthContext';

export function AuthModal({ isOpen, onClose, hasAdmin = true }) {
    const { login, register, authError, clearError } = useAuth();
    const [tab, setTab] = useState('login'); 
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [role, setRole] = useState('User');
    const [showPassword, setShowPassword] = useState(false);
    const [isSubmitting, setIsSubmitting] = useState(false);

    if (!isOpen) return null;

    const handleTabChange = (newTab) => {
        setTab(newTab);
        clearError();
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setIsSubmitting(true);
        try {
            if (tab === 'login') {
                await login(username, password);
            } else {
                const userRole = hasAdmin ? 'User' : role;
                await register(username, email, password, userRole);
            }
            onClose();
            setUsername('');
            setEmail('');
            setPassword('');
        } catch (err) {
            console.error('Auth submit error:', err);
        } finally {
            setIsSubmitting(false);
        }
    };

    return (
        <div className="modal-backdrop" onClick={onClose}>
            <div 
                className="modal-card auth-modal-card" 
                onClick={(e) => e.stopPropagation()}
                style={{ maxWidth: '440px' }}
            >
                <div className="auth-header">
                    <div className="auth-brand">
                        <span className="auth-icon">🔐</span>
                        <h3>AgriState Authentication</h3>
                    </div>
                    <button className="modal-close-btn" onClick={onClose}>×</button>
                </div>

                <div className="auth-tabs">
                    <button 
                        className={`auth-tab-btn ${tab === 'login' ? 'active' : ''}`}
                        onClick={() => handleTabChange('login')}
                    >
                        Sign In
                    </button>
                    <button 
                        className={`auth-tab-btn ${tab === 'register' ? 'active' : ''}`}
                        onClick={() => handleTabChange('register')}
                    >
                        Create Account
                    </button>
                </div>
                {authError && (
                    <div className="auth-error-alert">
                        ⚠️ {authError}
                    </div>
                )}
                <form onSubmit={handleSubmit} className="auth-form">
                    <div className="form-group">
                        <label className="form-label">Username</label>
                        <input
                            type="text"
                            className="form-input"
                            placeholder="Enter username"
                            value={username}
                            onChange={(e) => setUsername(e.target.value)}
                            required
                            autoFocus
                        />
                    </div>

                    {tab === 'register' && (
                        <>
                            <div className="form-group">
                                <label className="form-label">Email Address</label>
                                <input
                                    type="email"
                                    className="form-input"
                                    placeholder="e.g. user@agristate.com"
                                    value={email}
                                    onChange={(e) => setEmail(e.target.value)}
                                />
                            </div>

                            {!hasAdmin && (
                                <div className="form-group">
                                    <label className="form-label">User Role</label>
                                    <select 
                                        className="form-input"
                                        value={role} 
                                        onChange={(e) => setRole(e.target.value)}
                                    >
                                        <option value="User">Standard User</option>
                                        <option value="Admin">Estate Admin</option>
                                    </select>
                                </div>
                            )}
                        </>
                    )}

                    <div className="form-group">
                        <label className="form-label">Password</label>
                        <div style={{ position: 'relative' }}>
                            <input
                                type={showPassword ? 'text' : 'password'}
                                className="form-input"
                                placeholder="Enter password"
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                required
                            />
                            <button
                                type="button"
                                style={{
                                    position: 'absolute',
                                    right: '12px',
                                    top: '50%',
                                    transform: 'translateY(-50%)',
                                    background: 'none',
                                    border: 'none',
                                    cursor: 'pointer',
                                    fontSize: '14px'
                                }}
                                onClick={() => setShowPassword(!showPassword)}
                            >
                                {showPassword ? '👁️' : '🙈'}
                            </button>
                        </div>
                    </div>
                    <button 
                        type="submit" 
                        className="btn btn-primary btn-block"
                        disabled={isSubmitting}
                        style={{ marginTop: '16px', padding: '12px', fontSize: '15px' }}
                    >
                        {isSubmitting ? 'Authenticating...' : (tab === 'login' ? 'Sign In' : 'Create Account')}
                    </button>
                </form>
            </div>
        </div>
    );
}
