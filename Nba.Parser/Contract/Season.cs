using System.Runtime.Serialization;

namespace Nba.Parser.Contract
{
    [DataContract(Name = "season")]
    internal class Season
    {
        [DataMember(Name = "isCurrent")]
        public string IsCurrent { get; set; }
        [DataMember(Name = "rosterSeasonType")]
        public string RosterSeasonType { get; set; }
        [DataMember(Name = "rosterSeasonYear")]
        public string RosterSeasonYear { get; set; }
        [DataMember(Name = "rosterSeasonYearDisplay")]
        public string RosterSeasonYearDisplay { get; set; }
        [DataMember(Name = "scheduleSeasonType")]
        public string ScheduleSeasonType { get; set; }
        [DataMember(Name = "scheduleSeasonYear")]
        public string ScheduleSeasonYear { get; set; }
        [DataMember(Name = "scheduleYearDisplay")]
        public string ScheduleYearDisplay { get; set; }
        [DataMember(Name = "statsSeasonType")]
        public string StatsSeasonType { get; set; }
        [DataMember(Name = "statsSeasonYear")]
        public string StatsSeasonYear { get; set; }
        [DataMember(Name = "statsSeasonYearDisplay")]
        public string StatsSeasonYearDisplay { get; set; }
        [DataMember(Name = "year")]
        public string Year { get; set; }
        [DataMember(Name = "yearDisplay")]
        public string YearDisplay { get; set; }
    }
}
