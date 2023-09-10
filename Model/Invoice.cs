using EWMApi.Model.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace EWMApi.Model
{
    public class Invoice
    {
        [BsonId]
        public string Id { get; set; }

        public string Month { get; set; }

        public DateTime? Date { get; set; }

        public Item? Employee { get; set; }

        public List<Timesheet> Timesheets { get; set; }

        public double? TotalHours { get; set; }

        public double? TotalCost { get; set; }


    }
}
