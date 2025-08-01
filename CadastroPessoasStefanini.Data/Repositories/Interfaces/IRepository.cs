using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroPessoasStefanini.Data.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task Add(T model);
        public Task Edit(T model);
        public Task Delete(int id);
        public Task<IEnumerable<T>> GetAll();
        public Task<T>? GetById(int id);
    }
}
