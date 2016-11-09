using System.Runtime.Serialization;

namespace Nba.Domain.Dto
{
    [DataContract]
    public class GamePartDto
    {
        [DataMember(Name = "winnerScore")]
        public int WinnerScore { get; set; }
        [DataMember(Name = "loserScore")]
        public int LoserScore { get; set; }
        [DataMember(Name = "winner")]
        public TeamDto Winner { get; set; }
        [DataMember(Name = "loser")]
        public TeamDto Loser { get; set; }

        public static GamePartDto Get(GamePart gamePart)
        {
            return new GamePartDto
            {
                LoserScore = gamePart.LoserScore,
                WinnerScore = gamePart.WinnerScore,
                Winner = TeamDto.Get(gamePart.Winner),
                Loser = TeamDto.Get(gamePart.Loser)
            };
        }
    }
}
