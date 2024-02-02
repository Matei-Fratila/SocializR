import axios, { AxiosResponse } from 'axios';
import authHeader from './auth-header';
import { Comment as Comm, Comments } from "../types/types";
import authService from './auth.service';

class CommentService {
    async deleteComment(id: string) {
        return await axios.delete(`/api/Comments/${id}`, { headers: authHeader() });
    }

    async createComment(data: FormData): Promise<Comm> {
        const axiosResponse: AxiosResponse = await axios.post(`/api/Comments`, data, { headers: { ...authHeader(), 'content-type': 'application/json' } });
        const comment: Comm = axiosResponse.data;
        comment.userPhoto = authService.getCurrentUserPhoto();
        return comment;
    }

    async loadComments(postId: string, pageNumber: number): Promise<Comments> {
        const axiosResponse: AxiosResponse = await axios.get(`/api/Comments`, {params: {postId: postId, pageNumber: pageNumber}, headers: authHeader() });
        const comments: Comments = axiosResponse.data;
        return comments;
    }
}

export default new CommentService();