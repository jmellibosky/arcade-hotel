import useFetchData from "../hooks/useFetchData";

export function useDrinks() {
    const { data, isLoading, error } = useFetchData('drinks');
    
    return { data, isLoading, error };
}