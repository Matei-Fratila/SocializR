import { AxiosResponse } from 'axios';
import axiosInstance from '../helpers/axios-helper';
import { CurrentUser, LoginRequest, LoginResponse, RegisterRequest } from '../types/types';

class AuthService {
    async login(loginRequest: LoginRequest) {
        const axiosResponse: AxiosResponse = await axiosInstance.post('/Auth/login', loginRequest);
        const response: LoginResponse = axiosResponse.data;

        if (response.currentUser) {
            const user = { ...response.currentUser, token: response.token };
            localStorage.setItem('user', JSON.stringify(user));
        }
    }

    logout() {
        localStorage.removeItem('user');
    }

    async register(registerRequest: RegisterRequest) {
        try {
            await axiosInstance.post('/Auth/register', registerRequest);
        } catch (e) {
            console.error(e);
        }
    }

    async getRefreshToken() {
        try {
            const resp = await axiosInstance.post("auth/refresh", {accessToken: this.getAccessToken()}, { headers: {'Content-Type': 'application/json' } });
            return resp.data;
        } catch (e) {
            console.log("Error", e);
        }
    };

    saveToken(accessToken: string){
        const userStr = localStorage.getItem('user');
        if (userStr) {
            const user: CurrentUser = JSON.parse(userStr);
            localStorage.setItem('user', JSON.stringify({ ...user, token: accessToken }));
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

    updateCurrentUserPhoto(photo: string) {
        const userStr = localStorage.getItem('user');
        if (userStr) {
            const user: CurrentUser = JSON.parse(userStr);
            localStorage.setItem('user', JSON.stringify({ ...user, profilePhoto: photo }));
        }
    }

    getCurrentUserId(): string | undefined {
        const userStr = localStorage.getItem('user');
        if (userStr) {
            const user: CurrentUser = JSON.parse(userStr);
            return user.id;
        }

        return;
    }

    getAccessToken() {
        const userStr = localStorage.getItem('user');
        let user = null;
        if (userStr)
            user = JSON.parse(userStr);

        return user?.token;
    }
}

export default new AuthService();