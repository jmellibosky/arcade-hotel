import loginReducer from "./loginReducer";
import { configureStore } from "@reduxjs/toolkit";
import { persistStore, persistReducer } from 'redux-persist'
import storageSession from "redux-persist/lib/storage/session"

const persistConfig = {
    key: "login",
    storage: storageSession
};

const persistedLoginReducer = persistReducer(persistConfig, loginReducer);

const store = configureStore({
    reducer: {
        login: persistedLoginReducer
    }
});

export const persistor = persistStore(store);
export default store;