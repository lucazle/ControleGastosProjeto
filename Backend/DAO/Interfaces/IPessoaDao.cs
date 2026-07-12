using ControleGastos.Models;

namespace ControleGastos.DAO.Interfaces {
    public interface IPessoaDao {
        Task<List<Pessoa>> ListarPessoaAsync();
        Task<Pessoa?> BuscarPessoaPorIdAsync(int id);
        Task<Pessoa> CadastrarPessoaAsync(Pessoa pessoa);
        Task RemoverPessoaAsync(Pessoa pessoa);
    }
}

//criei as interfaces a serem utilizdas no PessoaDao, permitindo uma camada exclusiva só para o acesso dos métodos ao banco