using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class EventAttachment
{
    public EventAttachment()
    {
        CreatedBy = "system";
        CreatedOn=DateTimeOffset.Now;
    }
    public int Id { get; set; }

    public int EventId { get; set; }

    public int EventInviteeId { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTimeOffset CreatedOn { get; set; }

    public string AttachmentPath { get; set; }

    public string? AttachmentName { get; set; }

    public virtual Event Event { get; set; } = null!;
}
