import { Routes, Route, useNavigate } from 'react-router-dom';

import { FaArrowRightToBracket } from 'react-icons/fa6'

import Users from './components/Users';
import CashInOut from './components/CashInOut';

import { useState, useEffect } from 'react';
import { useSession } from './services/useUserTasks';
import Alert, { AlertError } from "./components/utils/Alert";
import { LoadingHover } from "./components/utils/Loading";

import { useDispatch, useSelector } from 'react-redux';
import { logout } from './redux/loginReducer';

export default function AppUser() {
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
        if (!userData || (userData && userData.type !== 'admin')) {
            navigate('/login');
        }
    }, [userData]);

    return (    
        <>
        {userData && (
            <>
            <div id="user-info">
                <span>{userData.name}</span>
                <div className="d-flex">
                    <div className="user-info-item" onClick={() => onLogout()}>
                        <FaArrowRightToBracket />
                    </div>
                </div>
            </div>
            <div className="container-fluid">
                <Routes>
                    <Route path="/list" element={<Users />} />
                    <Route path="/cash" element={<CashInOut />} />
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