import axios from 'axios';
import authHeader from './auth-header';

class WeatherService {
    async getWeather() {
        return await axios.get('api/WeatherForecast', { headers: authHeader() });
    }
}

export default new WeatherService();