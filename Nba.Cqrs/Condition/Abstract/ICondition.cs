using System;
using System.Linq.Expressions;

namespace Nba.Cqrs.Condition.Abstract
{
    public interface ICondition<T>
    {
        Expression<Func<T, bool>> Get();
    }
}
