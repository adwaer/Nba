using Adwaer.Entity;

namespace Nba.Cqrs.Condition.Abstract
{
    public interface IDictionaryEntity: IEntity<int>
    {
        string Name { get; set; }
    }
}