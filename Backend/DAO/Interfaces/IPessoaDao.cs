using ControleGastos.Models;

namespace ControleGastos.DAO.Interfaces {
    public interface IPessoaDao {
        Task<List<Pessoa>> ListarPessoaAsync();
        Task<Pessoa?> BuscarPessoaPorIdAsync(int id);
        Task<Pessoa> CadastrarPessoaAsync(Pessoa pessoa);
        Task RemoverPessoaAsync(Pessoa pessoa);
    }
}