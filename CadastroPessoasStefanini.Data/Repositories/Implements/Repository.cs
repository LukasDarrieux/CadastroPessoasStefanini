using CadastroPessoasStefanini.Data.Context;
using CadastroPessoasStefanini.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroPessoasStefanini.Data.Repositories.Implements
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly AppDbContext _context;
        public DbSet<T> _entity;

        public Repository(AppDbContext context, DbSet<T> entity )
        {
            _context = context;
            _entity = entity;
        }

        public async Task Add(T model)
        {
            _entity.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var model = await GetById(id);

            _entity.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(T model)
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _entity.ToListAsync();
        }

        public async Task<T>? GetById(int id)
        {
            var model = await _entity.FindAsync(id);
            return model;
        }
    }
}
