using LojaAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LojaAPI.Context;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<Vendedor>? Vendedores { get; set; }
    public DbSet<Produto>? Produtos { get; set; }
    
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Vendedor>(entity =>
        {
            entity.Property(v => v.Nome)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(v => v.Sobrenome)
                 .IsRequired()
                 .HasMaxLength(50);
        });

        base.OnModelCreating(modelBuilder); 
        modelBuilder.Entity<Produto>(entity => 
        {
            entity.HasOne(p => p.Vendedor)
                   .WithMany(v => v.Produtos)
                   .HasForeignKey(p => p.IdVendedor)
                   .OnDelete(DeleteBehavior.Cascade); 
        });
    }
    
}
