using System;

namespace Nba.Domain.Dto
{
    public class QuarterGamePartDto
    {
        public bool Team1Win { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public DateTime Date { get; set; }
    }
}