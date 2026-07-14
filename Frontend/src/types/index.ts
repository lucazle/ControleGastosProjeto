//Replica  tudo que eu criei no DTO do backend, aqui estou fazendo em 
//ts para o front saber o formato dos dados que vai receber.

export interface Pessoa {
    id: number;
    nome: string;
    idade: number;
}

export type TipoTransacao = "Despesa" | "Receita";

export interface Transacao {
    id: number;
    descricao: string;
    valor: number;
    tipo: TipoTransacao;
    pessoaId: number;
    pessoaNome: string;
}

export interface TotalPessoa {
    pessoaId: number;
    nome: string;
    totalReceitas: number;
    totalDespesas: number;
    saldo: number;
}

export interface RelatorioTotais{
    pessoas: TotalPessoa[];
    totalReceitasGeral: number;
    totalDespesasGeral: number;
    saldoGeral: number;
}