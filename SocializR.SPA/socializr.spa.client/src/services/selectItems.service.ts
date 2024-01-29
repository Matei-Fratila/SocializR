import axios, { AxiosResponse } from 'axios';
import { SelectItem } from '../types/types';

class LocationService {
    async getCounties(): Promise<SelectItem[]> {
        const axiosResponse: AxiosResponse = await axios.get(`/api/counties`);
        const counties: SelectItem[] = axiosResponse.data;
        return counties;
    }

    async getCities(id: string) {
        const axiosResponse: AxiosResponse = await axios.get(`/api/cities`, {params: {id: id}});
        const cities: SelectItem[] = axiosResponse.data;
        return cities;
    }

    async getInterests(): Promise<SelectItem[]> {
        const axiosResponse: AxiosResponse = await axios.get(`/api/interests`);
        const interests: SelectItem[] = axiosResponse.data;
        return interests;
    }

    async getGenders(): Promise<SelectItem[]> {
        const axiosResponse: AxiosResponse = await axios.get(`/api/genders`);
        const genders: SelectItem[] = axiosResponse.data;
        return genders;
    }
}

export default new LocationService();