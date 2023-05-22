using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class Country
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ContactAddress> ContactAddresses { get; } = new List<ContactAddress>();

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<State> States { get; set; } = new List<State>();

    public virtual ICollection<TimeZone> TimeZones { get; set; } = new List<TimeZone>();
}
