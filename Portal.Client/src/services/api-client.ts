import axios from "axios";

const apiClient = axios.create({
    baseURL: import.meta.env.VITE_PORTAL_API_URL,
    params: {
        //key: import.meta.env.VITE_API_KEY,
    },
});

export default apiClient;
