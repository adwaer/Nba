using System.Runtime.Serialization;

namespace Nba.Domain.Dto
{
    [DataContract]
    public class TeamDto
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
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
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "nameEn")]
        public string NameEn { get; set; }

        public static TeamDto Get(Team team)
        {
            return new TeamDto
            {
                Id = team.Id,
                Abbr = team.Abbr,
                City = team.City,
                CityEn = team.CityEn,
                Code = team.Code,
                DisplayAbbr = team.DisplayAbbr,
                Name = team.Name,
                NameEn = team.NameEn
            };
        }
    }
}
