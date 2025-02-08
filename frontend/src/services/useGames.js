import useFetchData from "../hooks/useFetchData";

export function useGames() {
    const { data, isLoading, error } = useFetchData('games');

    return { data, isLoading, error };
}