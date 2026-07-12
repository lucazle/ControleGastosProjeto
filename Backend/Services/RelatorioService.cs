using ControleGastos.DAO.Interfaces;
using ControleGastos.DTO.Relatorios;
using ControleGastos.Models;
using ControleGastos.Services.Interfaces;

namespace ControleGastos.Services {
    public class RelatorioService : IRelatorioService {
        private readonly IPessoaDao _pessoaDao;
        //Injeção de dependência para trazer os dados de cada pessoa do pessoaDao que já tem todas informações que preciso para gerar o relatório
        public RelatorioService(IPessoaDao pessoaDao) {              
            _pessoaDao = pessoaDao;
        }

    public async Task<RelatorioTotaisDto> GerarRelatorioAsync() {

            //aqui eu chamo todas pessoas (ja com suas transações como deixei no PessoaDao)
            var pessoas = await _pessoaDao.ListarPessoaAsync();         

            //para aqui ele vai fazer o total de cada pessoa
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

            //aqui eu retorno o total de todas pessoas que estão listadas
            return new RelatorioTotaisDto {
                Pessoas = totaisPorPessoa,
                TotalReceitasGeral = totaisPorPessoa.Sum(p => p.TotalReceitas),
                TotalDespesasGeral = totaisPorPessoa.Sum(p => p.TotalDespesas)
            };
        }
    }
}
