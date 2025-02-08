import useFetchData from "../hooks/useFetchData";

export function useRooms() {
    const { data, isLoading, error } = useFetchData('rooms');

    return { data, isLoading, error };
}