using System;
using System.Linq.Expressions;
using Nba.Cqrs.Condition.Abstract;
using Nba.Domain;

namespace Nba.Parser.QueryConditions
{
    internal class TeamByNameCondition : ICondition<Team>
    {
        private readonly string _team;

        public TeamByNameCondition(string team)
        {
            _team = team;
        }

        public Expression<Func<Team, bool>> Get()
        {
            var strings = _team.Split(' ');

            var team = strings[strings.Length - 1];
            var teamLong = $"{strings[strings.Length - 2]} {strings[strings.Length - 1]}";

            return entity => entity.Name == team
                             || entity.Name == teamLong;
        }
    }
}
