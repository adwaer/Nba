using System;
using Adwaer.Entity;

namespace Nba.Domain
{
    public class ScheduledGame : IEntity<int>
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime Date { get; set; }
        public virtual Team Team1 { get; set; }
        public virtual Team Team2 { get; set; }
    }
}
