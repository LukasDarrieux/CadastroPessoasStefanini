import { formatarCPF, formatarData } from "../Util";
import { React, useEffect, useState } from 'react'
import { Link, useNavigate } from "react-router-dom";
import Modal from "react-modal";
import Menu from "../components/Menu";

function Pessoa() {

    const NAO_AUTORIZADO = 401;

    const [id, setId] = useState(0);
    const [modalIsOpen, setModalIsOpen] = useState(false);

    const [pessoas, setPessoas] = useState();
    const [token, setToken] = useState();

    const navigate = useNavigate();

    useEffect(() => {
        const tokenSessao = sessionStorage.getItem("token");
        if (tokenSessao == undefined) {
            navigate("/");
            return;
        } 
        setToken(tokenSessao);
        loadPessoas(tokenSessao);
    }, []);

    async function loadPessoas(tokenSessao) {
        const url = "/api/Pessoa";

        const response = await fetch(url, {
            method: "GET",
            headers: {
                "Authorization": "Bearer " + tokenSessao,
                "Content-Type": "application/json"
            }
        });

        if (response.status === NAO_AUTORIZADO) {
            sessionStorage.removeItem("token");
            navigate("/");
        }

        if (response.ok) {
            const dados = await response.json();
            setPessoas(dados);
        } else {
            setPessoas([]);
        }
    }

    const excluir = (id) => {
        setId(id);
        setModalIsOpen(true);
    };

    const cancelarExclusao = () => {
        setId(0);
        setModalIsOpen(false);
    };

    const confirmarExclusao = async () => {

        const url = "/api/Pessoa/" + id;

        try {
            const response = await fetch(url, {
                method: 'DELETE',
                headers: {
                    "Authorization": "Bearer " + token,
                    "Content-Type": "application/json"
                }
            });

            if (response.status === NAO_AUTORIZADO) {
                sessionStorage.removeItem("token");
                navigate("/");
            }

            if (!response.ok) {
                document.getElementById("mensagem").innerHTML = "<p>Ops... Ocorreu um problema ao excluir registro</p>";
            } else {
                setModalIsOpen(false);
            }

        } finally {
            loadPessoas(token);
            
        }

        
    };

    const contents = pessoas === undefined
        ? <p className="text-center">Carregando as pessoas aguarde...</p>
        : <div>

            <table className="table table-hover table-striped mt-4 ">
                <thead className="table-dark">
                    <tr>
                        <th className="col-md-1" scope="col">Id</th>
                        <th className="col-md" scope="col">Nome</th>
                        <th className="col-md-2" scope="col">CPF</th>
                        <th className="col-md-2" scope="col">Data Nascimento</th>
                        <th className="col-md-2" scope="col">&nbsp;</th>
                    </tr>
                </thead>

                <tbody>
                    {pessoas.length === 0 ? (
                        <tr>
                            <td colSpan="5" className="text-center">Nenhum registro encontrado</td>
                        </tr>
                    ): (
                        pessoas.map(pessoa =>
                            <tr key={pessoa.idPessoa}>
                                <td scope="row">{pessoa.idPessoa}</td>
                                <td>{pessoa.nome}</td>
                                <td>{formatarCPF(pessoa.cpf)}</td>
                                <td>{formatarData(pessoa.dataNascimento)}</td>
                                <td>
                                    <Link to={"Edit/" + pessoa.idPessoa} className="btn btn-warning mx-2">
                                        <i className="text-light bi bi-pencil-square"></i>    
                                    </Link> 

                                    <button className="btn btn-danger" onClick={() => excluir(pessoa.idPessoa)}>
                                        <i className="bi bi-trash3"></i>  
                                    </button>
                                </td>
                            </tr>
                    )
                        
                    )}
                </tbody>
            </table>
        </div>;


    return (
        <div className="container">

            <Menu />

            <h2 className="mt-4 text-center">Cadastro de Pessoas Stefanini</h2>

            <Link className="btn btn-success mt-4 " to="Create">
                <i className="bi bi-plus-circle"></i>
                <span className="mx-2">Cadastrar</span>
            </Link>

            <div className="mt-4">
                { contents }
            </div>

            <Modal className="mt-1 modal d-block modal-dialog modal-sm" isOpen={modalIsOpen} onRequestClose={cancelarExclusao} contentLabel="Confirma a Exclusão">
                <div className="modal-content">

                    <h4 className="text-center mt-1">Confirma Exclusão</h4>

                    <div id="mensagem" className="text-center text-danger my-2"></div>

                    <div className="modal-body">
                        <p>Tem certeza que deseja excluir a pessoa Id: <strong>{id}</strong>?</p>
                    </div>
                    <div className="modal-footer">
                        <button type="button" className="btn btn-success mx-2" onClick={confirmarExclusao}>
                            <i className="mx-2 bi bi-check"></i>Sim
                        </button>
                        <button type="button" className="btn btn-danger" onClick={cancelarExclusao}>
                            <i className="mx-2 bi bi-x"></i>Não
                        </button>
                    </div>
                </div>
            </Modal>

        </div>
    );
}

export default Pessoa;