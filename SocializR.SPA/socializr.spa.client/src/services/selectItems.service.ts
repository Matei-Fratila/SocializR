import { AxiosResponse } from 'axios';
import axiosInstance from '../helpers/axios-helper';
import { SelectItem } from '../types/types';

class LocationService {
    async getCounties(): Promise<SelectItem[]> {
        const axiosResponse: AxiosResponse = await axiosInstance.get(`/counties`);
        const counties: SelectItem[] = axiosResponse.data;
        return counties;
    }

    async getCities(id: string) {
        const axiosResponse: AxiosResponse = await axiosInstance.get(`/cities`, {params: {id: id}});
        const cities: SelectItem[] = axiosResponse.data;
        return cities;
    }

    async getInterests(): Promise<SelectItem[]> {
        const axiosResponse: AxiosResponse = await axiosInstance.get(`/interests`);
        const interests: SelectItem[] = axiosResponse.data;
        return interests;
    }

    async getGenders(): Promise<SelectItem[]> {
        const axiosResponse: AxiosResponse = await axiosInstance.get(`/genders`);
        const genders: SelectItem[] = axiosResponse.data;
        return genders;
    }
}

export default new LocationService();