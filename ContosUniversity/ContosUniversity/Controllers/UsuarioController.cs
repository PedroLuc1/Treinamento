﻿using ContosUniversity.Data;
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
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        { 
          return await _context.Usuarios.FindAsync(id);

            
        }
        [HttpGet("nome/{Nome}")]
        public async Task<ActionResult<Usuario>> GetUsuarioNome(string nome)
        {
           return await _context.Usuarios.FirstOrDefaultAsync(usuario => usuario.Nome==nome);
            
        }
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario item)
        {
            _context.Usuarios.Add(item);
            await _context.SaveChangesAsync();
            return new ObjectResult("Usuario adicionado");

        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>>  PutUsuario(int id, Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
           await  _context.SaveChangesAsync();

            return new ObjectResult("Usuario Alterado");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> DeleteUsuario(int id)
        {
           var usuario= await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();

            return new ObjectResult("Usuario deletado.");

        }
    }
}