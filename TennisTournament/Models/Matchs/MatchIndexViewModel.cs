using TennisTournament.Entities;

namespace TennisTournament.Models.Matchs
{
    public class MatchIndexViewModel
    {
        public int ID { get; set; }
        public DateTime StartingDate { get; set; }

        public Player FirstPlayer { get; set; } = null!;
        public Player SecondPlayer { get; set; } = null!;

    }
}
