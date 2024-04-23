using System;
using System.Collections.Generic;

namespace Waitlist.EntityModels;

public partial class WaitRequest
{
    public int WaitId { get; set; }

    public int StudentId { get; set; }

    public string CourseCode { get; set; } = null!;

    public string Term { get; set; } = null!;

    public DateTime Time { get; set; }

    public virtual CourseDatum CourseCodeNavigation { get; set; } = null!;

    public virtual StudentDatum Student { get; set; } = null!;

    public virtual CourseDatum TermNavigation { get; set; } = null!;
}
