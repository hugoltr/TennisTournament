using System.ComponentModel.DataAnnotations;
using TennisTournament.Validator;

namespace TennisTournament.Models
{
    public class MatchCreateViewModel
    {
        [Required(ErrorMessage = "Ce champs est requis !")]
        public int FirstPlayerID { get; set; }

        [Required(ErrorMessage = "Ce champs est requis !")]
        [NotCompare("FirstPlayerID")]
        public int SecondPlayerID { get; set; }

        [Required(ErrorMessage = "Ce champs est requis !")]
        public int RefereeID { get; set; }

        [Required(ErrorMessage = "Ce champs est requis !")]
        public int CourtID { get; set; }

        [Required(ErrorMessage = "Ce champs est requis !")]
        public int TournamentID { get; set; }

        [Required(ErrorMessage = "Ce champs est requis !")]
        public DateTime StartingDate { get; set; } = DateTime.Now;
    }
}
