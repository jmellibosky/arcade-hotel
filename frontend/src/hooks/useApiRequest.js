import { useState } from "react";
import { config } from "../config/api";

export default function useApiRequest() {
    const { API_BASE_URL } = config;
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);

    const sendRequest = async ({ endpoint, method, body = null, headers = {} }) => {
        setIsLoading(true);
        setError(null);

        try {
            const response = await fetch(`${API_BASE_URL}${endpoint}`, {
                method,
                headers: {
                    "Content-Type": "application/json",
                    "Accept": "application/json",
                    ...headers,
                },
                body: body ? JSON.stringify(body) : null,
            });
            console.log(`Fetching: ${API_BASE_URL}${endpoint}-${JSON.stringify(body)}`);

            if (!response.ok) {
                throw new Error(`HTTP Error: ${response.status}`);
            }

            return await response.json();
        } catch (err) {
            console.error("Error fetching data:", err);
            throw new Error(err.message);
        } finally {
            setIsLoading(false);
        }
    };

    return { sendRequest, isLoading, error };
}
