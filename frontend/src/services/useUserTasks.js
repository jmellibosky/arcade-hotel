import useApiRequest from "../hooks/useApiRequest"
import useFetchData from "../hooks/useFetchData";

export function useSession() {
    const { sendRequest, isLoading, error } = useApiRequest();

    const login = async ({ room, pass }) => {
        return await sendRequest(
            {
                endpoint: 'users',
                method: 'POST',
                body: {
                    user: room,
                    pass: pass
                }
            }
        )
    }
    
    const logout = async ({ room, key }) => {
        return await sendRequest(
            {
                endpoint: 'users',
                method: 'DELETE',
                body: {
                    user: room,
                    key: key
                }
            }
        )
    }

    return { login, logout, isLoading, error };
}

export function usePassword() {
    const { sendRequest, isLoading, error } = useApiRequest();
    
    const changePassword = async ({ room, newPass }) => {
        return await sendRequest(
            {
                endpoint: 'rooms/pass',
                method: 'PUT',
                body: {
                    room: room,
                    pass: newPass
                }
            }
        )
    }

    return { changePassword, isLoading, error };
}

export function useCash() {
    const { sendRequest, isLoading, error } = useApiRequest();

    const extractCash = async ({ room, user, amount }) => {
        return await sendRequest(
            {
                endpoint: 'movements/extraction',
                method: 'POST',
                body: {
                    room: room,
                    userId: user,
                    amount: amount
                } 
            }
        );
    }
    
    const depositCash = async ({ room, user, amount }) => {
        return await sendRequest(
            {
                endpoint: 'movements/deposit',
                method: 'POST',
                body: {
                    room: room,
                    userId: user,
                    amount: amount
                } 
            }
        );
    }
    
    const resetCash = async ({ room, user }) => {
        return await sendRequest(
            {
                endpoint: 'movements/reset',
                method: 'POST',
                body: {
                    room: room,
                    userId: user,
                    amount: '0'
                } 
            }
        );
    }

    const processTransaction = async ({ room, amount, drinkId, gameId, player}) => {
        return await sendRequest(
            {
                endpoint: 'movements/transaction',
                method: 'POST',
                body: {
                    room: room,
                    amount: amount,
                    drinkId: drinkId ?? null,
                    gameId: gameId ?? null,
                    playerNumber: player ?? 1
                }
            }
        )
    }

    const testMqtt = async () => {
        console.log(2);
        return await sendRequest(
            {
                endpoint: 'movements/mqtt',
                method: 'POST'
            }
        )
    }

    return { extractCash, depositCash, resetCash, processTransaction, testMqtt, isLoading, error };
}