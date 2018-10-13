using System.Collections.Generic;

namespace Nba.Domain.Dto
{
    public class QuarterGameDto
    {
        public int Percent { get; set; }
        public QuarterGamePartDto CurrentSeasonScore { get; set; }
        public List<QuarterGamePartDto> Parts { get; set; }
    }
}
