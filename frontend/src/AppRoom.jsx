import { Routes, Route, useNavigate } from 'react-router-dom';

import { FaHistory } from 'react-icons/fa';
import { FaArrowRightToBracket } from 'react-icons/fa6'

import Home from './components/Home';
import Details from './components/Details';
import Machines from './components/Machines';
import History from './components/History';
import { useState, useEffect } from 'react';
import { useSession } from './services/useUserTasks';
import Alert, { AlertError } from "./components/utils/Alert";
import { LoadingHover } from "./components/utils/Loading";

import { useDispatch, useSelector } from 'react-redux';
import { logout } from './redux/loginReducer';

export default function AppRoom() {
    const navigate = useNavigate();
    const { logout: logoutTask, isLoading, error } = useSession();
        
    const [showAlert, setShowAlert] = useState(false);
    const [configAlert, setConfigAlert] = useState({
        message: "",
        type: "success"
    });

    const userData = useSelector((state) => state.login.login);
    console.log(userData);

    const dispatch = useDispatch();
    const onLogout = async () => {
        if (userData) {
            try {
                await logoutTask({room: userData.name, key: userData.key});
    
                if (error) {
                    throw new Error("Ocurrió un error al cerrar sesión.");
                } else {
                    dispatch(logout());
                }
            } catch (error) {
                setConfigAlert(AlertError("Ocurrió un error al cerrar sesión."));
                setShowAlert(true);
            }
        }
    }

    useEffect(() => {
        if (!userData || (userData && userData.type !== 'room')) {
            navigate('/login');
        }
    }, [userData]);

    return (    
        <>
        {userData && (
            <>
            <div id="user-info">
                <span>Habitación {userData.name}</span>
                <div className="d-flex">
                    <span className="user-info-item">$ {userData.balance}</span>
                    <div className="user-info-item" onClick={() => navigate('/history')}>
                        <FaHistory />
                    </div>
                    <div className="user-info-item" onClick={() => onLogout()}>
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
            </>
        )}
        {showAlert && (
            <Alert config={configAlert} onClose={() => setShowAlert(false)} />
        )}
        {isLoading && (
            <LoadingHover />
        )}
        </>
    );
}