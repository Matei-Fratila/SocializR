import { useEffect, useState } from 'react';
import 'bootstrap/dist/css/bootstrap-grid.min.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';
import './services/auth.service';
import './services/user.service';
import authService from './services/auth.service';
import userService from './services/user.service';
import Login from './components/Login.tsx';

interface Forecast {
    date: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}

function App() {
    const [forecasts, setForecasts] = useState<Forecast[]>();

    useEffect(() => {
        //populateWeatherData();
    }, []);

    const contents = <Login></Login>;

    return (
        <div>
            {contents}
        </div>
    );

    async function populateWeatherData() {
        let response = await authService.login('matei.fratila@essensys.ro', 'MateiFratila8');
        if(response?.data){
            response = await userService.getWeather();
        }
        setForecasts(response?.data);
    }
}

export default App;