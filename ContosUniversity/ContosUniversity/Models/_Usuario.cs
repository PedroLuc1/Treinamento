using ContosUniversity.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContosUniversity.Models
{
    [ModelMetadataType(typeof(UsuarioMetadata))]
    public partial class Usuario
    {

    }

    [ModelMetadataType(typeof(UsuarioMetadata))]
    public class UsuarioBusinessModels : Base<Usuario, object>
    {
        public UsuarioBusinessModels(UsuarioContext bd)
          : base(bd)
        {
        }

        /// <summary>
        /// Cadastra dados de usuário
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task<Usuario> Criar(Usuario usuario)
        {
            
            var novoUsuario = new Usuario();

            novoUsuario.Email = usuario.Email;
            novoUsuario.Nome = usuario.Nome;
            novoUsuario.Senha = usuario.Senha;

            await base.Criar(novoUsuario);
            await base.Persistir();

            return novoUsuario;
        }        
    }

    public class UsuarioMetadata
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
    }
}
