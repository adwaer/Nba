using System.Runtime.Serialization;

namespace Nba.Parser.Contract
{
    [DataContract(Name = "device")]
    internal class Device
    {
        [DataMember(Name = "clazz")]
        public string Clazz { get; set; }
    }
}