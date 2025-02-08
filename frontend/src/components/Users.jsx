import { useState } from "react";
import { useRooms } from "../services/useRooms";
import { FaEye, FaEyeSlash, FaArrowRightToBracket } from 'react-icons/fa6';
import { useNavigate } from 'react-router-dom';
import { useCash, usePassword } from "../services/useUserTasks";
import Alert, { AlertSuccess, AlertError } from "./utils/Alert";
import Loading from "./utils/Loading";

export default function Users() {
    const [editingRoom, setEditingRoom] = useState(null);
    const [editingPass, setEditingPass] = useState("");
    const [search, setSearch] = useState("");
    const [showAlert, setShowAlert] = useState(false);
    const [configAlert, setConfigAlert] = useState({
        message: "",
        type: "success"
    });

    const navigate = useNavigate();

    const { data: rooms, isLoading: isLoadingRooms, error: errorRooms } = useRooms();
    const { changePassword, isLoading: isLoadingPassword, error: errorPassword } = usePassword();
    const { resetCash, isLoading: isLoadingCash, error: errorCash } = useCash();

    const startEditing = (room, currentPass) => {
        setEditingRoom(room);
        setEditingPass(currentPass);
    };

    const stopEditing = () => {
        setEditingRoom(null);
        setEditingPass("");
    };

    const savePassword = async () => {
        if (!editingRoom) return;

        try {
            await changePassword({ room: editingRoom, newPass: editingPass });
            if (errorPassword) {
                throw new Error();
            } else {
                setConfigAlert(AlertSuccess("Password was saved successfully."));
                rooms.filter((o) => o.room === editingRoom)[0].pass = editingPass;
            }
        } catch (error) {
            setConfigAlert(AlertError());
        }
        setShowAlert(true);

        stopEditing();
    };

    const restartUser = async (room) => {
        try {
            await resetCash({ room: room, user: 1 });

            rooms.filter((o) => o.room === room)[0].balance = 0;
            setConfigAlert(AlertSuccess("Room's balance was reset successfully."));
        } catch {
            setConfigAlert(AlertError("Error resetting the room's balance."));
        }
        setShowAlert(true);
    };

    return (
        <div className="container-fluid">
            {showAlert && (
                <Alert config={configAlert} onClose={() => setShowAlert(false)} />
            )}

            <div className="section-admin">
                <h3>
                    Habitaciones
                    <div
                        className="d-flex align-items-center btn-icon"
                        onClick={() => navigate('/login/admin')}
                        title="Cerrar sesión"
                    >
                        <FaArrowRightToBracket />
                    </div>
                </h3>
                {!isLoadingRooms && !errorRooms && (
                    <>
                    <div className="table-content table-header mt-3">
                        <div className="col-1 text-center">
                            <input
                                style={{ width: "5em" }}
                                type="number"
                                placeholder="Buscar"
                                value={search}
                                onChange={(e) => setSearch(e.target.value)}
                            />
                        </div>
                        <div className="col-3 text-center">Habitación</div>
                        <div className="col-2 text-center">Contraseña</div>
                        <div className="col-3 text-center">Saldo</div>
                        <div className="col-3 text-center">Acciones</div>
                    </div>
                    {
                        rooms
                        .filter((o) => o.room.toString().includes(search))
                        .map((o, i) => (
                            <div key={o.room} className={(i % 2 === 0 ? "table-content table-row-even" : "table-content table-row-odd")}>
                                <div className="col-1 text-center">{i + 1}</div>
                                <div className="col-3 text-center">{o.room}</div>
                                <div className="col-2 text-center">
                                    {editingRoom === o.room ? (
                                        <div className="d-flex align-items-center">
                                            <span className="btn-item" onClick={() => stopEditing()}>
                                                <FaEyeSlash />
                                            </span>
                                            <div className="input-group">
                                                <input
                                                    className="form-control"
                                                    type="number"
                                                    value={editingPass}
                                                    onChange={(e) => setEditingPass(e.target.value)}
                                                    />
                                                <button className="btn-admin" onClick={() => savePassword()}>
                                                    Guardar
                                                </button>
                                            </div>
                                        </div>
                                    ) : (
                                        <div className="d-flex align-items-center">
                                            <span className="btn-item" onClick={() => startEditing(o.room, o.pass)} >
                                                <FaEye />
                                            </span>
                                            <input className="form-control" disabled value="****" />
                                        </div>
                                    )}
                                </div>
                                <div className="col-3 text-center">$ {o.balance}</div>
                                <div className="col-3 text-center">
                                    <div className="row">
                                        <div className="col">
                                            <button
                                                className="btn-admin"
                                                onClick={() => navigate('/users/cash', { state: o })}
                                            >
                                                Cargar/Retirar
                                            </button>
                                        </div>
                                        <div className="col">
                                            <button
                                                className="btn-admin"
                                                style={{backgroundColor: "#900"}}
                                                onClick={() => restartUser(o.room)}
                                            >
                                                Reiniciar
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        ))
                    }
                    </>
                )}
                {errorRooms && (
                    <div className="section-admin">
                        <div className="text-center">
                            <span>No se han podido recuperar las habitaciones.</span>
                        </div>
                    </div>
                )}
                {(isLoadingRooms || isLoadingCash || isLoadingPassword) && (
                    <div className="section-admin">
                        <Loading />
                    </div>
                )}
            </div>
        </div>
    );
}
