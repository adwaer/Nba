using System.Data.Entity;
using System.Threading.Tasks;
using Adwaer.Entity;

namespace Nba.Cqrs.Query
{
    public class ByIdQuery<T> : IQuery<int, Task<T>> where T : class, IEntity<int>
    {
        private readonly DbContext _dbContext;
        public ByIdQuery(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Execute(int id)
        {
            return await _dbContext
                .Set<T>()
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
