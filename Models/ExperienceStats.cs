namespace TravelApp.Models
{
    public class ExperienceStats
    {
        public int Id { get; set; }
        public int SuccessfulTours { get; set; }
        public int YearsExperience { get; set; }
        public int TotalCustomers { get; set; }
        public decimal? PrimaryMetricPercentage { get; set; }
        public decimal? WarningMetricPercentage { get; set; }
        public decimal? DangerMetricPercentage { get; set; }
    }
}