import axios from 'axios';

class WeatherService {
    async getWeather() {
        return await axios.get('/WeatherForecast');
    }
}

export default new WeatherService();