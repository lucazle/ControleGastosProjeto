import type { Transacao, TipoTransacao } from "../types";
import { buscarPessoas } from "./pessoaService";

const API_URL = "http://localhost:5256/api/transacoes";

//essa função vai listar todas as transações trazendo elas do back
export async function buscarTransacoes(): Promise<Transacao[]> {
    const resposta = await fetch(API_URL);
    return resposta.json();
}

//essa função cadastra uma transação nova, inclui a mensagem de erro para caso o usuario seja menor de 18 anos
export async function cadastrarTransacao(
    descricao: string,
    valor: number,
    tipo: TipoTransacao,
    pessoaId: number
): Promise<Transacao> {
    const resposta = await fetch(API_URL, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ descricao, valor, tipo, pessoaId })
    })

    if (!resposta.ok) {
        const erro = await resposta.json();
        throw new Error(erro.mensagem)
    }

    return resposta.json();
}