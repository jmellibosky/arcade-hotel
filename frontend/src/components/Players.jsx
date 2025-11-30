import { useNavigate, useLocation } from "react-router-dom";
import { useEffect } from "react";

export default function Machines() {
    const navigate = useNavigate();
    const game = useLocation().state;

    useEffect(() => {
        if (!game) {
            navigate('/');
        }
    }, [game, navigate]);
    
    console.log(game);
    if (!game) {
        return(
            <div>No se ha recibido ningún item</div>
        );
    }

    return (
        <div className="section">
            <h3>{game.name}</h3>
            <div className="scroll-list">
            {
                Array.from({ length: game.players }).map((_, index) => {
                    return (
                        <div key={index} className="list-icon btn btn-secondary d-flex flex-row justify-content-center align-items-center" onClick={() => navigate('/details', { state: { ...game, player: index + 1 } })}>
                            <span>Jugador {index+1}</span>
                        </div>
                    );
                })
            }
            </div>
            <div className="item-section">                
                <div id="section-buttons">
                    <div className="btn-item" onClick={() => navigate(-1)}>
                        Volver
                    </div>
                </div>
            </div>
        </div>
    );
}