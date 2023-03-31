using EWMApi.Model.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace EWMApi.Model
{
    public class Project
    {
        [BsonId]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public double? BudgetAmount { get; set; }

        public bool HasTicketsAssigned { get; set; }

        public ProjectStatus? Status { get; set; }

        public string Location { get; set; }

    }
}
