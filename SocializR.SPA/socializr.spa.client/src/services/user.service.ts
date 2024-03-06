import { AxiosResponse } from 'axios';
import axiosInstance from '../helpers/axios-helper';
import { User } from '../types/types';

class UsersService {
    async searchUsers(searchKey: string, pageIndex: number): Promise<User[]> {
        const axiosResult: AxiosResponse = await axiosInstance.get('/Users', { params: { searchKey: searchKey, pageIndex: pageIndex } });
        const users: User[] = axiosResult.data;
        return users;
    }
}

export default new UsersService();