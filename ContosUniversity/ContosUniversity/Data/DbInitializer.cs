using ContosUniversity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ContosUniversity.Data
{
    public class DbInitializer
    {
        public static void Initialize(UsuarioContext context)
        {
            if (context.Usuarios.Any())
            {
                return;
            }
            var usuarios = new Usuario[]
            {
                new Usuario{Nome="Jose",Email="JoseJosecom",Senha="123"},
                new Usuario{Nome="Maria",Email="MariaMcom",Senha="321"}
            };
            foreach(Usuario s in usuarios)
            {
                context.Usuarios.Add(s);
            }
            context.SaveChanges();
        }
    }
}
