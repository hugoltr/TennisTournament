using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisTournament.Entities
{
    public class Result
    {
        public int ID { get; set; }
        public DateTime EndingDate { get; set; }
        public Player Winner { get; set; } = null!;
        public Match Match { get; set; } = null!;
        public string Scores { get; set; } = null!;
    }
}
