using System;
using System.Linq.Expressions;
using Adwaer.Entity;
using Nba.Cqrs.Condition.Abstract;

namespace Nba.Cqrs.Condition
{
    public class ByIdCondition<T> : ICondition<T> where T : IEntity<int>
    {
        private readonly int _id;

        public ByIdCondition(int id)
        {
            _id = id;
        }

        public Expression<Func<T, bool>> Get()
        {
            return division => division.Id == _id;
        }
    }
}
