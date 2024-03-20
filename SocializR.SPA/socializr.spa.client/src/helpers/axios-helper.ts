import axios, { AxiosError } from "axios";
import authService from "../services/auth.service";

const instance = axios.create({
    baseURL: (!process.env.NODE_ENV || process.env.NODE_ENV === 'development') ? "/api/" : "",
    headers: {
        "Content-Type": "application/json"
    },
    withCredentials: true
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
            const prevRequest = error?.config;

            if (status === 401 && !prevRequest?.sent) {
                try {
                    // attempting to refresh token;
                    prevRequest.sent = true;
                    const token = await authService.getRefreshToken();
                    authService.saveToken(token);
                    instance.defaults.headers.common["Authorization"] = `Bearer ${token}`;

                    return await instance(error.config);
                } catch (error) {
                    // Handle token refresh error.You can clear all storage and redirect the user to the login page
                    console.error(error);
                    if(error.response.status === 401) {
                        return window.location.href = "/login";
                    }
                }
            }

            return Promise.reject(error);

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