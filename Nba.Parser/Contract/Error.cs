using System.Runtime.Serialization;

namespace Nba.Parser.Contract
{
    [DataContract(Name = "error")]
    internal class Error
    {
        [DataMember(Name = "detail")]
        public string Detail { get; set; }
        [DataMember(Name = "isError")]
        public bool IsError { get; set; }
        [DataMember(Name = "message")]
        public string Message { get; set; }
    }
}