using Microsoft.EntityFrameworkCore;
using Emprestimos.Models;

namespace Emprestimos.Infra
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Emprestimo> Emprestimos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Emprestimo>().HasKey(e => e.Id);
            // Adicione mais configurações aqui se precisar

            base.OnModelCreating(modelBuilder);
        }
    }
}
