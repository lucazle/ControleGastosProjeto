using ControleGastos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }  

        public DbSet<Pessoa> Pessoas => Set<Pessoa>();
        public DbSet<Transacao> Transacoes => Set<Transacao>(); 

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Transacao>()
                .HasOne(t => t.Pessoa)
                .WithMany(p => p.Transacoes)
                .HasForeignKey(t => t.PessoaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

//O AppDbContext faz a "tradução" das minhas classes em SQL para que o SQLite consiga registrar tudo no banco sem precisar digitar nada
//em sql puro. As linhas 8 e 9 criam as tabelas a partir das entidaddes que eu criei. A linha 11 vai montar o banco a partir das entidades 
// que criei também, a chave estrangeira, o delete em cascata, como é solicitado e a dependência 1 para muito.
