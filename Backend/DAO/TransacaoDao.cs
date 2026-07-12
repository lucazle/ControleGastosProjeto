using ControleGastos.DAO.Interfaces;
using ControleGastos.Data;
using ControleGastos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.DAO {
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

// Aqui eu to herdando a interface que criei e criando os metódos que fazem as consultas, add, delete do banco.
//Usei o padrão de projeto DAO para organizar melhor em camadas o projeto, deixando cada camada mais específica para o que se propõe a fazer.