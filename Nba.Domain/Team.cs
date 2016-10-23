using System.Collections.Generic;
using System.Runtime.Serialization;
using Adwaer.Entity;

namespace Nba.Domain
{
    [DataContract]
    public class Team : IEntity<string>
    {
        [DataMember(Name = "abbr")]
        public string Abbr { get; set; }
        [DataMember(Name = "city")]
        public string City { get; set; }
        [DataMember(Name = "cityEn")]
        public string CityEn { get; set; }
        [DataMember(Name = "code")]
        public string Code { get; set; }
        [DataMember(Name = "displayAbbr")]
        public string DisplayAbbr { get; set; }
        [DataMember(Name = "displayConference")]
        public string DisplayConference { get; set; }
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "isAllStarTeam")]
        public bool IsAllStarTeam { get; set; }
        [DataMember(Name = "isLeagueTeam")]
        public bool IsLeagueTeam { get; set; }
        [DataMember(Name = "leagueId")]
        public string LeagueId { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "nameEn")]
        public string NameEn { get; set; }

        [DataMember(Name = "conference")]
        public Conference Conference { get; set; }
        [DataMember(Name = "division")]
        public Division Division { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}
