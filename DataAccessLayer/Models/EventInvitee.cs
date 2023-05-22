using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class EventInvitee
{
    public EventInvitee()
    {
        CreatedBy = "system";
        CreatedOn= DateTimeOffset.Now;

    }
    public int Id { get; set; }

    public int ContactId { get; set; }

    public int EventId { get; set; }

    public string? Status { get; set; }

    public DateTimeOffset CreatedOn { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string? StatusBy { get; set; }

    public DateTimeOffset? StatusOn { get; set; }

    public virtual Contact Contact { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;
}
