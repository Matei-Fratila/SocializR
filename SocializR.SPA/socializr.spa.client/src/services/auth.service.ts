import axios, { AxiosResponse } from 'axios';
import { LoginRequest, RegisterRequest } from '../types/types';

interface LoginResponse {
    token: string,
    currentUser: CurrentUser
}

interface CurrentUser {
    id: string,
    firstName: string,
    lastName: string,
    profilePhoto: string,
    roles: [Role]
}

interface Role {
    name: string,
    description: string
}

class AuthService {
    async login(loginRequest: LoginRequest) {
        try {
            const axiosResponse: AxiosResponse = await axios.post('api/Auth/login', loginRequest);
            const response: LoginResponse = axiosResponse.data;

            if (response.token) {
                localStorage.setItem('accessToken', JSON.stringify(response.token));
                const currentUser = response.currentUser;
                if (currentUser) {
                    localStorage.setItem('currentUser', JSON.stringify(currentUser));
                }
            }
        } catch (e) {
            console.error(e);
        }
    }

    logout() {
        localStorage.removeItem('accessToken');
        localStorage.removeItem('currentUser');
    }

    async register(registerRequest: RegisterRequest) {
        const response: AxiosResponse = await axios.post('api/Auth/register', registerRequest);
    }

    getCurrentUser() {
        const userStr = localStorage.getItem('currentUser');
        if (userStr) return JSON.parse(userStr) as CurrentUser;
        return null;
    }
}

export default new AuthService();