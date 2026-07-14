import type { Pessoa } from "../types";

//Endereço da API do backend, se a porta mudar, ajuste aqui
const API_URL = "https://localhost:7279/api/pessoas";

//Busca a lista de todas pessoas cadastradas
export async function buscarPessoas(): Promise<Pessoa[]> {
    const resposta = await fetch(API_URL);
    return resposta.json();
}

//Cadastra uma nova pessoa
export async function cadastrarPessoa(nome:string, idade: number): Promise<void> {
    const resposta =await fetch(API_URL, {
        method: "POST",
        headers: {"Content-Type": "application/json"},
        body: JSON.stringify({nome, idade}),
    })

    //Se o backend responder com texto de sucesso, não precisa ler o corpo da resposta.
    //Se der algum erro o backend devolve o JSON com a mensagem de erro.
    if (!resposta.ok) {
        const erro = await resposta.json();
        throw new Error(erro.mensagem)
    }
}

//Remove uma pessoa pelo ID. As transações dessa pessoa também são deletadas.
export async function removerPessoa(id: number): Promise<void> {
    await fetch(`${API_URL}/${id}`, {method: "DELETE"});
}

