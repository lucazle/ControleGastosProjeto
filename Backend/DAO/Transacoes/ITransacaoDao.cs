using ControleGastos.Models;

namespace ControleGastos.DAO.Transacoes {
    public interface ITransacaoDao {
        Task<List<Transacao>> ListarTransacaoAsync();
        Task<Transacao> CadastrarTransacaoAsync(Transacao transacao);

    }
}
