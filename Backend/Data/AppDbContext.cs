using ControleGastos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Data {

    //Relaciona as tabelas 'Pessoas' e 'Transacoes' no banco (SQLite) através das migrations.
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }  

        public DbSet<Pessoa> Pessoas => Set<Pessoa>();
        public DbSet<Transacao> Transacoes => Set<Transacao>(); 

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            
        }
    }
}