import React, { useState } from 'react';
import { Link, useNavigate } from "react-router-dom";

function NovoUsuario() {

    const navigate = useNavigate();

    const [nome, setNome] = useState("");
    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");

    const handleNomeChange = (event) => {
        setNome(event.target.value);
    }

    const handleEmailChange = (event) => {
        setEmail(event.target.value);
    }

    const handleSenhaChange = (event) => {
        setSenha(event.target.value);
    }

    const cadastrar = async (event) => {
        event.preventDefault();

        document.getElementById("mensagem").innerHTML = "";
        const url = "/api/Usuario";

        const usuario = {
            nome: nome,
            email: email,
            senha: senha
        };

        try {
            const request = await fetch(url, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(usuario)
            });

            var response = await request;

            if (response.ok) {

                navigate("/");

            } else {
                const BAD_REQUEST = 400;
                if (response.status === BAD_REQUEST) {
                    const retorno = await response.json();
                    if (retorno.length > 0) {
                        var mensagens = "";
                        retorno.forEach(function (mensagem) {
                            mensagens += mensagem + "<br/>";
                        });
                        document.getElementById("mensagem").innerHTML = mensagens;
                    }
                    else {
                        document.getElementById("mensagem").innerHTML = "Ops... Não foi possível efetuar o cadastro";
                    }
                }
                else
                    document.getElementById("mensagem").innerHTML = "Ops... Não foi possível efetuar o cadastro";
            }

        }
        catch (error) {
            console.error("Erro ao cadastrar usuário: ", error);
        }
    }

    return (
        <div className="d-flex flex-column min-vh-100 overflow-y-hidden">
            <main className="container d-flex flex-grow-1 justify-content-center align-items-center">
                <div className="card p-4 shadow w-100" style={{ maxWidth: "400px" }}>
                    <h2 className="text-center">Novo Usuário</h2>

                    <div id="mensagem" className="text-danger text-center my-2"></div>

                    <div className="mb-3">
                        <label htmlFor="nome" className="form-label">Nome</label>
                        <input type="text" className="form-control" placeholder="Nome" id="nome" name="nome" value={nome} onChange={handleNomeChange} required />
                    </div>

                    <div className="mb-3">
                        <label htmlFor="email" className="form-label">E-mail</label>
                        <input type="email" className="form-control" placeholder="exemplo@email.com" id="email" name="email" value={email} onChange={handleEmailChange} required />
                    </div>

                    <div className="mb-3">
                        <label htmlFor="senha" className="form-label">Senha</label>
                        <input type="password" className="form-control" placeholder="********" id="senha" name="senha" value={senha} onChange={handleSenhaChange} required />
                    </div>

                    <div className="d-flex justify-content-center">
                        <button type="submit" className="btn btn-primary mx-3" onClick={cadastrar}>
                            <i className="bi bi-plus-circle"></i>
                            <span className="mx-2">Cadastrar</span>
                        </button>
                        <Link className="btn btn-danger" to="/">
                            <i className="bi bi-x-circle"></i>
                            <span className="mx-2">Cancelar</span>
                        </Link>
                    </div>

                </div>
            </main>
            
        </div>
    );
}

export default NovoUsuario;