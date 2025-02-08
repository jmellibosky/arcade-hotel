import useApiRequest from "../hooks/useApiRequest"

export function useSession() {
    const { sendRequest, isLoading, error } = useApiRequest();

    const roomLogin = async ({ room, pass }) => {
        
    }
    
    const adminLogin = async ({ user, pass }) => {
        
    }
    
    const roomLogout = async ({ room }) => {
        
    }
    
    const adminLogout = async ({ user }) => {
        
    }

    return { roomLogin, adminLogin, roomLogout, adminLogout, isLoading, error };
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

    const processTransaction = async({ room, amount, drinkId, gameId}) => {
        console.log({
            room: room,
            amount: amount,
            drinkId: drinkId ?? null,
            gameId: gameId ?? null
        });
        return await sendRequest(
            {
                endpoint: 'movements/transaction',
                method: 'POST',
                body: {
                    room: room,
                    amount: amount,
                    drinkId: drinkId ?? null,
                    gameId: gameId ?? null
                }
            }
        )
    }

    return { extractCash, depositCash, resetCash, processTransaction, isLoading, error };
}