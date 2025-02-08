import { useNavigate, useLocation } from "react-router-dom";
import { useEffect } from "react";

export default function Machines() {
    const navigate = useNavigate();
    const drink = useLocation().state;

    useEffect(() => {
        if (!drink) {
            navigate('/');
        }
    }, [drink, navigate]);
    
    if (!drink) {
        return(
            <div>No se ha recibido ningún item</div>
        );
    }

    return (
        <div className="section">
            <h3>{drink.name}</h3>
            {
                drink.machines.map((o, i) => {
                    return (
                        <div key={i} className="item-section" onClick={() => navigate('/details', { state: { ...drink, machines: [o]} })}>
                            <div className="row">
                                <div className="w-auto">
                                    <div className="list-icon">
                                        <img src={drink.machines[i].img ? drink.machines[i].img : 'https://i.imgur.com/fedszQ1.png'} alt="Imagen de máquina de expendedora." />
                                    </div>
                                </div>
                                <div className="col">
                                    <div className="d-flex flex-column">
                                        <span>{drink.machines[i].name}</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    );
                })
            }
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