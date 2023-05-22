using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class TimeZone
{
    public int Id { get; set; }

    public int CountryId { get; set; }

    public DateTimeOffset DateTime { get; set; }

    public virtual Country Country { get; set; } = null!;
}
