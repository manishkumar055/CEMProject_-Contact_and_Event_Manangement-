using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class ContactAddress
{
    public int Id { get; set; }

    public int ContactId { get; set; } = 0;

    public int CountryId { get; set; }

    public int StateId { get; set; }

    public string? City { get; set; }

    public decimal PostalCode { get; set; }

    public string? AddressType { get; set; }

    public bool IsPrimary { get; set; }=false;

    public bool IsDeleted { get; set; } = false;

    public virtual Contact Contact { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual State State { get; set; } = null!;
}
