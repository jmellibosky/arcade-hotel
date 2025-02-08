import { useEffect, useState } from 'react';
import { config } from '../config/api';

export default function useFetchData(endpoint) {
    const { API_BASE_URL } = config;

    const [data, setData] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        // Validación del endpoint
        if (!endpoint || typeof endpoint !== 'string') {
            setError("Invalid endpoint");
            setIsLoading(false);
            return;
        }

        const loadData = async () => {
            try {
                setIsLoading(true);
                const response = await fetch(`${API_BASE_URL}${endpoint}`);
                console.log(`Fetching: ${API_BASE_URL}${endpoint}`);
                
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                
                const result = await response.json();
                setData(result);
            } catch (err) {
                console.error("Error fetching data:", err);
                setError(err.message);
            } finally {
                setIsLoading(false);
            }
        };

        loadData();
    }, [endpoint, API_BASE_URL]);

    return { data, isLoading, error };
}
