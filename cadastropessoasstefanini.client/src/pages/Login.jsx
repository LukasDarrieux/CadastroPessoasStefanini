import { useState } from "react";
import { useNavigate, Link } from "react-router-dom";

function Login() {

    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");

    function handleEmail(e) {
        setEmail(e.target.value);
    }

    function handleSenha(e) {
        setSenha(e.target.value);
    }

    const navigate = useNavigate();

    const login = async (event) => {
        event.preventDefault();

        document.getElementById("mensagem").innerHTML = "";
        const url = "/api/Usuario/Login";

        const login = {
            email: email,
            senha: senha
        };

        try {
            const request = await fetch(url, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(login)
            });

            var response = await request;
            if (response.ok) {
                var tokenDTO = await response.json();
                sessionStorage.setItem("token", tokenDTO.token);
                navigate("/Pessoa");

            } else {
                document.getElementById("mensagem").innerHTML = "<p>Usuário Inválido</p>";
            }

        }
        catch (erro) {
            console.error("Erro ao efetuar o login: ", error);
        }
    }

    return (
        <div className="d-flex flex-column min-vh-100 overflow-y-hidden">
            <main className="container d-flex flex-grow-1 justify-content-center align-items-center">
                <div className="card p-4 shadow w-100" style={{ maxWidth: "400px" }}>

                    <div className="d-flex justify-content-center mb-4 text-center">
                        <h1>Login</h1>
                    </div>

                    <div className="text-danger text-center" id="mensagem"></div>

                    <form method="POST" action="/">
                        <div className="mb-3">
                            <label htmlFor="email" className="form-label">E-mail</label>
                            <input type="email" className="form-control" placeholder="exemplo@email.com" value={email} onChange={handleEmail} />
                        </div>

                        <div className="mb-3">
                            <label htmlFor="senha" className="form-label">Senha</label>
                            <input type="password" className="form-control" placeholder="********" value={senha} onChange={handleSenha} />
                        </div>

                        <div className="d-flex justify-content-center">
                            <button type="button" className="btn btn-primary w-50" onClick={login}>
                                <span>Login</span>
                            </button>
                        </div>

                        <div className="mt-4 text-center">
                            <Link to="NovoUsuario">Cadastre-se</Link>
                        </div>
                    </form>

                </div>
            </main>
        </div>
    );
}

export default Login;
