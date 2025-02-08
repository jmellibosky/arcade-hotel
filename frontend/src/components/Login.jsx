import { useNavigate } from "react-router-dom";


export default function Login() {
    const navigate = useNavigate();
    
    const checkPassword = () => {
        // check password
        navigate('/');
    }

    return (
        <div className="container-fluid">   
            <h3>Iniciar sesión</h3>
            <div className="section text-center">
                <img id="logo" src='/assets/lyf.png' alt="Logo Luz y Fuerza" onClick={() => navigate('/login/admin')} />
                <input type="number" placeholder="N° de Habitación" />
                <input type="password" placeholder="Contraseña" />
                <div className="btn-item" onClick={() => checkPassword()}>
                    Iniciar sesión
                </div>
            </div>
        </div>
    );
}