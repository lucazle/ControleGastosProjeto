import { useEffect, useState } from "react";
import type { Pessoa, Transacao, TipoTransacao } from "../types";
import { buscarTransacoes, cadastrarTransacao } from "../services/transacaoService";
import { buscarPessoas } from "../services/pessoaService";

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

    useEffect(() => {
        carregarDados();
    }, []);

    async function handleCadastrar() {
        setErro("");
        try {
            await cadastrarTransacao(descricao, Number(valor), tipo, Number(pessoaId));
            setDescricao("");
            setValor("");
            setTipo("Despesa");
            setPessoaId("");
            carregarDados();
        } catch (e) {
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

            <button onClick={handleCadastrar}>Cadastrar</button>
        </div>

        <ul>
            {transacoes.map((transacao) => (
                <li key={transacao.id}>
                    {transacao.descricao} - R$ {transacao.valor} - {transacao.tipo} - {transacao.pessoaNome}
                </li>
            ))}
        </ul>
    </div>
)}

export default Transacoes;