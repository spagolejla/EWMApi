using MongoDB.Bson.Serialization.Attributes;


namespace EWMApi.Model
{
    public class Employee
    {
        [BsonId]
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? Email { get; set; }

        public string Password { get; set; }

        public string Telephone { get; set; }

        public string Position { get; set; }

        public DateTime? StartWorkDate { get; set; }

        public string ShortDescription { get; set; }

        public double SalaryPerHour { get; set; }

        public bool Active { get; set; }

        public string? Picture { get; set; }

        public string? AvatarUrl { get; set; }

    }
}
