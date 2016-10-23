using System.Runtime.Serialization;

namespace Nba.Parser.Contract
{
    [DataContract(Name = "listGroups")]
    internal class ListGroups
    {
        [DataMember(Name = "teams")]
        public Teams[] Teams { get; set; }
        [DataMember(Name = "conference")]
        public string Conference { get; set; }
        [DataMember(Name = "division")]
        public string Division { get; set; }
    }
}