namespace DataAccessLayer.RequestDTOs
{
    public class EventDto
    {
        public int[] HostIds { get; set; }
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string? Agenda { get; set; }

        public string CountryName { get; set; }

        public string StateName { get; set; }

        public decimal PostalCode { get; set; }



        public DateTimeOffset StartingDate { get; set; }

        public DateTimeOffset EndingDate { get; set; }



        public decimal? CostPerAttendies { get; set; }



        public decimal? EstimateCost { get; set; }
    }
}
