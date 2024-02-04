import axios, { AxiosResponse } from 'axios';
import { CurrentUser, LoginRequest, LoginResponse, RegisterRequest } from '../types/types';

class AuthService {
    async login(loginRequest: LoginRequest) {
        try {
            const axiosResponse: AxiosResponse = await axios.post('api/Auth/login', loginRequest);
            const response: LoginResponse = axiosResponse.data;

            if (response.currentUser) {
                const user = { ...response.currentUser, token: response.token };
                localStorage.setItem('user', JSON.stringify(user));
            }
        } catch (e) {
            console.error(e);
        }
    }

    logout() {
        localStorage.removeItem('user');
    }

    async register(registerRequest: RegisterRequest) {
        try {
            await axios.post('api/Auth/register', registerRequest);
        } catch (e) {
            console.error(e);
        }
    }

    getCurrentUser() {
        const userStr = localStorage.getItem('user');
        if (userStr) return JSON.parse(userStr) as CurrentUser;
        return null;
    }

    getCurrentUserPhoto() {
        const userStr = localStorage.getItem('user');
        if (userStr) {
            const user: CurrentUser = JSON.parse(userStr);
            return user.profilePhoto;
        }

        return "";
    }

    getCurrentUserId(): string | undefined {
        const userStr = localStorage.getItem('user');
        if (userStr) {
            const user: CurrentUser = JSON.parse(userStr);
            return user.id;
        }

        return;
    }
}

export default new AuthService();