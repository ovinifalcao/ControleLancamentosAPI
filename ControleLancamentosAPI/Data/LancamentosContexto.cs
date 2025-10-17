using ControleLancamentosAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleLancamentosAPI.Data;

public class LancamentosContexto : DbContext
{
    public LancamentosContexto(DbContextOptions<LancamentosContexto> opcoes) : base(opcoes) { }

    public DbSet<LancamentoFinanceiro> Lancamento { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
        .HasSequence<int>("SeqNumeroLancamento")
        .StartsAt(10000000)
        .IncrementsBy(1);

        modelBuilder.Entity<LancamentoFinanceiro>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.NumeroLancamento)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("nextval('\"SeqNumeroLancamento\"')");
        });
    }
}

