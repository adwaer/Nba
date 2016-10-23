using System.Runtime.Serialization;

namespace Nba.Parser.Contract
{
    [DataContract(Name = "user")]
    internal class User
    {
        [DataMember(Name = "countryCode")]
        public string CountryCode { get; set; }
        [DataMember(Name = "countryName")]
        public string CountryName { get; set; }
        [DataMember(Name = "locale")]
        public string Locale { get; set; }
        [DataMember(Name = "timeZone")]
        public string TimeZone { get; set; }
        [DataMember(Name = "timeZoneCity")]
        public string TimeZoneCity { get; set; }
    }
}