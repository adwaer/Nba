using System.Runtime.Serialization;
using Nba.Cqrs.Condition.Abstract;

namespace Nba.Cqrs.Condition
{
    public class PagingContition : IPagingContition
    {
        [DataMember(Name = "page")]
        public int Page { get; set; }
        [DataMember(Name = "count")]
        public int Count { get; set; }
    }
}
