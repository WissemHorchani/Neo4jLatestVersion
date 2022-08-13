namespace Neo4jCrud.Models.Request
{
    public class ProfileCreationRequest
    {
        public ProfileCreationRequest(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
