import { useNavigate } from "react-router-dom";
import { useDrinks } from "../services/useDrinks";
import { useGames } from "../services/useGames";
import Loading from "./utils/Loading";

export default function Home() {
    const { data: drinks, isLoading: isLoadingDrinks, error: errorDrinks } = useDrinks();
    const { data: games, isLoading: isLoadingGames, error: errorGames } = useGames();
    const navigate = useNavigate();

    return (
        <>
        <div className="section">
            <h3>Bebidas</h3>
                {!isLoadingDrinks && !errorDrinks && (
                <div className="scroll-list">
                {
                    drinks.map((o, i) => {
                        return (
                            <div id={"icon-" + i} key={i} className="list-icon" onClick={() => navigate('/machines', { state: o })}>
                                <img src={o.img} alt={o.name} />
                            </div>
                        );
                    })
                }    
                </div>
                )}
                {errorDrinks && (
                    <div className="section-admin">
                        <span className="text-dark">No se han podido recuperar las bebidas.</span>
                    </div>
                )}
                {isLoadingDrinks && (
                    <div>
                        <Loading />
                    </div>
                )}
        </div>
        <div className="section">
            <h3>Juegos</h3>
            {!isLoadingGames && !errorGames && (
            <div className="scroll-list">
                {
                    games.map((o, i) => {
                        return (
                            <div id={"icon-" + i} key={i} className="list-icon" onClick={() => navigate('/players', { state: o })}>
                                <img src={o.img} alt={o.name} />
                            </div>
                        );
                    })
                }
            </div>
            )}
            {errorGames && (
                <div className="section-admin">
                    <span className="text-dark">No se han podido recuperar los juegos.</span>
                </div>
            )}
            {isLoadingGames && (
                <div>
                    <Loading />
                </div>
            )}
        </div>
        </>
    );
}