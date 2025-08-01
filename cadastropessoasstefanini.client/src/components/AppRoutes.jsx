import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

import Login from "../pages/Login";
import Pessoa from "../pages/Pessoa";
import PessoaCreate from "../pages/PessoaCreate";
import PessoaEdit from "../pages/PessoaEdit";
import NotFound from "../pages/NotFound";
import NovoUsuario from '../pages/NovoUsuario';

function AppRoutes() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Login />} />
                <Route path="/Pessoa" element={<Pessoa />} />
                <Route path="/Pessoa/Create" element={<PessoaCreate />} />
                <Route path="/Pessoa/Edit/:id" element={<PessoaEdit />} />
                <Route path="/NovoUsuario" element={<NovoUsuario />} />
                <Route path="*" element={<NotFound />} />
            </Routes>
        </Router>
    );
}

export default AppRoutes;