using System;
using ContosUniversity.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosUniversity.Data
{
    public class UsuarioContext : DbContext
    {
        public UsuarioContext(DbContextOptions<UsuarioContext>options) :base(options)
        {

        }
        public Dbset<Students> Student { get; set; }
    }
}