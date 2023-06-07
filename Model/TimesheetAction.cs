using EWMApi.Model.Enums;

namespace EWMApi.Model
{
    public class TimesheetAction
    {
        public string Id { get; set; }

        public TimesheetStatus Status { get; set; }

        public string Comment { get; set; }

        public Item User { get; set; }

        public DateTime Date { get; set; }
    }
}
