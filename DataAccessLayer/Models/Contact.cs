using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class Contact:ICloneable
{
    public Contact()
    {
        Category = "Non Gov";
    }
    public int Id { get; set; }

    public string Prefix { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int CounterPartyId { get; set; }

    public string? Title { get; set; }

    public string CreatedBy { get; set; } = "System";  

    public DateTimeOffset CreatedOn { get; set; }

    public int EmployeeId { get; set; }

    public string? Category { get; set; } = "Non Gov";

    public bool IsArchieved { get; set; }

    public bool IsDeleted { get; set; }
    public string? DeletedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? UpdatedOn { get; set; }

    public virtual ICollection<ContactAddress> ContactAddresses { get; set; } = new List<ContactAddress>();

    public virtual ICollection<ContactInfo> ContactInfos { get; set; } = new List<ContactInfo>();
    public virtual CounterParty CounterParty { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<EventInvitee> EventInvitees { get; } = new List<EventInvitee>();

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
