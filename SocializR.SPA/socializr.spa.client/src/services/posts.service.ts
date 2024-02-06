import axios, { AxiosResponse } from 'axios';
import authHeader from './auth-header';
import { Post } from '../types/types';
import authService from './auth.service';

class PostsService {
    async getPaginatedAsync(userId: string, pageNumber: number, isProfileView: boolean) {
        return await axios.get('/Posts', { params: { userId: userId, pageNumber: pageNumber, isProfileView: isProfileView }, headers: authHeader() });
    }

    async likePost(id: string) {
        return await axios.post(`/Posts/like/${id}`, {}, { headers: authHeader() });
    }

    async dislikePost(id: string) {
        return await axios.delete(`/Posts/like/${id}`, { headers: authHeader() });
    }

    async createPost(data: FormData): Promise<Post> {
        const axiosResponse: AxiosResponse = await axios.post(`/Posts`, data, { headers: authHeader() });
        const post: Post = axiosResponse.data;
        post.userPhoto = authService.getCurrentUserPhoto();
        return post;
    }

    async deletePost(id: string) {
        return await axios.delete(`/Posts/${id}`, { headers: authHeader() });
    }
}

export default new PostsService();