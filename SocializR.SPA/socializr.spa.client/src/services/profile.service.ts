import axios, { AxiosResponse } from 'axios';
import { Profile } from '../types/types';
import authHeader from './auth-header';

class ProfileService {
    async getProfileAsync(id: string): Promise<Profile> {
        const axiosResult: AxiosResponse = await axios.get(`/api/Profile/${id}`, {headers: authHeader()});
        const profile: Profile = axiosResult.data;
        return profile;
    }

    async editProfileAsync(data: any): Promise<Profile> {
        const axiosResult: AxiosResponse = await axios.post(`/api/Profile`, data, {headers: {...authHeader(), 'Content-Type': 'multipart/form-data'}});
        const profile: Profile = axiosResult.data;
        return profile;
    }
}

export default new ProfileService();