import { useEffect, useState } from "react";
import type { Pessoa } from "../types";
import { buscarPessoas, cadastrarPessoa, removerPessoa } from "../services/pessoaService";

//Componente responsável pela tela Pessoas, inclui o cadastro, listagem e remoção
//Busca os dados da API pelas funções do pessoaService
function Pessoas() {
    const [pessoas, setPessoas] = useState<Pessoa[]>([]);
    const [nome, setNome] = useState("");
    const [idade, setIdade] = useState("");
    const [erro, setErro] = useState("");

    async function carregarPessoas() {
        const dados = await buscarPessoas();
        setPessoas(dados);
    }

    //Busca a lista de pessoas assim que o componente é executado 
    useEffect(() => {
        carregarPessoas();
    }, []);

    async function handleCadastrar() {
        setErro("");
        try{
            await cadastrarPessoa(nome, Number(idade));
            //Limpa o formulário e busca a lista atualizada
            setNome("");
            setIdade("");
            carregarPessoas();
    } catch (e) {
        //Erro de validação vindo do backend
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