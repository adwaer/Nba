using System.Runtime.Serialization;

namespace Nba.Parser.Contract
{
    [DataContract(Name = "teams")]
    internal class Teams
    {
        [DataMember(Name = "profile")]
        public Profile Profile { get; set; }
    }
}