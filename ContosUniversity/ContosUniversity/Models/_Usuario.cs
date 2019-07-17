using ContosUniversity.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
            novoUsuario.CPF = usuario.CPF;

            var verifica = await Obter(usuario1 => usuario1.Email == novoUsuario.Email);

            if (verifica.Count() != 0)
            {
                throw new Exception("Email invalido");

            }
            ValidaCpf(novoUsuario.CPF);

            await base.Criar(novoUsuario);
            await base.Persistir();

            return novoUsuario;
        }
        public async Task<Usuario> Editar(Usuario usuario, long id)
        {

            var editaUsuario = new Usuario();
            editaUsuario.ID = usuario.ID;
            editaUsuario.Email = usuario.Email;
            editaUsuario.Nome = usuario.Nome;
            editaUsuario.Senha = usuario.Senha;
            editaUsuario.CPF = usuario.CPF;

            await base.Editar(editaUsuario, id);
            await base.Persistir();

            return editaUsuario;
        }
        public async Task<Usuario> Excluir(int id)
        {

            var pegaId = await Obter(id);

            await base.Excluir(pegaId);
            await base.Persistir();

            return pegaId;
        }
        public async Task<IEnumerable<Usuario>> ObterNome(string nome)
        {
            var pesquisa = await Obter(usuario => usuario.Nome.Contains(nome));
            return pesquisa;
        }
        public void ValidaCpf(string cpf)
        {

            string valor = cpf;
            int digito1, digito2, i;

            //formula fixa para calcular os digitos verificadores
            int[] peso1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] peso2 = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int total, soma = 0, resto = 0;

            //vetor para receber os digitos do cpf digitado
            int[] digitos = new int[11];
            //usar substring  converter para int para armazenar os digitos no vetor digitos
            for (i = 0; i <= 8; i++)
            {
                //var pegaDigito = valor[i] - 48;
                //digitos[i] = pegaDigito;

                digitos[i] = valor[i] - 48;
            }
            // laço para calcular  o primeiro digito verificador
            for (i = 0; i <= 8; i++)
            {
                total = peso1[i] * digitos[i];
                soma += total;
            }
            resto = soma % 11;
            if (resto < 2)
            {
                digito1 = 0;
            }
            else
            {
                digito1 = 11 - resto;
            }
            digitos[9] = digito1;
            soma = 0;
            //laço para calcular o segundo digito verificador
            for (i = 0; i <= 9; i++)
            {
                total = peso2[i] * digitos[i];
                soma += total;
            }
            resto = soma % 11;
            if (resto < 2)
            {
                digito2 = 0;
            }
            else
            {
                digito2 = 11 - resto;
            }
            digitos[10] = digito2;
            if (digitos[9] != valor[9]-48 || digitos[10] != valor[10]-48)
            {
                throw new Exception("CPF invalido");
            }

        }
    }

    public class UsuarioMetadata
    {

    }
}
