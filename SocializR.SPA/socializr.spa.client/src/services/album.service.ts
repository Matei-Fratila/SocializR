import { AxiosResponse } from 'axios';
import axiosInstance from '../helpers/axios-helper';
import { Album, Media } from '../types/types';

class AlbumService {
    async getAlbum(id: string): Promise<Album> {
        const axiosResponse: AxiosResponse = await axiosInstance.get(`/Albums/${id}`);
        const album: Album = axiosResponse.data;
        return album;
    }

    async createAlbum(data: FormData): Promise<Album> {
        const axiosResponse: AxiosResponse = await axiosInstance.post(`/Albums`, data);
        const album: Album = axiosResponse.data;
        return album;
    }

    async deleteAlbum(id: string) {
        return await axiosInstance.delete(`/Albums/${id}`);
    }

    async getMedia(id: string): Promise<Media> {
        const axiosResponse: AxiosResponse = await axiosInstance.get(`/Albums/Media/${id}`);
        const media: Media = axiosResponse.data;
        return media;
    }

    async updateMedia(data: FormData): Promise<Media> {
        const axiosResponse: AxiosResponse = await axiosInstance.put(`/Albums/Media`, data);
        const media: Media = axiosResponse.data;
        return media;
    }

    async deleteMedia(id: string) {
        return await axiosInstance.delete(`/Albums/Media/${id}`);
    }
}

export default new AlbumService();