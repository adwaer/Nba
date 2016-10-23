using System.Data.Entity;
using System.Threading.Tasks;
using Nba.Cqrs;
using Nba.Dal;
using Nba.Domain;

namespace Nba.Parser
{
    public class FetchTeamsCommand : ICommand
    {
        private readonly DbContext _dbContext;

        public FetchTeamsCommand(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute()
        {
            var teams = await new TeamsQuery(_dbContext)
                .Execute();

            _dbContext
                .Set<Team>()
                .AddRange(teams);

            await _dbContext.SaveChangesAsync();
        }
    }
}
