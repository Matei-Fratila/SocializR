import { AxiosResponse } from 'axios';
import axiosInstance from '../helpers/axios-helper';
import { Profile } from '../types/types';

class ProfileService {
    async getProfileAsync(id: string): Promise<Profile> {
        const axiosResult: AxiosResponse = await axiosInstance.get(`/Profile/${id}`);
        const profile: Profile = axiosResult.data;
        return profile;
    }

    async getAvatarAsync(id: string): Promise<string> {
        const axiosResult: AxiosResponse = await axiosInstance.get(`/Profile/avatar/${id}`);
        return axiosResult.data;
    }

    async editProfileAsync(data: any) {
        await axiosInstance.put(`/Profile`, data, {headers: {'Content-Type': 'multipart/form-data'}});
    }
}

export default new ProfileService();