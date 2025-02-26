import { Routes, Route, useNavigate } from 'react-router-dom';

import { FaHistory } from 'react-icons/fa';
import { FaArrowRightToBracket } from 'react-icons/fa6'

import Home from './components/Home';
import Details from './components/Details';
import Machines from './components/Machines';
import History from './components/History';
import { useState } from 'react';
import { useSession } from './services/useUserTasks';
import Alert, { AlertError } from "./components/utils/Alert";
import { LoadingHover } from "./components/utils/Loading";

export default function AppUser() {
    const navigate = useNavigate();
    const { logout, isLoading, error } = useSession();
        
    const [showAlert, setShowAlert] = useState(false);
    const [configAlert, setConfigAlert] = useState({
        message: "",
        type: "success"
    });

    const logoutProcess = async () => {
        try {
            await logout({room: '303'});

            if (error) {
                throw new Error("Ocurrió un error al cerrar sesión.");
            } else {
                navigate('/login');
            }
        } catch (error) {
            setConfigAlert(AlertError("Ocurrió un error al cerrar sesión."));
            setShowAlert(true);
        }
    }

    return (    
        <>
        <div id="user-info">
            <span>Habitación {JSON.parse(localStorage.getItem('login')).name}</span>
            <div className="d-flex">
                <span className="user-info-item">$ 5000.00</span>
                <div className="user-info-item" onClick={() => navigate('/history')}>
                    <FaHistory />
                </div>
                <div className="user-info-item" onClick={() => logoutProcess()}>
                    <FaArrowRightToBracket />
                </div>
            </div>
        </div>
        <div className="container-fluid">
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/machines" element={<Machines />} />
                <Route path="/details" element={<Details />} />
                <Route path="/history" element={<History />} />
            </Routes>
        </div>
        {showAlert && (
            <Alert config={configAlert} onClose={() => setShowAlert(false)} />
        )}
        {isLoading && (
            <LoadingHover />
        )}
        </>
    );
}