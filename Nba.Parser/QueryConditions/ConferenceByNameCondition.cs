using System;
using System.Linq.Expressions;
using Nba.Cqrs.Condition.Abstract;
using Nba.Domain;

namespace Nba.Parser.QueryConditions
{
    internal class ConferenceByNameCondition : ICondition<Conference>
    {
        private readonly string _name;

        public ConferenceByNameCondition(string name)
        {
            _name = name;
        }

        public Expression<Func<Conference, bool>> Get()
        {
            return entity => entity.Name == _name;
        }

        public bool Get(Conference entity)
        {
            return entity.Name == _name;
        }
    }
}
