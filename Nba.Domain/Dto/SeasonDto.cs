using System;

namespace Nba.Domain.Dto
{
    public class SeasonDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
