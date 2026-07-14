import { useEffect, useState } from "react";
import type { Pessoa, Transacao, TipoTransacao } from "../types";
import { buscarTransacoes, cadastrarTransacao } from "../services/transacaoService";
import { buscarPessoas } from "../services/pessoaService";

//Componente responsável pela tela Transações, inclui o cadastro e listagem
//Busca os dados da API pelas funções do transacaoService
function Transacoes() {
    const [transacoes, setTransacoes ] = useState<Transacao[]>([]);
    const [pessoas, setPessoas] = useState<Pessoa[]>([]);

    const [descricao, setDescricao] = useState("");
    const [valor, setValor] = useState("");
    const [tipo, setTipo] = useState<TipoTransacao>("Despesa");
    const [pessoaId, setPessoaId] = useState("");
    const [erro, setErro] = useState("");

    async function carregarDados() {
        const listarTransacoes = await buscarTransacoes();
        const listarPessoas = await buscarPessoas();
        setTransacoes(listarTransacoes);
        setPessoas(listarPessoas);        
    }

    //Busca a lista de pessoas assim que o componente é executado 
    useEffect(() => {
        carregarDados();
    }, []);

    async function handleCadastrar() {
        setErro("");
        try {
            await cadastrarTransacao(descricao, Number(valor), tipo, Number(pessoaId));
            //Limpa o formulário e busca a lista atualizada
            setDescricao("");
            setValor("");
            setTipo("Despesa");
            setPessoaId("");
            carregarDados();
        } catch (e) {
            //Erro de validação vindo do backend
            setErro((e as Error).message);
        }
    }

    return (
        <div>
            <h2>Transações</h2>

            {erro && <p style={{ color: "red" }}>{erro}</p>}

            <div>
            <input
                type="text"
                placeholder="Descrição"
                value={descricao}
                onChange={(e) => setDescricao(e.target.value)}
            />
            <input
                type="number"
                placeholder="Valor"
                value={valor}
                onChange={(e) => setValor(e.target.value)}
            />

            <select value={tipo} onChange={(e) => setTipo(e.target.value as TipoTransacao)}>
                <option value="0">Despesa</option>
                <option value="1">Receita</option>
            </select>

            <select value={pessoaId} onChange={(e) => setPessoaId(e.target.value)}>
                <option value="">Selecione uma pessoa</option>
                {pessoas.map((pessoa) => (
                <option key={pessoa.id} value={pessoa.id}>
                    {pessoa.nome}
                </option>
                ))}
            </select>

            <div>
                <button onClick={carregarDados}>Atualizar</button>
                <button onClick={handleCadastrar}>Cadastrar</button>
                <div>
                    Sempre que adicionar ou remover uma nova pessoa clique em atualizar.
                </div>
            </div>
            </div>

            <table>
            <thead>
                <tr>
                <th>Descrição</th>
                <th>Valor</th>
                <th>Tipo</th>
                <th>Pessoa</th>
                </tr>
            </thead>
            <tbody>
                {transacoes.map((transacao) => (
                <tr key={transacao.id}>
                    <td>{transacao.descricao}</td>
                    <td>R$ {transacao.valor}</td>
                    <td>{transacao.tipo}</td>
                    <td>{transacao.pessoaNome}</td>
                </tr>
                ))}
            </tbody>
            </table>
        </div>
)};

export default Transacoes;