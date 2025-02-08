import { FaSpinner } from 'react-icons/fa6'

export default function Loading() {
    return (
        <div className="bg-loading">
            <div className="spinner-border"></div>
        </div>
    );
}

export function LoadingHover() {
    return (
        <div className="bg-hover">
            <div>
                <div className="spinner-border"></div>
            </div>
        </div>
    )
}