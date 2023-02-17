namespace TennisTournament.Models
{
    public class MatchCreateViewModel
    {
        public int FirstPlayerID { get; set; }
        public int SecondPlayerID { get; set; }
        public int RefereeID { get; set; }
        public int CourtID { get; set; }
        public int TournamentID { get; set; }
        public DateTime StartingDate { get; set; } = DateTime.Now;
    }
}
