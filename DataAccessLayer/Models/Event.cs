using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public class Event
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? Agenda { get; set; }

    public int CountryId { get; set; }

    public int StateId { get; set; }

    public decimal PostalCode { get; set; }

    public int TimeZoneId { get; set; } = 1;

    public string CreatedBy { get; set; } = "system";

    public DateTimeOffset CreatedOn { get; set; }= DateTimeOffset.Now;

    public DateTimeOffset StartingDate { get; set; }

    public DateTimeOffset EndingDate { get; set; }

    public string Status { get; set; } = "Not Required";

    public decimal? CostPerAttendies { get; set; }

    public DateTimeOffset? UpdatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public bool IsOpen { get; set; }=false;

    public decimal? EstimateCost { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<EventApproval> EventApprovals { get; set; } = new List<EventApproval>();

    public virtual ICollection<EventAttachment> EventAttachments { get; set; } = new List<EventAttachment>();

    public virtual ICollection<EventInvitee> EventInvitees { get; set; } = new List<EventInvitee>();

    public virtual ICollection<Host> Hosts { get; set; } = new List<Host>();
}
