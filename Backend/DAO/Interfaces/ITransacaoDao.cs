using ControleGastos.Models;

namespace ControleGastos.DAO.Interfaces {
    public interface ITransacaoDao {
        Task<List<Transacao>> ListarTransacaoAsync();
        Task<Transacao> CadastrarTransacaoAsync(Transacao transacao);

    }
}