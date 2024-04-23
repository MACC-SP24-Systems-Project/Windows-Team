using System;
using System.Collections.Generic;

namespace Waitlist.EntityModels;

public partial class StudentDatum
{
    public int StudentId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public virtual WaitRequest? WaitRequest { get; set; }
}
