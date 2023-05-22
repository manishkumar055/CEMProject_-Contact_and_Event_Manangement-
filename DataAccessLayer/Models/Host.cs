using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class Host
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public int EventId { get; set; }

    public string CreatedBy { get; set; } = "system";

    public DateTimeOffset CreatedOn { get; set; }= DateTimeOffset.Now;

    public string HostType { get; set; } = "Host";

    public virtual Event Event { get; set; } = null!;
}
