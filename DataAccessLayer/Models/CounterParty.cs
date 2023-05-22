using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class CounterParty
{
    public int Id { get; set; }

    public string CompanyName { get; set; } = null!;

    public string IsApproved { get; set; } = "Pending";

    public string CreatedBy { get; set; } ="system";

    public DateTimeOffset CreatedOn { get; set; }=DateTimeOffset.Now;

    public int EmployeeId { get; set; } = 1;

    public bool IsDeleted { get; set; }=false;
    public string? ApprovedBy { get; set; }

    public virtual Employee Employee { get; set; } = null!;
    public virtual ICollection<Contact> Contacts { get; set; }=new List<Contact>();
}
