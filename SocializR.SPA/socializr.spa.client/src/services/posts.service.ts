import axios, { AxiosResponse } from 'axios';
import authHeader from './auth-header';
import { Post } from '../types/types';
import authService from './auth.service';

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

    async createPost(data: FormData): Promise<Post> {
        const axiosResponse: AxiosResponse = await axios.post(`api/Posts`, data, { headers: authHeader() });
        const post: Post = axiosResponse.data;
        post.userPhoto = authService.getCurrentUserPhoto();
        return post;
    }

    async deletePost(id: string) {
        return await axios.delete(`api/Posts/delete/${id}`, { headers: authHeader() });
    }
}

export default new PostsService();