using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Nba.Domain.Dto
{
    [DataContract]
    public class GameDto: GamePartDto
    {
        [DataMember(Name = "url")]
        public string Url { get; set; }
        [DataMember(Name = "date")]
        public DateTime DateTime { get; set; }
        [DataMember(Name = "parts")]
        public GamePartDto[] Parts { get; set; }

        public static GameDto Get(Game game)
        {
            return new GameDto
            {
                DateTime = game.DateTime,
                Url = game.Url,
                Loser = TeamDto.Get(game.Loser),
                Winner = TeamDto.Get(game.Winner),
                WinnerScore = game.WinnerScore,
                LoserScore = game.LoserScore,
                Parts = game.GameParts.Select(Get).ToArray()
            };
        }
    }
}
