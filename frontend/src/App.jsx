import { Routes, Route } from 'react-router-dom';

import AppRoom from './AppRoom';
import AppUser from './AppUser';
import Login from './components/Login';
import Background from './components/Background';

export default function App() {
  return (
    <>
    <Background />
    <Routes>
      <Route path="/login" element={<Login />} />
      <Route path="/*" element={<AppRoom />} />
      <Route path="/users/*" element={<AppUser />} />
    </Routes>
    </>
  );
}
