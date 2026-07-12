using ControleGastos.DAO.Interfaces;
using ControleGastos.Data;
using ControleGastos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.DAO {
    public class PessoaDao : IPessoaDao {

        private readonly AppDbContext _context;          

        public PessoaDao(AppDbContext context) {       //injecão de dependência para conexão com o banco, sem precisa ficar usando 
            _context = context;                        //new AppDbContext todas vezes, isso deixa o código mais limpo e é uma boa prática de desenvolvimento
        }

        public async Task<List<Pessoa>> ListarPessoaAsync() {        //Utilizei esse listar para o relatório de pessoas, incluindo as transações de cada uma
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


//Aqui eu to herdando a interface que criei e criando os metódos que fazem as consultas, add, delete do banco.
//Usei o padrão de projeto DAO para organizar melhor em camadas o projeto, deixando cada camada mais específica para o que se propõe a fazer.