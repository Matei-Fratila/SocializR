import axios, { AxiosResponse } from 'axios';
import authHeader from './auth-header';
import { User } from '../types/types';

class FriendshipService {
    async getAllFriends(id: string): Promise<User[]> {
        const axiosResponse: AxiosResponse = await axios.get(`/friendship/${id}`, { headers: authHeader() });
        return axiosResponse.data as User[];
    }

    async addFriend(id: string) {
        await axios.post(`/friendship/${id}`, {}, { headers: authHeader() });
    }

    async deleteFriend(id: string) {
        await axios.delete(`/friendship/${id}`, { headers: authHeader() });
    }

    async createFriendRequest(id: string) {
        await axios.post(`/friendrequest/${id}`, {}, { headers: authHeader() });
    }

    async deleteFriendRequest(id: string) {
        await axios.delete(`/friendrequest/${id}`, { headers: authHeader() });
    }
}

export default new FriendshipService();