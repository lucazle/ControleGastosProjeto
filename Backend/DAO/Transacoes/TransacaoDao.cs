using ControleGastos.Data;
using ControleGastos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.DAO.Transacoes {
    public class TransacaoDao : ITransacaoDao {

        private readonly AppDbContext _context;

        public TransacaoDao(AppDbContext context) {
            _context = context;
        }

        public async Task<List<Transacao>> ListarTransacaoAsync() {
            return await _context.Transacoes
                .Include(t => t.Pessoa)
                .ToListAsync();
        } 

        public async Task<Transacao> CadastrarTransacaoAsync(Transacao transacao) {
            _context.Transacoes.Add(transacao);
            await _context.SaveChangesAsync();
            return transacao;
        }
    }
}
