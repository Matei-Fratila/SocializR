import { AxiosResponse } from 'axios';
import axiosInstance from '../helpers/axios-helper';
import { Comment as Comm, Comments } from "../types/types";
import authService from './auth.service';

class CommentService {
    async deleteComment(id: string) {
        return await axiosInstance.delete(`/Comments/${id}`);
    }

    async createComment(data: FormData): Promise<Comm> {
        const axiosResponse: AxiosResponse = await axiosInstance.post(`/Comments`, data, { headers: {'Content-Type': 'application/json' } });
        const comment: Comm = axiosResponse.data;
        comment.userPhoto = authService.getCurrentUserPhoto();
        return comment;
    }

    async loadComments(postId: string, pageNumber: number): Promise<Comments> {
        const axiosResponse: AxiosResponse = await axiosInstance.get(`/Comments`, {params: {postId: postId, pageNumber: pageNumber}});
        const comments: Comments = axiosResponse.data;
        return comments;
    }
}

export default new CommentService();