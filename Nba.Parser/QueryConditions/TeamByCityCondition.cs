using System;
using System.Linq.Expressions;
using Nba.Cqrs.Condition.Abstract;
using Nba.Domain;

namespace Nba.Parser.QueryConditions
{
    internal class TeamByCityCondition : ICondition<Team>
    {
        private readonly string _team;

        public TeamByCityCondition(string team)
        {
            _team = team;
        }

        public Expression<Func<Team, bool>> Get()
        {
            var strings = _team.Split(' ');
            var city = strings[0];
            var cityLong = $"{strings[0]} {strings[1]}";

            return entity => entity.City == city
                             || entity.City == cityLong;
        }
    }
}
