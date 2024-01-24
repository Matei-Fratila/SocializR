import axios, { AxiosResponse } from 'axios';
import authHeader from './auth-header';

class PostsService{
    async getPaginatedAsync(page: number){
        return await axios.get('api/Posts', { params: {pageNumber: page}, headers: authHeader() });
    }
}

export default new PostsService();