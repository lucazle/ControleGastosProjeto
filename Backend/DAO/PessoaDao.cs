using ControleGastos.DAO.Interfaces;
using ControleGastos.Data;
using ControleGastos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.DAO {
    public class PessoaDao : IPessoaDao {

        private readonly AppDbContext _context;          

        public PessoaDao(AppDbContext context) {        
            _context = context;                      
        }

        public async Task<List<Pessoa>> ListarPessoaAsync() {       
            return await _context.Pessoas 
                .Include(p => p.Transacoes)
                .ToListAsync();
        }

        public async Task<Pessoa?> BuscarPessoaPorIdAsync(int id) {
            return await _context.Pessoas.FindAsync(id);
        }

        public async Task<Pessoa> CadastrarPessoaAsync(Pessoa pessoa) {
            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();
            return pessoa;
        }

        public async Task RemoverPessoaAsync(Pessoa pessoa) {
            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();
        }
    }
}