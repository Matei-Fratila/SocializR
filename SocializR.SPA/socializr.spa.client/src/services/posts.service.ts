import { AxiosResponse } from 'axios';
import axiosInstance from '../helpers/axios-helper';
import { Post } from '../types/types';
import authService from './auth.service';

class PostsService {
    async getPaginatedAsync(userId: string, pageNumber: number, isProfileView: boolean) {
        return await axiosInstance.get('/Posts', { params: { userId: userId, pageNumber: pageNumber, isProfileView: isProfileView } });
    }

    async likePost(id: string) {
        return await axiosInstance.post(`/Posts/like/${id}`);
    }

    async dislikePost(id: string) {
        return await axiosInstance.delete(`/Posts/like/${id}`);
    }

    async createPost(data: FormData): Promise<Post> {
        const axiosResponse: AxiosResponse = await axiosInstance.post(`/Posts`, data);
        const post: Post = axiosResponse.data;
        post.userPhoto = authService.getCurrentUserPhoto();
        return post;
    }

    async deletePost(id: string) {
        return await axiosInstance.delete(`/Posts/${id}`);
    }
}

export default new PostsService();