namespace TennisTournament.Entities
{
    public class Court
    {
        public int ID { get; set; }

        public string Name { get; set; } = null!;
        public int Number { get; set; }

        private List<Match> _matchs = new List<Match>();
        public IReadOnlyCollection<Match> Matchs => _matchs.AsReadOnly();
    }
}