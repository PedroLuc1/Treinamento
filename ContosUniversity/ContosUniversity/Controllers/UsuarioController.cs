﻿
using ContosUniversity.Data;
using ContosUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosUniversity.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioContext _context;

        public UsuarioController(UsuarioContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Models.Usuario>> GetUsuario(int id)
        { 
          return await _context.Usuarios.FindAsync(id);            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
       // [HttpGet("nome/{Nome}")]
       // public async Task<ActionResult<Usuario>> GetUsuarioNome(string nome)
      //  {
            //
            //
            // ATENÇÃO x1234
            // 
            //
            //

            //return await _context.Usuarios.FirstOrDefaultAsync(usuario => usuario.Nome == nome);  

          //  throw new NotImplementedException();
       // }
        [HttpGet("pesquisa/{Nome}")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetPesquisaNome(string nome)
        {
            var iusuario = new UsuarioBusinessModels(_context);
            var pesquisa = await iusuario.ObterNome(nome);
            return new ObjectResult(pesquisa);
            

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario item)
        {

            var iusuario = new UsuarioBusinessModels(_context);

            var novoUsuario = await iusuario.Criar(item);

            return novoUsuario;

            //_context.Usuarios.Add(item);
            //await _context.SaveChangesAsync();
            //return new ObjectResult("Usuario adicionado");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>>  PutUsuario(Usuario usuario, int id)
        {
            var iusuario = new UsuarioBusinessModels(_context);
            var editaUsuario = await iusuario.Editar(usuario, id);
          /*  _context.Entry(usuario).State = EntityState.Modified;
           await  _context.SaveChangesAsync();*/

            return new ObjectResult("Usuario Alterado");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> DeleteUsuario(int id)
        {
            var iusuario = new UsuarioBusinessModels(_context);
            var excluiUsuario = await iusuario.Excluir(id);
          // var usuario= await _context.Usuarios.FindAsync(id);
           // _context.Usuarios.Remove(usuario);
           // _context.SaveChanges();

            return new ObjectResult("Usuario deletado.");

        }
    }
}