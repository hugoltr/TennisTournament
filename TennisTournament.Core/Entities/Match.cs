namespace TennisTournament.Entities
{
    public class Match
    {
        public int ID { get; set; }
        public DateTime StartingDate { get; set; }

        public Player FirstPlayer { get; set; } = null!;
        public Player SecondPlayer { get; set; } = null!;
        public Referee Referee { get; set; } = null!;
        public Court Court { get; set; } = null!;
        public Tournament Tournament { get; set; } = null!;
    }
}
