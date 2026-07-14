import { useEffect, useState } from "react";
import type { Pessoa } from "../types";
import { buscarPessoas, cadastrarPessoa, removerPessoa } from "../services/pessoaService";

//criei esse componente de pessoas 

function Pessoas() {
    const [pessoas, setPessoas] = useState<Pessoa[]>([]);
    const [nome, setNome] = useState("");
    const [idade, setIdade] = useState("");
    const [erro, setErro] = useState("");

    async function carregarPessoas() {
        const dados = await buscarPessoas();
        setPessoas(dados);
    }

    useEffect(() => {
        carregarPessoas();
    }, []);

    async function handleCadastrar() {
        setErro("");
        try{
            await cadastrarPessoa(nome, Number(idade));
            setNome("");
            setIdade("");
            carregarPessoas();
    } catch (e) {
    setErro((e as Error).message);
  }
}

    async function handleRemover(id: number) {
        await removerPessoa(id);
        carregarPessoas();        
    }

    return (
        <div>
            <h2>Pessoas</h2>

            {erro && <p style={{ color: "red" }}>{erro}</p>}

            <div>
            <input
                type="text"
                placeholder="Nome"
                value={nome}
                onChange={(e) => setNome(e.target.value)}
            />
            <input
                type="number"
                placeholder="Idade"
                value={idade}
                onChange={(e) => setIdade(e.target.value)}
            />
            <button onClick={handleCadastrar}>Cadastrar</button>
            </div>

            <table>
            <thead>
                <tr>
                <th>Nome</th>
                <th>Idade</th>
                <th></th>
                </tr>
            </thead>
            <tbody>
                {pessoas.map((pessoa) => (
                <tr key={pessoa.id}>
                    <td>{pessoa.nome}</td>
                    <td>{pessoa.idade} anos</td>
                    <td>
                    <button onClick={() => handleRemover(pessoa.id)}>Remover</button>
                    </td>
                </tr>
                ))}
            </tbody>
            </table>
        </div>
    );
}

export default Pessoas;