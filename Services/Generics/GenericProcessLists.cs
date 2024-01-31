using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Models.Base;
using ProvaPub.Repository;
using ProvaPub.Services.Interfaces;

namespace ProvaPub.Services.Generics {
    public class GenericProcessLists<T> : IGenericProcessLists<T> where T : BaseModel {

        private readonly TestDbContext _ctx;
        private readonly DbSet<T> _dbSet;

        public GenericProcessLists(TestDbContext ctx) {
            _ctx = ctx;
            _dbSet = _ctx.Set<T>();
        }

        public async Task<List<T>> ListEntitiesAsync(int page) {
            try {
                var itensPorPag = 10; // do requisito
                int itemsPular = (page - 1) * itensPorPag;

                List<T> entities = await _dbSet.Skip(itemsPular).Take(itensPorPag).ToListAsync();

                return entities;
            }
            catch(Exception) {
                throw;
            }
        }

    }
}
