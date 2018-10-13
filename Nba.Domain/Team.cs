using System.Collections.Generic;
using System.Runtime.Serialization;
using Adwaer.Entity;

namespace Nba.Domain
{
    [DataContract]
    public class Team : EntityBase<int>
    {
        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; }

    }
}
