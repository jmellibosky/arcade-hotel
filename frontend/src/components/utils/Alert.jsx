export default function Alert({ config, onClose }) {
    const { message, type } = config;
    
    const getAlertClass = () => {
        switch (type) {
            case "success":
                return "alert-success";
            case "warning":
                return "alert-warning";
            case "error":
                return "alert-error";
            default:
                return "alert-default";
        }
    };

    return (
        <div className="full-screen-alert">
            <div className={`alert-box ${getAlertClass()}`}>
                <p>{message}</p>
                <button onClick={onClose} className="close-btn">Close</button>
            </div>
        </div>
    );
};

export const AlertError = () => {
    return { message: "An error has ocurred.", type: "error" }
}

export const AlertWarning = (message) => {
    return { message: `Warning! ${message}`, type: "warning" }
}

export const AlertSuccess = (message) => {
    return { message: message, type: "success" }
}