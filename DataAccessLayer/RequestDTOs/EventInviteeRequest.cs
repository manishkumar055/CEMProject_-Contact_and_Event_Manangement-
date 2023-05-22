namespace DataAccessLayer.RequestDTOs
{
    public class EventInviteeRequest
    {
        public int ContactId { get; set; }

        public int EventId { get; set; }
        public Del Want_Delete { get; set; }
    }
    public enum Del
    {
        Yes=1,
        No=2
    }
}


