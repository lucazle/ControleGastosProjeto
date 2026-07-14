import type { RelatorioTotais } from "../types";

//Endereço da API do backend, se a porta mudar, ajuste aqui
const API_URL = "https://localhost:7279/api/relatorio";

//Busca o relatorio total de pessoas e total geral.
export async function buscarRelatorio(): Promise<RelatorioTotais> {
    const resposta = await fetch(API_URL);
    return resposta.json();
}