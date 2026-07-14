using ControleGastos.DAO.Interfaces;
using ControleGastos.Services.Interfaces;
using ControleGastos.DTO.Transacoes;
using ControleGastos.Models;

namespace ControleGastos.Services {

    //Camada responsável pelas regras de negócio de transação. O construtor é responsável pelo acesso ao banco
    //através de Injeção de Dependência. Injeta IPessoaDao e ITransacaoDao pois é preciso trazer as informações de cada pessoa
    //e sua respectiva transação. Ele valida os dados e converte entre a entidade Transacao, TipoTransacao
    //e os DTOs que entram e saem da API. 
    public class TransacaoService : ITransacaoService {
        private readonly ITransacaoDao _transacaoDao;
        private readonly IPessoaDao _pessoaDao;

        public TransacaoService (ITransacaoDao transacaoDao, IPessoaDao pessoaDao) {
            _transacaoDao = transacaoDao;
            _pessoaDao = pessoaDao;
        }

        public async Task<List<ResponseTransacaoDto>> ListarTransacoesAsync() {

            var transacoes = await _transacaoDao.ListarTransacaoAsync();

            //Converte cada entidade Transaca para ResponseTransacaoDto, que é o formato exposto pela API.
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

            //Validação de negócio, a descrição não pode ser vazio
            if (dto.Descricao == "")
                throw new Exception("A descrição é obrigatória.");

            //Validação de negócio, o valor não pode ser menor ou igual a zero
            if (dto.Valor <= 0)
                throw new Exception("O valor deve ser maior que zero.");

            //Validação de negócio, busca a pessoa antes de relacionar uma transação a ela
            var pessoa = await _pessoaDao.BuscarPessoaPorIdAsync(dto.PessoaId);
            if (pessoa == null)
                throw new Exception("Pessoa não encontrada.");

            //Validação de negócio, limitação para pessoas menor de 18 anos
            if (pessoa.Idade < 18 && dto.Tipo == TipoTransacao.Receita)
                throw new Exception("Pessoas menores de 18 anos só podem ter transações do tipo despesa."); 

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