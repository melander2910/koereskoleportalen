import { useEffect, useState } from "react";
import apiClient from "../services/api-client";
import { AxiosRequestConfig, CanceledError } from "axios";

const useData = <T>(endpoint: string, requestConfig?: AxiosRequestConfig, dependencies?: any) => {
    const [data, setData] = useState<T[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState("");

    useEffect(
        () => {
            const controller = new AbortController();
            setIsLoading(true);
            apiClient
                .get(endpoint, {
                    signal: controller.signal,
                    ...requestConfig,
                })
                .then((res) => setData(res.data))
                .catch((error) => {
                    if (error instanceof CanceledError) return;
                    setError(error);
                })
                .finally(() => {
                    if (!controller.signal.aborted) {
                        setIsLoading(false);
                    }
                });
            return () => controller.abort();
        },
        dependencies ? [...dependencies] : []
    );

    return { data, isLoading, error };
};

export default useData;
