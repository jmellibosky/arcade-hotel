import { useState, useEffect } from 'react'
import { useNavigate } from "react-router-dom";
import { useSession } from "../services/useUserTasks";
import Alert, { AlertError, AlertSuccess } from "./utils/Alert";
import { LoadingHover } from "./utils/Loading";
import { useDispatch, useSelector } from 'react-redux';
import { login } from '../redux/loginReducer';

export default function Login() {
    const navigate = useNavigate();
    const { login: loginTask, testMqtt, isLoading, error } = useSession();
        
    const [showAlert, setShowAlert] = useState(false);
    const [configAlert, setConfigAlert] = useState({
        message: "",
        type: "success"
    });

    const [user, setUser] = useState('')
    const [pass, setPass] = useState('')

    const dispatch = useDispatch();
    const checkPassword = async () => {
        try {
            let result = await loginTask({room: user, pass});
            if (error) {
                throw new Error("Error al iniciar sesión.");
            } else {
                dispatch(login(result));

                if (result.type === 'admin') {
                    navigate('/users/list');
                } else {
                    navigate('/');
                }
            }
        } catch (error) {
            setConfigAlert(AlertError("Error al iniciar sesión."));
            setShowAlert(true);
        }
    }

    const onTestMqtt = async () => {
        console.log('onTestMqtt');
        try {
            let result = await testMqtt();
            if (error) {
                throw new Error("Error al testear MQTT.");
            } else {
                setConfigAlert(AlertSuccess("Salió bien MQTT (ahora sí)"));
                setShowAlert(true);
            }
        } catch (error) {
            setConfigAlert(AlertError("Falló MQTT"));
            setShowAlert(true);
        }
    }

    const userData = useSelector((state) => state.login.login);
    
    useEffect(() => {
        console.log(userData);
        if (userData) {
            switch (userData.type) {
                case 'admin':
                    navigate('/users/list');
                    break;
                case 'room':
                    navigate('/');
                    break;
            }
        }
    }, [userData]);

    return (
        <div className="container-fluid">   
            <h3>Iniciar sesión</h3>
            <div className="section text-center">
                <img id="logo" src='/assets/lyf.png' alt="Logo Luz y Fuerza" />
                <input type="text" placeholder="N° de Habitación" onChange={(event) => setUser(event.target.value)} />
                <input type="password" placeholder="Contraseña" onChange={(event) => setPass(event.target.value)} />
                <div className="btn-item" onClick={() => checkPassword()}>
                    Iniciar sesión
                </div>
                <div className="btn-item" onClick={() => onTestMqtt()}>
                    Test MQTT
                </div>
            </div>
            {showAlert && (
                <Alert config={configAlert} onClose={() => setShowAlert(false)} />
            )}
            {isLoading && (
                <LoadingHover />
            )}
        </div>
    );
}