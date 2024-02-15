import axios, { AxiosResponse } from 'axios';
import { Profile } from '../types/types';
import authHeader from './auth-header';

class ProfileService {
    async getProfileAsync(id: string): Promise<Profile> {
        const axiosResult: AxiosResponse = await axios.get(`/Profile/${id}`, {headers: authHeader()});
        const profile: Profile = axiosResult.data;
        return profile;
    }

    async getAvatarAsync(id: string): Promise<string> {
        const axiosResult: AxiosResponse = await axios.get(`/Profile/avatar/${id}`);
        return axiosResult.data;
    }

    async editProfileAsync(data: any) {
        await axios.put(`/Profile`, data, {headers: {...authHeader(), 'Content-Type': 'multipart/form-data'}});
    }
}

export default new ProfileService();