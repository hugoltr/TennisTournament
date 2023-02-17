namespace TennisTournament.Seedwork
{
    public class Entity
    {
        public int ID { get; init; }

        public string FirstName { get; init; } = null!;

        private string lastName;
        public string Lastname
        {
            get { return lastName; }
            set { Lastname = value.ToUpper(); }
        }
    }
}
