using ContosUniversity.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ContosUniversity.Models
{
    public class Base<TPrimaryEntity, TDependentEntity>
        where TPrimaryEntity : class
        where TDependentEntity : class
    {
        protected readonly UsuarioContext bd;
        protected readonly DbSet<TPrimaryEntity> PrimaryDatabaseSet;
        protected readonly DbSet<TDependentEntity> DependentDatabaseSet;

        public Base(UsuarioContext _bd)
        {
            this.bd = _bd;
            this.PrimaryDatabaseSet = bd.Set<TPrimaryEntity>();
            this.DependentDatabaseSet = bd.Set<TDependentEntity>();
        }

        public virtual async Task<TPrimaryEntity> Obter(long id)
        {            
            return await PrimaryDatabaseSet.FindAsync(id).ConfigureAwait(false);
        }

        public virtual async Task<TPrimaryEntity> Obter(int id)
        {
            return await PrimaryDatabaseSet.FindAsync(id).ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TPrimaryEntity>> Obter(/*bool asNoTraking = true*/)
        {            
            return await this.PrimaryDatabaseSet.ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<TPrimaryEntity>> Obter(Expression<Func<TPrimaryEntity, bool>> expressao/*, bool asNoTraking = true*/)
        {           
            return await PrimaryDatabaseSet.Where(expressao).ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task Criar(TPrimaryEntity Objeto)
        {
            if (Objeto == null)
            {
                throw new ArgumentNullException("Objeto: Não é possível adicionar entidade com valores nulos.");
            }

            await this.PrimaryDatabaseSet.AddAsync(Objeto);
        }

        public virtual async Task Criar(TPrimaryEntity ObjetoPrimario, TDependentEntity ObjetoDependente)
        {
            if (ObjetoPrimario == null)
            {
                throw new ArgumentNullException("ObjetoPrimario: Não é possível adicionar entidade com valores nulos.");
            }
            if (ObjetoDependente == null)
            {
                throw new ArgumentNullException("ObjetoDependente: Não é possível adicionar entidade com valores nulos.");
            }
            await this.Criar(ObjetoPrimario);
            this.DependentDatabaseSet.Attach(ObjetoDependente); // atacha
            bd.Entry(ObjetoDependente).State = EntityState.Added; //Insere
        }

        public virtual Task Excluir(TPrimaryEntity Objeto)
        {
            if (Objeto == null)
            {
                throw new ArgumentNullException("Objeto: Não é possível excluir entidade com valores nulos.");
            }
            this.PrimaryDatabaseSet.Remove(Objeto);
            return Task.CompletedTask;
        }

        public virtual Task Excluir(Func<TPrimaryEntity, bool> expressao)
        {

            PrimaryDatabaseSet.RemoveRange(PrimaryDatabaseSet.ToList().Where(expressao));
            return Task.CompletedTask;
        }

        public virtual Task Editar(TPrimaryEntity Objeto, long key)
        {
            this.Update<TPrimaryEntity>(Objeto, key);
            return Task.CompletedTask;
        }

        //public virtual Task Editar(TPrimaryEntity ObjetoPrimario, TDependentEntity ObjetoDependente)
        //{
        //    this.Editar(ObjetoPrimario);
        //    this.Update<TDependentEntity>(ObjetoDependente, ObjetoPrimario.Id);
        //    return Task.CompletedTask;
        //}

        private void Update<T>(T model, long key) where T : class
        {
            if (model == null)
            {
                throw new ArgumentNullException(model.ToString() + ": Não é possível atualizar objeto com valores nulos.");
            }
            var entry = this.bd.Entry(model);
            if (entry.State == EntityState.Detached)
            {
                var currentEntry = this.bd.Set<T>().Find(key);
                if (currentEntry != null)
                {
                    var attachedEntry = this.bd.Entry(currentEntry);
                    attachedEntry.CurrentValues.SetValues(model);
                }
                else
                {
                    this.bd.Set<T>().Attach(model);
                    entry.State = EntityState.Modified;
                }
            }

        }

        public async Task Persistir()
        {
            await bd.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
