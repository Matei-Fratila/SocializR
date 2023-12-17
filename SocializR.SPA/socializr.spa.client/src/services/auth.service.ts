import axios, { AxiosResponse } from 'axios';

class AuthService{
    async login(username: string, password: string){
        try{
            const response: AxiosResponse = await axios.post('api/Auth/login', {username, password});
            const accessToken: string = response.data;
            if(accessToken){
                localStorage.setItem('user', JSON.stringify(accessToken));
            }
            return response.status;
        } catch (e) {
            console.error(e);
        }
    }

    logout(){
        localStorage.removeItem('user');
    }

    async register(username: string, email: string, password: string){
        const response: AxiosResponse = await axios.post('api/Auth/login', {username, password, email});
    }

    getCurrentUser(){
        const userStr = localStorage.getItem('user');
        if(userStr) return JSON.parse(userStr);
        return null;
    }
}

export default new AuthService();