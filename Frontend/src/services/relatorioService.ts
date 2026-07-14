import type { RelatorioTotais } from "../types";

const API_URL = "https://localhost:7279/api/relatorio";

export async function buscarRelatorio(): Promise<RelatorioTotais> {
    const resposta = await fetch(API_URL);
    return resposta.json();
}