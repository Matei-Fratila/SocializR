import axios, { AxiosResponse } from 'axios';
import authHeader from './auth-header';
import { Album, Media } from '../types/types';

class AlbumService {
    async getAlbum(id: string): Promise<Album> {
        const axiosResponse: AxiosResponse = await axios.get(`/Albums/${id}`, { headers: authHeader() });
        const album: Album = axiosResponse.data;
        return album;
    }

    async createAlbum(data: FormData): Promise<Album> {
        const axiosResponse: AxiosResponse = await axios.post(`/Albums`, data, { headers: authHeader() });
        const album: Album = axiosResponse.data;
        return album;
    }

    async deleteAlbum(id: string) {
        return await axios.delete(`/Albums/${id}`, { headers: authHeader() });
    }

    async getMedia(id: string): Promise<Media> {
        const axiosResponse: AxiosResponse = await axios.get(`/Albums/Media/${id}`, { headers: authHeader() });
        const media: Media = axiosResponse.data;
        return media;
    }

    async updateMedia(data: FormData): Promise<Media> {
        const axiosResponse: AxiosResponse = await axios.put(`/Albums/Media`, data, { headers: authHeader() });
        const media: Media = axiosResponse.data;
        return media;
    }

    async deleteMedia(id: string) {
        return await axios.delete(`/Albums/Media/${id}`, { headers: authHeader() });
    }
}

export default new AlbumService();