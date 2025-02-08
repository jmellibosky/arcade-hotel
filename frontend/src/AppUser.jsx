import { Routes, Route, useNavigate } from 'react-router-dom';

import { FaHistory } from 'react-icons/fa';
import { FaArrowRightToBracket } from 'react-icons/fa6'

import Home from './components/Home';
import Details from './components/Details';
import Machines from './components/Machines';
import History from './components/History';

export default function AppUser() {
    const navigate = useNavigate();

    return (    
        <>
        <div id="user-info">
            <span>Habitación 303</span>
            <div className="d-flex">
                <span className="user-info-item">$ 5000.00</span>
                <div className="user-info-item" onClick={() => navigate('/history')}>
                    <FaHistory />
                </div>
                <div className="user-info-item" onClick={() => navigate('/login')}>
                    <FaArrowRightToBracket />
                </div>
            </div>
        </div>
        <div className="container-fluid">
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/machines" element={<Machines />} />
                <Route path="/details" element={<Details />} />
                <Route path="/history" element={<History />} />
            </Routes>
        </div>
        </>
    );
}