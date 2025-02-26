import { useState } from 'react'
import { useNavigate } from "react-router-dom";
import { useSession } from "../services/useUserTasks";
import Alert, { AlertError } from "./utils/Alert";
import { LoadingHover } from "./utils/Loading";


export default function Login() {
    const navigate = useNavigate();
    const { login, isLoading, error } = useSession();
        
    const [showAlert, setShowAlert] = useState(false);
    const [configAlert, setConfigAlert] = useState({
        message: "",
        type: "success"
    });

    const [user, setUser] = useState('')
    const [pass, setPass] = useState('')

    const checkPassword = async () => {
        try {
            let result = await login({room: user, pass});
            if (error) {
                throw new Error("Error al iniciar sesión.");
            } else {
                console.log(result);
                localStorage.setItem('login', JSON.stringify({key: result.key, name: result.name}));
                if (result.type === 'admin') {
                    navigate('/users');
                } else {
                    navigate('/');
                }
            }
        } catch (error) {
            setConfigAlert(AlertError("Error al iniciar sesión."));
            setShowAlert(true);
        }
    }

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