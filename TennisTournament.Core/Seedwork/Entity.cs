using TennisTournament.Entities;

namespace TennisTournament.Seedwork
{
    public class Entity
    {
        public int ID { get; init; }

        public string FirstName { get; init; } = null!;

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { LastName = value.ToUpper(); }
        }

        public bool PlayerIsAlReadyRegistered(List<Player> players)
        {
            if(players.Exists(p => p.FirstName == FirstName) && players.Exists(p => p.LastName == LastName))
            {
                return true;
            }
            return false;
        }
    }
}
