import { I18nextProvider } from 'react-i18next';
import i18n from '../scripts/i18n.js';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { Provider } from 'react-redux'
import store, { persistor } from '../redux/store.js';
import { PersistGate } from 'redux-persist/integration/react';

export default function Providers({children}) {
    return (
        <Provider store={store}>
            <PersistGate loading={null} persistor={persistor}>
                <I18nextProvider i18n={i18n}>
                    <BrowserRouter>
                        <Routes>
                            <Route path="/*" element={children} />
                        </Routes>
                    </BrowserRouter>
                </I18nextProvider>
            </PersistGate>
        </Provider>
    );
}