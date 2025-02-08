import { useState, useEffect } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { useCash } from "../services/useUserTasks";
import Alert, { AlertSuccess, AlertError } from "./utils/Alert";
import { LoadingHover } from "./utils/Loading";

export default function Details() {
    const navigate = useNavigate();
    const item = useLocation().state;

    const [showAlert, setShowAlert] = useState(false);
    const [configAlert, setConfigAlert] = useState({
        message: "",
        type: "success"
    });

    const { processTransaction, isLoading: isLoadingCash, error: errorCash } = useCash();

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

    const confirm = async () => {
        try {
            await processTransaction({ room: "303", amount: item.price, drinkId: item.drinkId, gameId: item.gameId });

            if (errorCash) {
                throw new Error("Error en la venta");
            } else {
                setConfigAlert(AlertSuccess("Venta correcta"));
            }
        } catch (error) {
            setConfigAlert(AlertError("Error en la venta"));
        }

        setShowAlert(true);
    }

    return (
        <div className="section">
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