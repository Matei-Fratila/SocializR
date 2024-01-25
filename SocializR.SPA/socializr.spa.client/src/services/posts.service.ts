import axios, { AxiosResponse } from 'axios';
import authHeader from './auth-header';

class PostsService {
    async getPaginatedAsync(page: number) {
        return await axios.get('api/Posts', { params: { pageNumber: page }, headers: authHeader() });
    }

    async likePost(id: string) {
        return await axios.post(`api/Posts/like/${id}`, {}, { headers: authHeader() });
    }

    async dislikePost(id: string) {
        return await axios.delete(`api/Posts/like/delete/${id}`, { headers: authHeader() });
    }
}

export default new PostsService();