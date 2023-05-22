using DataAccessLayer.RequestDTOs;

namespace DataAccessLayer.Models;

public partial class EventApproval
{
    public EventApproval()
    {
        ChiefStatus = status.Pending.ToString();
        CreatedBy = "system";
        CreatedOn = DateTimeOffset.Now;
        ComplianceTeamStatus= status.Pending.ToString();
    }
    public int Id { get; set; }

    public int EventId { get; set; }

    public string? ComplianceTeamStatus { get; set; } 

    public string? ChiefStatus { get; set; }

    public string? CommentByChief { get; set; }

    public string? CommentByCompliance { get; set; }

    public string CreatedBy { get; set; }
    public bool IsDeleted { get; set; } = false;
    public string? ComplianceEmail { get; set; }
    public string ? ChiefEmail { get; set; }

    public DateTimeOffset CreatedOn { get; set; } 

    public virtual Event Event { get; set; } = null!;
}
