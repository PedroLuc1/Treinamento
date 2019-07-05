using System;
using ContosUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosUniversity.Data
{
    public class UsuarioContext : DbContext
    {
        public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySQL("server=localhost;database=treinamento;user=root;password=");
            optionsBuilder.UseMySQL("server=localhost;database=treinamento;user=root;password=123456");
        }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}