import axios from "axios";
import authService from "../services/auth.service";

const instance = axios.create({
    baseURL: (!process.env.NODE_ENV || process.env.NODE_ENV === 'development') ? "/api/" : ""
});

// Add a global request interceptor
instance.interceptors.request.use(
    (config) => {
        config.headers["Authorization"] = `Bearer ${authService.getAccessToken()}`;
        return config;
    },
    async (err) => {
        console.error(err);
        return Promise.reject(err);
    });

// Add a global response interceptor
instance.interceptors.response.use(
    (response) => {
        const { status, data, config } = response;
        console.log(`Response from ${config.url}:`, {
            code: status,
            ...data,
        });
        return response;
    },
    async (error) => {
        if (error.response) {
            const { status } = error.response;

            switch (status) {
                case 401:
                    // token has expired;
                    try {
                        // attempting to refresh token;
                        const response = await instance.post("auth/refresh", { accessToken: authService.getAccessToken() }, { headers: { 'Content-Type': 'application/json' } });
                        instance.defaults.headers.common["Authorization"] = `Bearer ${response.data}`;
                        const config = error.config;
                        return await axios({ method: config.method, url: config.url, data: config.data });
                    } catch (error) {
                        // Handle token refresh error.You can clear all storage and redirect the user to the login page
                        console.error(error);
                        return window.location.href = "/login";
                    }
                default:
                    return Promise.reject(error);
            }
        } else if (error.request) {
            // The request was made but no response was received
            return Promise.reject(error);
        } else {
            // Something happened in setting up the request that triggered an Error
            return Promise.reject(error);
        }
    }
);

export default instance;