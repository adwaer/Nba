using System;
using System.Linq.Expressions;
using Nba.Cqrs.Condition.Abstract;
using Nba.Domain;

namespace Nba.Parser.QueryConditions
{
    internal class DivisionByNameCondition : ICondition<Division>
    {
        private readonly string _name;

        public DivisionByNameCondition(string name)
        {
            _name = name;
        }
        
        public Expression<Func<Division, bool>> Get()
        {
            return division => division.Name == _name;
        }
    }
}
