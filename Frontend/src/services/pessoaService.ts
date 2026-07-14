import type { Pessoa } from "../types";

const API_URL = "https://localhost:7279/api/pessoas";

export async function buscarPessoas(): Promise<Pessoa[]> {
    const resposta = await fetch(API_URL);
    return resposta.json();
}

export async function cadastrarPessoa(nome:string, idade: number): Promise<void> {
    const resposta =await fetch(API_URL, {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({nome, idade}),
    })
    if (!resposta.ok) {
        const erro = await resposta.json();
        throw new Error(erro.mensagem)
    }
}
export async function removerPessoa(id: number): Promise<void> {
    await fetch(`${API_URL}/${id}`, {method: "DELETE"});
}

