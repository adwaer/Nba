using System.Runtime.Serialization;

namespace Nba.Parser.Contract
{
    [DataContract(Name = "ConferenceTeamListResponse")]
    internal class ConferenceTeamListResponse
    {
        [DataMember(Name = "context")]
        public Context Context { get; set; }
        [DataMember(Name = "error")]
        public Error Error { get; set; }
        [DataMember(Name = "payload")]
        public Payload Payload { get; set; }
        [DataMember(Name = "timestamp")]
        public string Timestamp { get; set; }
    }
}