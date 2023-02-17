using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using TennisTournament.Seedwork;

namespace TennisTournament.Entities
{
    public class Player : Entity
    {
        public Nationality Nationality { get; set; }

        public Sexe Sexe { get; set; }

        private List<Match> _matchs = new List<Match>();
        [NotMapped]
        public IReadOnlyCollection<Match> Matchs => _matchs.AsReadOnly();

        private List<Result> _results = new List<Result>();
        public IReadOnlyCollection<Result> Results => _results.AsReadOnly(); 
    }
}
