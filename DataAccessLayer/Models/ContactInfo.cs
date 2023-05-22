using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class ContactInfo
{
    public int Id { get; set; }

    public int ContactId { get; set; }

    public string? ContactType { get; set; }

    public bool IsPrimary { get; set; }

    public string ContactContent { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public virtual Contact Contact { get; set; } = null!;
}
