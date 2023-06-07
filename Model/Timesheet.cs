using EWMApi.Model.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EWMApi.Model
{
    public class Timesheet
    {
        [BsonId]
        public string Id { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Date { get; set; }

        public Item User { get; set; }

        public TimesheetStatus Status { get; set; }

        public double? TotalHours { get; set; }

        public List<WorkPeriod> WorkPeriods { get; set; }

        public List<TimesheetAction> Actions { get; set; }

    }
}
