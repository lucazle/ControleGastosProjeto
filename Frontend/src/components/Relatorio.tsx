import { useEffect, useState } from "react";
import type { RelatorioTotais } from "../types";
import { buscarRelatorio } from "../services/relatorioService";

//Componente responsável pela tela de Relatórios. Busca os dados e exibe na tela
//Busca os dados da API pelas funções do relatorioService
function Totais() {
  const [relatorio, setRelatorio] = useState<RelatorioTotais | null>(null);

  async function carregarRelatorio() {
    const dados = await buscarRelatorio();
    setRelatorio(dados);
  }

  //Busca o relatório assim que o componente é iniciado 
  useEffect(() => {
    carregarRelatorio();
  }, []);

  //Se o relatório não carregar ele exibe uma mensagem
  if (!relatorio) {
    return <p>Carregando...</p>;
  }

  return (
    <div>
      <h2>Totais</h2>
        <div>
            <button onClick={carregarRelatorio}>Atualizar</button>
            <div>
                Sempre que adicionar ou remover uma nova transação clique em atualizar.
            </div>
        </div>

      <table border={1} cellPadding={8}>
        <thead>
          <tr>
            <th>Pessoa</th>
            <th>Receitas</th>
            <th>Despesas</th>
            <th>Saldo</th>
          </tr>
        </thead>
        <tbody>
          {relatorio.pessoas.map((pessoa) => (
            <tr key={pessoa.pessoaId}>
              <td>{pessoa.nome}</td>
              <td>R$ {pessoa.totalReceitas}</td>
              <td>R$ {pessoa.totalDespesas}</td>
              <td>R$ {pessoa.saldo}</td>
            </tr>
          ))}
          <tr>
            <td><strong>Total Geral</strong></td>
            <td><strong>R$ {relatorio.totalReceitasGeral}</strong></td>
            <td><strong>R$ {relatorio.totalDespesasGeral}</strong></td>
            <td><strong>R$ {relatorio.saldoGeral}</strong></td>
          </tr>
        </tbody>
      </table>
    </div>
  );
}

export default Totais;