import { I18nextProvider } from 'react-i18next';
import i18n from '../scripts/i18n.js';
import { BrowserRouter, Routes, Route } from 'react-router-dom';

export default function Providers({children}) {
    return (
        <I18nextProvider i18n={i18n}>
            <BrowserRouter>
                <Routes>
                    <Route path="/*" element={children} />
                </Routes>
            </BrowserRouter>
        </I18nextProvider>
    );
}