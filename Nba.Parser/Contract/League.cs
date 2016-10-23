using System.Runtime.Serialization;

namespace Nba.Parser.Contract
{
    [DataContract(Name = "league")]
    internal class League
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}