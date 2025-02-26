import { Routes, Route } from 'react-router-dom';

import AppUser from './AppUser';
import Login from './components/Login';
import Users from './components/Users';
import CashInOut from './components/CashInOut';
import Background from './components/Background';

export default function App() {
  return (
    <>
    <Background />
    <Routes>
      <Route path="/login" element={<Login />} />
      <Route path="/users" element={<Users />} />
      <Route path="/users/cash" element={<CashInOut />} />
      <Route path="/*" element={<AppUser />} />
    </Routes>
    </>
  );
}
