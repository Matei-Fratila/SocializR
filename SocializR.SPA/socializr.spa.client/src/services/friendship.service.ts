import { AxiosResponse } from 'axios';
import axiosInstance from '../helpers/axios-helper';
import { User } from '../types/types';

class FriendshipService {
    async getAllFriends(id: string): Promise<User[]> {
        const axiosResponse: AxiosResponse = await axiosInstance.get(`/friendship/${id}`);
        return axiosResponse.data as User[];
    }

    async addFriend(id: string) {
        await axiosInstance.post(`/friendship/${id}`);
    }

    async deleteFriend(id: string) {
        await axiosInstance.delete(`/friendship/${id}`);
    }

    async createFriendRequest(id: string) {
        await axiosInstance.post(`/friendrequest/${id}`);
    }

    async deleteFriendRequest(id: string) {
        await axiosInstance.delete(`/friendrequest/${id}`);
    }
}

export default new FriendshipService();