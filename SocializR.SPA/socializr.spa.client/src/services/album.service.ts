import axios, { AxiosResponse } from 'axios';
import authHeader from './auth-header';
import { Album, Media } from '../types/types';

class AlbumService {
    async getAlbum(id: string): Promise<Album> {
        const axiosResponse: AxiosResponse = await axios.get(`/api/Albums/${id}`, { headers: authHeader() });
        const album: Album = axiosResponse.data;
        return album;
    }

    async deleteAlbum(id: string) {
        return await axios.delete(`/api/Albums/media/${id}`, { headers: authHeader() });
    }

    async getMedia(id: string): Promise<Media> {
        const axiosResponse: AxiosResponse = await axios.get(`/api/Albums/Media/${id}`, { headers: authHeader() });
        const media: Media = axiosResponse.data;
        return media;
    }
}

export default new AlbumService();