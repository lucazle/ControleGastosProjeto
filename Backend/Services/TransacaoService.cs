using ControleGastos.DAO.Interfaces;
using ControleGastos.Services.Interfaces;
using ControleGastos.DTO.Transacoes;
using ControleGastos.Models;

namespace ControleGastos.Services {
    public class TransacaoService : ITransacaoService {
        private readonly ITransacaoDao _transacaoDao;
        private readonly IPessoaDao _pessoaDao;

        public TransacaoService (ITransacaoDao transacaoDao, IPessoaDao pessoaDao) {
            _transacaoDao = transacaoDao;
            _pessoaDao = pessoaDao;
        }

        public async Task<List<ResponseTransacaoDto>> ListarTransacoesAsync() {

            var transacoes = await _transacaoDao.ListarTransacaoAsync();

            return transacoes
                .Select(t => new ResponseTransacaoDto {
                    Id = t.Id,
                    Descricao = t.Descricao,
                    Valor = t.Valor,
                    Tipo = t.Tipo,
                    PessoaId = t.PessoaId,
                    PessoaNome = t.Pessoa!.Nome
                })
                .ToList();
        }

        public async Task <ResponseTransacaoDto> CadastrarTransacaoAsync(RequestTransacaoDto dto) {

            if (dto.Descricao == "")
                throw new Exception("A descrição é obrigatória.");

            if (dto.Valor <= 0)
                throw new Exception("O valor deve ser maior que zero.");

            var pessoa = await _pessoaDao.BuscarPessoaPorIdAsync(dto.PessoaId);
            if (pessoa == null)
                throw new Exception("Pessoa não encotrada.");

            if (pessoa.Idade < 18 && dto.Tipo == TipoTransacao.Receita)
                throw new Exception("Pessoas menores de 18 anos só podem ter transações do tipo despesa."); //Regra de negócios solicitada no desafio

            var transacao = new Transacao {
                Descricao = dto.Descricao,
                Valor = dto.Valor,
                Tipo = dto.Tipo,
                PessoaId = dto.PessoaId
            };

            await _transacaoDao.CadastrarTransacaoAsync(transacao);

            return new ResponseTransacaoDto {
                Id = transacao.Id,
                Descricao = transacao.Descricao,
                Valor = transacao.Valor,
                Tipo = transacao.Tipo,
                PessoaId = transacao.PessoaId,
                PessoaNome = pessoa.Nome
            };
        }
    }
}

//TransacaoService também com regra de negócios aplicada, esse arquivo faz basicamente a estrutura para que os dados sejam solicitados corretamente
//ele também aplica as regras de negócio, lança erros a serem usados no controller e exibidos para os usuários