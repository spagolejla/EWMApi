using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using EWMApi.Model.Enums;
using TaskStatus = EWMApi.Model.Enums.TaskStatus;


namespace EWMApi.Model
{
    public class Task
    {
        [BsonId]
        public string Id { get; set; }

        public int TaskNo { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Item? Assigner { get; set; }

        public Item? Project { get; set; }

        public TaskStatus? Status { get; set; }

        public Priority? Priority { get; set; }

    }
}
