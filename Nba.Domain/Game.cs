using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Adwaer.Entity;
namespace Nba.Domain
{
    [DataContract]
    public class Game : EntityBase<int>
    {
        public string Url { get; set; }
        public DateTime DateTime { get; set; }
        public int WinnerScore { get; set; }
        public int LoserScore { get; set; }
        public virtual Team Winner { get; set; }
        public virtual Team Loser { get; set; }
        public virtual Season Season { get; set; }
        public virtual ICollection<GamePart> GameParts { get; set; }
    }
}
