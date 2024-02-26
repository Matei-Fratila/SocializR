import axios, { AxiosError, AxiosResponse } from 'axios';
import { CurrentUser, LoginRequest, LoginResponse, RegisterRequest } from '../types/types';

class AuthService {
    async login(loginRequest: LoginRequest) {
        const axiosResponse: AxiosResponse = await axios.post('/Auth/login', loginRequest);
        const response: LoginResponse = axiosResponse.data;

        if (response.currentUser) {
            const user = { ...response.currentUser, token: response.token };
            localStorage.setItem('user', JSON.stringify(user));
        }
    }

    async googleLogin() {
        try {
            const axiosResponse: AxiosResponse = await axios.post('/Auth/loginOrRegister', {}, {
                headers: {
                    "Authorization": `Bearer ${this.getAccessToken()}`
                }
            });
            
            const id = axiosResponse.data;
            if (id !== undefined) {
                const userStr = localStorage.getItem('user');
                if (userStr) {
                    const user: CurrentUser = JSON.parse(userStr);
                    localStorage.setItem('user', JSON.stringify({ ...user, id: id }));
                }
            }
        } catch (error) {
            console.error(error);
        }
    }

    logout() {
        localStorage.removeItem('user');
    }

    async register(registerRequest: RegisterRequest) {
        try {
            await axios.post('/Auth/register', registerRequest);
        } catch (e) {
            console.error(e);
        }
    }

    async getRefreshToken() {
        try {
            const resp = await axios.post("auth/refresh", { accessToken: this.getAccessToken() }, { headers: { 'Content-Type': 'application/json' } });
            return resp.data;
        } catch (e) {
            console.log("Error", e);
        }
    };

    saveUserAndToken(user: CurrentUser, token: string) {
        const userAndToken = { ...user, token: token };
        localStorage.setItem('user', JSON.stringify(userAndToken));
    }

    saveToken(accessToken: string) {
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