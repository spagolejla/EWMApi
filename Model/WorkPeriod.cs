namespace EWMApi.Model
{
    public class WorkPeriod
    {
        public string Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime? End { get; set; }

        public double? TotalHours { get; set; }

        public Task Task { get; set; }

    }
}
