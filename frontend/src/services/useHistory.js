import useFetchData from "../hooks/useFetchData";

export function useHistory({room, reset}) {
    const { data, isLoading, error} = useFetchData(reset ? `movements/${room}?reset=true` : `movements/${room}`)

    data.forEach(row => {
        switch (row.item) {
            case "1":
                row.item = "Reset";
                break;
            case "2":
                row.item = "Deposit";
                break;
            case "3":
                row.item = "Extraction";
                break;
            case "-1":
                row.item = "Other";
                break;
            default:
                break;
        }
    });

    return { data, isLoading, error };
}