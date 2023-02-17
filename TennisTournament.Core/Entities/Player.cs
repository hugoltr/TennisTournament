using System.Text.RegularExpressions;
using TennisTournament.Seedwork;

namespace TennisTournament.Entities
{
    public class Player : Entity
    {
        public Nationality Nationality { get; set; }

        public Sexe Sexe { get; set; }

        private List<Match> _matchs = new List<Match>();
        public IReadOnlyCollection<Match> Matchs => _matchs.AsReadOnly();
    }
}
