using ControleGastos.DAO.Interfaces;
using ControleGastos.Services.Interfaces;
using ControleGastos.DTO.Pessoas;
using ControleGastos.Models;

namespace ControleGastos.Services {
    public class PessoaService : IPessoaService {
        private readonly IPessoaDao _pessoaDao;

        public PessoaService(IPessoaDao pessoaDao) {
            _pessoaDao = pessoaDao;
        }

        public async Task<List<ResponsePessoaDto>> ListarPessoasAsync() {

            var pessoas = await _pessoaDao.ListarPessoaAsync();

            return pessoas
                .Select(p => new ResponsePessoaDto {
                    Id = p.Id,
                    Nome = p.Nome,
                    Idade = p.Idade
                })
                .ToList();
        }

        public async Task<ResponsePessoaDto> CadastrarPessoaAsync(RequestPessoaDto dto) {

            if (dto.Nome == "")
                throw new Exception("O nome é obrigatório.");

            var pessoa = new Pessoa {
                Nome = dto.Nome,
                Idade = dto.Idade
            };
            await _pessoaDao.CadastrarPessoaAsync(pessoa);

            return new ResponsePessoaDto {
                Id = pessoa.Id,
                Nome = pessoa.Nome,
                Idade = pessoa.Idade
            };
        }

        public async Task RemoverPessoaAsync (int id) {

            var pessoa = await _pessoaDao.BuscarPessoaPorIdAsync(id);
            if (pessoa == null)
                throw new Exception("Pessoa não encontrada.");

            await _pessoaDao.RemoverPessoaAsync(pessoa);
        }
    }
}


//Aqui eu apliquei as regras de negócio que foram solicitadas no desafio, criei somente os métodos solicitados, herdando a interface
//e fazendo a injeção de dependência da IPessoaDao.