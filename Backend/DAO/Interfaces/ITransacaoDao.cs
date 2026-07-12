using ControleGastos.Models;

namespace ControleGastos.DAO.Interfaces {
    public interface ITransacaoDao {
        Task<List<Transacao>> ListarTransacaoAsync();
        Task<Transacao> CadastrarTransacaoAsync(Transacao transacao);

    }
}

//criei as interfaces a serem utilizdas no TransacaoDao, permitindo uma camada exclusiva só para o acesso dos métodos ao banco