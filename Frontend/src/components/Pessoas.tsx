import { useEffect, useState } from "react";
import type { Pessoa } from "../types";
import { buscarPessoas, cadastrarPessoa, removerPessoa } from "../services/pessoaService";

//criei esse componente de pessoas 

function Pessoas() {
    const [pessoas, setPessoas] = useState<Pessoa[]>([]);
    const [nome, setNome] = useState("");
    const [idade, setIdade] = useState("");

    async function carregarPessoas() {
        const dados = await buscarPessoas();
        setPessoas(dados);
    }

    useEffect(() => {
        carregarPessoas();
    }, []);

    async function handleCadastrar() {
    await cadastrarPessoa(nome, Number(idade));
    setNome("");
    setIdade("");
    carregarPessoas();
  }

    async function handleRemover(id: number) {
        await removerPessoa(id);
        carregarPessoas();        
    }

    return (
        <div>
            <h2>Pessoas</h2>

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

            <ul>
                {pessoas.map((pessoa) => (
                    <li key={pessoa.id}>
                        {pessoa.nome} - {pessoa.idade} anos
                        <button onClick={() => handleRemover(pessoa.id)}>Remover</button>
                    </li>
                ))}
            </ul>
        </div>
    )
}

export default Pessoas;