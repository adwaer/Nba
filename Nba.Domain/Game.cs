using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adwaer.Entity;

namespace Nba.Domain
{
    public class Game : IEntity<string>
    {
        public string Id { get; set; }
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
