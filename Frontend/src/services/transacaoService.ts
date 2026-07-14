import type { Transacao, TipoTransacao } from "../types";

//Endereço da API do backend, se a porta mudar, ajuste aqui
const API_URL = "https://localhost:7279/api/transacoes";

//lista todas transações
export async function buscarTransacoes(): Promise<Transacao[]> {
    const resposta = await fetch(API_URL);
    return resposta.json();
}

//essa função cadastra uma transação nova
export async function cadastrarTransacao(
    descricao: string,
    valor: number,
    tipo: TipoTransacao,
    pessoaId: number
): Promise<void> {
    const resposta = await fetch(API_URL, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ descricao, valor, tipo, pessoaId })
    })

    //Se o backend responder com texto de sucesso, não precisa ler o corpo da resposta.
    //Se der algum erro o backend devolve o JSON com a mensagem de erro.
    if (!resposta.ok) {
        const erro = await resposta.json();
        throw new Error(erro.mensagem)
    }
}