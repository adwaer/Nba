using System;
using Adwaer.Entity;
using Nba.Domain.Enums;

namespace Nba.Domain
{
    public class Season : IEntity<int>
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCompleted { get; set; }
        public SeasonType SeasonType { get; set; }
    }
}
