using ControleGastos.DAO.Interfaces;
using ControleGastos.DTO.Relatorios;
using ControleGastos.Models;
using ControleGastos.Services.Interfaces;

namespace ControleGastos.Services {

    //Camada responsável pelas regras de negócio do Relatório. O construtor é responsável pelo acesso ao banco
    //através de Injeção de Dependência. Só injeta o IPessoaDao por que essa tabela já está vinculada a tabela de transações.
    //Ele exibe os dados de cada pessoa somando o total de receitas, despesas e saldo.
    public class RelatorioService : IRelatorioService {

        private readonly IPessoaDao _pessoaDao;
        public RelatorioService(IPessoaDao pessoaDao) {
            _pessoaDao = pessoaDao;
        }

        public async Task<RelatorioTotaisDto> GerarRelatorioAsync() {

            var pessoas = await _pessoaDao.ListarPessoaAsync();

            //Essa variável calcula e mostra o total de despesas e receitas de cada pessoa da lista.
            //Chama pessoa por pessoa e faz a soma dos dados. O saldo ja é calculado direto no TotalPessoaDto.
            var totaisPorPessoa = pessoas
                .Select(p => new TotalPessoaDto {
                    PessoaId = p.Id,
                    Nome = p.Nome,
                    TotalReceitas = p.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Receita)
                    .Sum(t => t.Valor),
                    TotalDespesas = p.Transacoes
                    .Where(t => t.Tipo == TipoTransacao.Despesa)
                    .Sum(t => t.Valor)
                })
            .ToList();

            //Retorna o relatório total de todas pessoas.
            return new RelatorioTotaisDto {
                Pessoas = totaisPorPessoa,
                TotalReceitasGeral = totaisPorPessoa.Sum(p => p.TotalReceitas),
                TotalDespesasGeral = totaisPorPessoa.Sum(p => p.TotalDespesas)
            };
        }
    }
}
