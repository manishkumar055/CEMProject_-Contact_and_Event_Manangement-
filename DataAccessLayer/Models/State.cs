using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class State
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CountryId { get; set; }

    public virtual ICollection<ContactAddress> ContactAddresses { get; } = new List<ContactAddress>();

    public virtual Country Country { get; set; } = null!;
}
