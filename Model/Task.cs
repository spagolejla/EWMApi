using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using EWMApi.Model.Enums;
using TaskStatus = EWMApi.Model.Enums.TaskStatus;


/*
     id: string | undefined;
    taskNo: number | undefined;
    title: string | undefined;
    description: string | undefined;
    startDate?: Date | undefined;
    endDate?: Date | undefined;
    assigner?: Item | undefined;
    project: Item | undefined;
    status: TaskStatus | undefined;
    priority: Priority | undefined;
 */

namespace EWMApi.Model
{
    public class Task
    {
        [BsonId]
        public ObjectId InternalId { get; set; }

        public string Id { get; set; }

        public int TaskNo { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Item? Assigner { get; set; }

        public Item? Project { get; set; }

        public TaskStatus? Status { get; set; }

        public Priority? Priority { get; set; }

    }
}
