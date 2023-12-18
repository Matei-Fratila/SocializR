import axios, { AxiosResponse } from 'axios';
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
    async login(username: string, password: string) {
        try {
            const axiosResponse: AxiosResponse = await axios.post('api/Auth/login', { username, password });
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
        localStorage.removeItem('user');
    }

    async register(username: string, email: string, password: string) {
        const response: AxiosResponse = await axios.post('api/Auth/login', { username, password, email });
    }

    getCurrentUser() {
        const userStr = localStorage.getItem('user');
        if (userStr) return JSON.parse(userStr);
        return null;
    }
}

export default new AuthService();