import { useNavigate } from "react-router-dom";

import { FaUser } from 'react-icons/fa6';

export default function LoginAdmin() {
    const navigate = useNavigate();
    
    const checkPassword = () => {
        // check password
        navigate('/users');
    }

    return (
        <div className="container-fluid">   
            <h3>
                <span>Iniciar sesión admin</span>
                <div className="d-flex align-items-center btn-icon" onClick={() => navigate('/login')}>
                    <FaUser />
                </div>
            </h3>
            <div className="section-admin text-center">
                <img id="logo-admin" src='/assets/lyf.png' alt="Logo Luz y Fuerza" />
                <input type="number" placeholder="Usuario" />
                <input type="password" placeholder="Contraseña" />
                <div className="btn-admin" onClick={() => checkPassword()}>
                    Iniciar sesión
                </div>
            </div>
        </div>
    );
}