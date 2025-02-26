import { useState, useEffect } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { useCash, useBalance } from "../services/useUserTasks";
import Alert, { AlertSuccess, AlertError, AlertWarning } from "./utils/Alert";
import { LoadingHover } from "./utils/Loading";
import { useDispatch, useSelector } from "react-redux";
import { updateBalance } from "../redux/loginReducer";

export default function Details() {
    const navigate = useNavigate();
    const item = useLocation().state;

    const [showAlert, setShowAlert] = useState(false);
    const [configAlert, setConfigAlert] = useState({
        message: "",
        type: "success"
    });

    const { processTransaction, isLoading: isLoadingCash, error: errorCash } = useCash();

    const userData = useSelector((state) => state.login.login);
    const dispatch = useDispatch();

    useEffect(() => {
        if (!item) {
            navigate('/');
        }
    }, [item, navigate]);

    if (!item) {
        return(
            <div>No se ha recibido ningún item</div>
        );
    }

    const validateBalance = () => {
        return userData.balance - item.price > 0;
    }

    const confirm = async () => {
        try {
            if (!validateBalance()) {
                setConfigAlert(AlertWarning("No posees suficiente dinero para realizar esta compra."));
                setShowAlert(true);
                return;
            }

            await processTransaction({ room: userData.name, amount: item.price, drinkId: item.drinkId, gameId: item.gameId });

            if (errorCash) {
                throw new Error("Error en la venta");
            } else {
                setConfigAlert(AlertSuccess("Venta correcta"));
                dispatch(updateBalance(userData.balance - item.price));
            }
        } catch (error) {
            setConfigAlert(AlertError("Error en la venta"));
        }

        setShowAlert(true);
    }

    const OnAlertSuccessClose = () => {
        navigate('/');
    }

    return (
        <div className="section">    
            {showAlert && (
                <Alert config={configAlert} onClose={() => OnAlertSuccessClose()} />
            )}
            <h3>{item.name}</h3>
            <div className="item-section">
                <div className="row">
                    <div className="col">
                        <div className="d-flex flex-column">
                            <span>{item.machines[0].name}</span>
                            <h4>$ {item.price}</h4>
                        </div>
                    </div>
                    <div className="w-auto">
                        <div className="list-icon">
                            <img src={item.machines[0].img ? item.machines[0].img : 'https://i.imgur.com/fedszQ1.png'} alt="Imagen de máquina de juegos o expendedora." />
                        </div>
                    </div>
                </div>
                <div id="section-buttons">
                    <div className="btn-item" onClick={() => navigate(-1)}>
                        Volver
                    </div>
                    <div className="btn-item" onClick={() => confirm()}>
                        Confirmar
                    </div>
                </div>
            </div>
            {isLoadingCash && (
                <LoadingHover />
            )}
        </div>
    );
}