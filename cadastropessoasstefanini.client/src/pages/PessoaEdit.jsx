import { formatarCPF, formatarDataAAAAMMDD } from "../Util";
import { React, useEffect, useState } from 'react'
import { Link, useNavigate, useParams } from "react-router-dom";
import Menu from "../components/Menu";

function PessoaEdit() {

    const NAO_AUTORIZADO = 401;

    const navigate = useNavigate();
    const [token, setToken] = useState("");

    const { id } = useParams();
    const [nome, setNome] = useState("");
    const [cpf, setCpf] = useState("");
    const [dataNascimento, setDataNascimento] = useState("");
    const [sexo, setSexo] = useState(0);
    const [naturalidade, setNaturalidade] = useState("");
    const [nacionalidade, setNacionalidade] = useState("");
    const [email, setEmail] = useState("");

    const handleNomeChange = (event) => {
        setNome(event.target.value);
    }

    const handleCpfChange = (event) => {
        const TAMANHO_CPF = 11;
        if (event.target.value.length == TAMANHO_CPF)
            setCpf(formatarCPF(event.target.value))
        else
            setCpf(event.target.value);
    }

    const handleDataNascimentoChange = (event) => {
        setDataNascimento(event.target.value);
    }

    const handleSexoChange = (event) => {
        setSexo(event.target.value);
    }

    const handleNaturalidadeChange = (event) => {
        setNaturalidade(event.target.value);
    }

    const handleNacionalidadeChange = (event) => {
        setNacionalidade(event.target.value);
    }

    const handleEmailChange = (event) => {
        setEmail(event.target.value);
    }

    useEffect(() => {
        const tokenSessao = sessionStorage.getItem("token");
        if (!tokenSessao) {
            navigate("/"); // Redireciona para login
        }
        setToken(tokenSessao);
        loadPessoa(tokenSessao);
    }, []);

    async function loadPessoa(tokenSessao) {
        const url = "/api/pessoa/" + parseInt(id);

        document.getElementById("mensagem").innerHTML = "";

        const response = await fetch(url, {
            method: "GET",
            headers: {
                "Authorization": "Bearer " + tokenSessao,
                "Content-Type": "application/json",
            }
        });

        if (response.status === NAO_AUTORIZADO) {
            sessionStorage.removeItem("token");
            navigate("/");
        }

        if (response.ok) {

            const pessoa = await response.json();

            setNome(pessoa.nome);
            setCpf(formatarCPF(pessoa.cpf));
            setDataNascimento(formatarDataAAAAMMDD(pessoa.dataNascimento));
            setSexo(pessoa.sexo);
            setNaturalidade(pessoa.naturalidade);
            setNacionalidade(pessoa.nacionalidade);
            setEmail(pessoa.email);

        } else {
            document.getElementById("mensagem").innerHTML = "Ops... Não foi possível carregar o registro";
            await delay(3000) //5 segundos 
            navigate("/Pessoa");
        }

    }

    function delay(tempo) {
        return new Promise(resolve => setTimeout(resolve, tempo));
    }

    const salvar = async (event) => {
        event.preventDefault();

        document.getElementById("mensagem").innerHTML = "";
        const url = "/api/Pessoa/" + id;

        if (cpf === undefined || cpf === "") {
            document.getElementById("mensagem").innerHTML = "Informe o CPF";
            return;
        }

        if (dataNascimento === undefined || dataNascimento === "") {
            document.getElementById("mensagem").innerHTML = "Informe a Data de Nascimento";
            return;
        }

        if (sexo === "" || sexo == undefined)
            setSexo(0);

        const pessoa = {
            nome: nome,
            cpf: cpf,
            dataNascimento: dataNascimento,
            sexo: parseInt(sexo),
            naturalidade: naturalidade,
            nacionalidade: nacionalidade,
            email: email
        };

        try {
            const request = await fetch(url, {
                method: "PUT",
                headers: {
                    "Authorization": "Bearer " + token,
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(pessoa)
            });

            var response = await request;

            if (response.status === NAO_AUTORIZADO) {
                sessionStorage.removeItem("token");
                navigate("/");
            }

            if (response.ok) {

                navigate("/Pessoa");

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
                        document.getElementById("mensagem").innerHTML = "Ops... Não foi possível efetuar a atualização";
                    }
                }
                else
                    document.getElementById("mensagem").innerHTML = "Ops... Não foi possível efetuar a atualização";
            }

        }
        catch (erro) {
            console.error("Erro ao cadastrar pessoa: ", error);
        }
    }

    return (
        <div className="d-flex flex-column min-vh-100 overflow-y-hidden">
            <Menu />

            <main className="container d-flex flex-grow-1 justify-content-center align-items-center">
                <div className="card p-4 shadow w-100" style={{ maxWidth: "800px" }}>
                    <h2 className="text-center mb-4">Editar Pessoa</h2>
                    <form>

                        <div id="mensagem" className="text-danger text-center my-2"></div>

                        <div className="row mb-3">

                            <div className="col-md-6">
                                <label className="form-label">Nome</label>
                                <input type="text" className="form-control" placeholder="Nome" value={nome} onChange={handleNomeChange} required />
                            </div>

                            <div className="col-md-6">
                                <label className="form-label">CPF</label>
                                <input type="text" className="form-control" placeholder="000.000.000-00" value={cpf} onChange={handleCpfChange} required />
                            </div>

                        </div>


                        <div className="row mb-3">

                            <div className="col-md-6">
                                <label className="form-label">Data Nascimento</label>
                                <input type="date" className="form-control" value={dataNascimento} onChange={handleDataNascimentoChange} required />
                            </div>

                            <div className="col-md-6">
                                <label className="form-label">Sexo</label>
                                <select className="form-select" value={sexo} onChange={handleSexoChange}>
                                    <option value="0">Selecione...</option>
                                    <option value="1">Masculino</option>
                                    <option value="2">Feminino</option>
                                </select>
                            </div>

                        </div>

                        <div className="row mb-3">

                            <div className="col-md-6">
                                <label className="form-label">Naturalidade</label>
                                <input type="text" className="form-control" placeholder="Naturalidade" value={naturalidade} onChange={handleNaturalidadeChange} />
                            </div>

                            <div className="col-md-6">
                                <label className="form-label">Nacionalidade</label>
                                <input type="text" className="form-control" placeholder="Nacionalidade" value={nacionalidade} onChange={handleNacionalidadeChange} />
                            </div>

                        </div>


                        <div className="row mb-4">

                            <div className="col-md-12">
                                <label className="form-label">E-mail</label>
                                <input type="email" className="form-control" placeholder="exemplo@email.com" value={email} onChange={handleEmailChange} />
                            </div>

                        </div>


                        <div className="d-flex justify-content-center">
                            <button type="submit" className="btn btn-primary mx-2" onClick={salvar}>
                                <i className="bi bi-floppy"></i>
                                <span className="mx-2">Salvar</span>
                            </button>
                            <Link className="btn btn-danger" to="/Pessoa">
                                <i className="bi bi-x-circle"></i>
                                <span className="mx-2">Cancelar</span>
                            </Link>

                        </div>

                    </form>
                </div>
            </main>
        </div>
    );
}

export default PessoaEdit;