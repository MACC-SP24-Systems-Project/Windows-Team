using System;
using System.Collections.Generic;

namespace Waitlist.EntityModels;

public partial class CourseDatum
{
    public string CourseCode { get; set; } = null!;

    public string Term { get; set; } = null!;

    public string CourseTitle { get; set; } = null!;

    public string Instructor { get; set; } = null!;

    public string Seats { get; set; } = null!;

    public string Schedule { get; set; } = null!;

    public string Location { get; set; } = null!;

    public decimal Credits { get; set; }
}
