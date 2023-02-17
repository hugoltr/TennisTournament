namespace TennisTournament.Entities
{
    public class Tournament
    {
        private List<Match> _matchs = new List<Match>();
        public IReadOnlyCollection<Match> Matchs => _matchs.AsReadOnly();

        public string Name { get; set; } = null!;

        public int ID { get; set; }
    }
}