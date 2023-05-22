using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string Title { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();
    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<CounterParty> CounterParties { get; set; } = new List<CounterParty>();
}
