import { createSlice } from "@reduxjs/toolkit";

const initialState = {
    login: {
        message: '',
        key: '',
        name: '',
        type: '',
        balance: 0
    }
};

const authSlice = createSlice({
    name: "login",
    initialState,
    reducers: {
        login: (state, action) => {
            state.login = action.payload;
        },
        logout: (state) => {
            state.login = null;
        },
        updateBalance: (state, action) => {
            state.login.balance = action.payload;
        }
    }
});

export const { login, logout, updateBalance } = authSlice.actions;
export default authSlice.reducer;