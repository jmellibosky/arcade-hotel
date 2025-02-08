import { useNavigate } from 'react-router-dom';
import { useHistory } from '../services/useHistory';
import { FaArrowLeft } from 'react-icons/fa'
import Loading from './utils/Loading';

export default function History() {
    const { data: history, isLoading: isLoadingHistory, error: errorHistory } = useHistory({room: 303, reset: false});
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
            <div className="section-admin">
                {errorHistory && (
                    <div className="text-center">
                        <span>No se ha podido recuperar el listado de habitaciones.</span>
                    </div>
                )}
                {isLoadingHistory && (
                    <Loading />
                )}
            </div>
        </div>
    );
}