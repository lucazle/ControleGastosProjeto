using ControleGastos.DAO.Interfaces;
using ControleGastos.Services.Interfaces;
using ControleGastos.DTO.Pessoas;
using ControleGastos.Models;

namespace ControleGastos.Services {

    //Camada responsável pelas regras de negócio de Pessoa. O construtor é responsável pelo acesso ao banco
    //através de Injeção de Dependência. Ele só valida os dados e converte entre a entidade Pessoa e os
    //DTOs que entram e saem da API.
    public class PessoaService : IPessoaService {
        private readonly IPessoaDao _pessoaDao;

        public PessoaService(IPessoaDao pessoaDao) {
            _pessoaDao = pessoaDao;
        }

        public async Task<List<ResponsePessoaDto>> ListarPessoasAsync() {

            var pessoas = await _pessoaDao.ListarPessoaAsync();

            //Converte cada entidade Pessoa para ResponsePessoaDto, que é o formato exposto pela API.
            return pessoas
                .Select(p => new ResponsePessoaDto {
                    Id = p.Id,
                    Nome = p.Nome,
                    Idade = p.Idade
                })
                .ToList();
        }

        public async Task<ResponsePessoaDto> CadastrarPessoaAsync(RequestPessoaDto dto) {

            //Validação de negócio, o nome é obrigatório para o cadastro
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

            //Busca a pessoa antes de excluir, caso não ache retorna erro
            //o AppDbContext cuida de excluir todas transações junto a pessoa.
            var pessoa = await _pessoaDao.BuscarPessoaPorIdAsync(id);
            if (pessoa == null)
                throw new Exception("Pessoa não encontrada.");

            await _pessoaDao.RemoverPessoaAsync(pessoa);
        }
    }
}