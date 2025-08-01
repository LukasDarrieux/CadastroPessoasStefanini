import React from 'react';

function NotFound(){
    return (
        <div className=" d-flex flex-column min-vh-100 overflow-y-hidden">
            <div className="container d-flex flex-grow-1 justify-content-center align-items-center">
                <div className="text-center p-4 w-100" style={{ maxWidth: "500px" }}>
                    <h1>404 - Not Found</h1>
                    <h3>Ops... Página não encontrada</h3>
                </div>
            </div>
        </div>
    );
}

export default NotFound;