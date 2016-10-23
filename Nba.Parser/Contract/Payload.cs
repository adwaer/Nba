using System.Runtime.Serialization;

namespace Nba.Parser.Contract
{
    [DataContract(Name = "payload")]
    internal class Payload
    {
        [DataMember(Name = "listGroups")]
        public ListGroups[] ListGroups { get; set; }
        [DataMember(Name = "grouping")]
        public string Grouping { get; set; }
        [DataMember(Name = "league")]
        public League League { get; set; }
        [DataMember(Name = "season")]
        public Season Season { get; set; }
    }
}