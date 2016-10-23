using System.Runtime.Serialization;

namespace Nba.Parser.Contract
{
    [DataContract(Name = "context")]
    internal class Context
    {
        [DataMember(Name = "user")]
        public User User { get; set; }
        [DataMember(Name = "device")]
        public Device Device { get; set; }
    }
}