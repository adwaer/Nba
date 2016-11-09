using Nba.Domain;
using Nba.Domain.Dto;
using Nba.Queries.Criteria;

namespace Nba.Web.Models
{
    public class ScheduleViewModel
    {
        public ScheduledGameDto[] Games { get; set; }
        public DayGamesCondition Condition { get; set; }
        public SeasonDto[] Seasons { get; set; }
    }
}