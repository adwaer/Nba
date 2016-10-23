using System.Data.Entity;
using System.Linq;
using Nba.Cqrs.Condition.Abstract;

namespace Nba.Cqrs.Query
{
    public class SimpleQuery : IQuery
    {
        private readonly DbContext _dbContext;

        public SimpleQuery(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Execute<T>(ICondition<T> condition) where T : class
        {
            return _dbContext
                .Set<T>()
                .Where(condition.Get());
        }
    }
}
