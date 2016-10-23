using System.Runtime.Serialization;
using Adwaer.Entity;
using Nba.Domain.Abstract;

namespace Nba.Domain
{
    [DataContract]
    public class Conference : EntityBase<int>, IHasName
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}