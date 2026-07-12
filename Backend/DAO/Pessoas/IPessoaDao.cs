using ControleGastos.Models;

namespace ControleGastos.DAO.Pessoas {
    public interface IPessoaDao {
        Task<List<Pessoa>> ListarPessoaAsync();
        Task<Pessoa?> BuscarPessoaPorIdAsync(int id);
        Task<Pessoa> CadastrarPessoaAsync(Pessoa pessoa);
        Task<bool> RemoverPessoaAsync(Pessoa pessoa);
    }
}