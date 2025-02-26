import { useNavigate } from 'react-router-dom';
import { useHistory } from '../services/useHistory';
import { FaArrowLeft } from 'react-icons/fa'
import Loading from './utils/Loading';
import { useSelector } from 'react-redux';

export default function History() {
    const userData = useSelector((state) => state.login.login);

    const { data: history, isLoading: isLoadingHistory, error: errorHistory } = useHistory({room: userData.name, reset: false});
    const navigate = useNavigate();

    return(
        <div className="section">
            <h3>
                <span>Movimientos</span>
                <div className="d-flex align-items-center btn-icon" onClick={() => navigate(-1)}>
                    <FaArrowLeft />
                </div>    
            </h3>
            {!isLoadingHistory && !errorHistory && (
                <div className="history full-screen-scroll">
                {
                    history.map((o, i) => {
                        return (
                            <div key={i} className="history-item">
                                <div className="d-flex justify-content-between">
                                    <span>{o.item}</span>
                                    <span>$ {o.price}</span>
                                </div>
                                <span>{new Date(Date.parse(o.time)).toLocaleDateString('es', { weekday:"long", year:"numeric", month:"long", day:"numeric"})}</span>
                            </div>
                        );
                    })
                }
                </div>
            )}
            {errorHistory && (
                <div className="text-center">
                    <span>No hay movimientos.</span>
                </div>
            )}
            {isLoadingHistory && (
                <Loading />
            )}
        </div>
    );
}