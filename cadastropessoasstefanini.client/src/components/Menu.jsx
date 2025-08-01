import React from 'react';
import { useNavigate } from "react-router-dom";

const Menu = () => {

    const navigate = useNavigate();

    const logout = async (event) => {
        event.preventDefault();

        sessionStorage.removeItem("token");
        navigate("/");
    }

    return (
        <div className="d-flex mt-2 w-100 justify-content-end">
            <button className="btn btn-primary" onClick={logout}>
                <i className="mx-2 bi bi-box-arrow-left"></i>
                Logout   
            </button>
        </div>
    );
};

export default Menu;