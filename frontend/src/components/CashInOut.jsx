import { useNavigate, useLocation } from "react-router-dom";
import { useHistory } from '../services/useHistory';
import { useState } from "react";
import { useCash } from "../services/useUserTasks";
import { FaArrowLeft } from "react-icons/fa6";
import Alert, { AlertSuccess, AlertError } from "./utils/Alert";
import Loading from "./utils/Loading";

export default function CashInOut() {
    const navigate = useNavigate();
    const { extractCash, depositCash, isLoading: isLoadingCash, error: errorCash } = useCash();

    const user = useLocation().state;
    const [isDeposit, setIsDeposit] = useState(null);
    const [amount, setAmount] = useState('');
    const [showAlert, setShowAlert] = useState(false);
    const [configAlert, setConfigAlert] = useState({
        message: "",
        type: "success"
    });

    const { data: history, isLoading: isLoadingHistory, error: errorHistory } = useHistory({room: user.room, reset: true});

    const confirm = async () => {
        try {
            if (isDeposit) {
                await depositCash({ room: user.room, user: 1, amount: amount });
            } else {
                await extractCash({ room: user.room, user: 1, amount: amount });
            }
            
            setConfigAlert(AlertSuccess("Se realizó el movimiento correctamente"));
        } catch (error) {
            setConfigAlert(AlertError());
        }

        setShowAlert(true);
    };

    const OnAlertSuccessClose = () => {
        setShowAlert(false);
        navigate('/users/list');
    }

    const OnInputChange = (event) => {
        let valor = event.target.value.replace(/\D/g, "");

        setAmount(valor);
    }

    return (
        <div className="container-fluid">
            {showAlert && (
                <Alert config={configAlert} onClose={() => OnAlertSuccessClose()} />
            )}

            <div className="row">
                <div className="col-lg-6 col-md-12">
                    <div className="section-admin text-center">
                        <h3>
                            <span>Habitación N°{user.room}</span>
                            <div className="d-flex align-items-center btn-icon" onClick={() => navigate('/users/list')} title="Volver al listado">
                                <FaArrowLeft />
                            </div>
                        </h3>
                        <div className="pt-2">
                            Saldo actual:
                            <h4>$ {user.balance}</h4>
                        </div>
                        <hr />
                        Seleccione el tipo de movimiento:
                        <div className="row">
                            <div className="col-6">
                                <div className="btn-admin" onClick={() => setIsDeposit(true)}>
                                    Cargar
                                </div>
                            </div>
                            <div className="col-6">
                                <div className="btn-admin" onClick={() => setIsDeposit(false)}>
                                    Retirar
                                </div>
                            </div>
                        </div>
                        {isDeposit !== null && (
                            <>
                                <hr />
                                <span>{isDeposit ? "Cargar saldo" : "Retirar saldo"}</span>
                                <input type="text" value={amount} onChange={OnInputChange} />
                                <span>Saldo luego del movimiento:</span>
                                {
                                    isDeposit ?
                                    <h4>$ {user.balance + (amount ? parseInt(amount) : 0)}</h4> :
                                    <h4>$ {user.balance - (amount ? parseInt(amount) : 0)}</h4>
                                }
                                {
                                    (!isDeposit && user.balance - (amount ? parseInt(amount) : 0) < 0)
                                    ?
                                    <div className="btn-admin" style={{backgroundColor: "#900", cursor: "not-allowed"}}>
                                        Confirmar transacción
                                    </div>
                                    :
                                    <div className="btn-admin" onClick={() => confirm()}>
                                        Confirmar transacción
                                    </div>
                                }

                            </>    
                        )}
                    </div>
                </div>
                <div className="col-lg-6 col-md-12">
                    <div className="section-admin">
                        <h3>Movimientos</h3>
                        {!isLoadingHistory && !errorHistory && (
                            <div className="history mt-3 full-screen-scroll">
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
                            <div className="pt-2 text-center">
                                <span>No se ha podido recuperar el listado de habitaciones.</span>
                            </div>
                        )}
                        {(isLoadingHistory || isLoadingCash) && (
                            <Loading />
                        )}
                    </div>
                </div>
            </div>
        </div>
    );
}