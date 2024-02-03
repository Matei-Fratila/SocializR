import axios from 'axios';
import authHeader from './auth-header';

class FriendshipService {
    async getAllFriends(id: string) {
        await axios.get(`/api/friendship/${id}`, { headers: authHeader() });
    }

    async addFriend(id: string) {
        await axios.post(`/api/friendship/${id}`, {}, { headers: authHeader() });
    }

    async deleteFriend(id: string) {
        await axios.delete(`/api/friendship/${id}`, { headers: authHeader() });
    }

    async createFriendRequest(id: string) {
        await axios.post(`/api/friendrequest/${id}`, {}, { headers: authHeader() });
    }

    async deleteFriendRequest(id: string) {
        await axios.delete(`/api/friendrequest/${id}`, { headers: authHeader() });
    }
}

export default new FriendshipService();