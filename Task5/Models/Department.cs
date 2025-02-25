using System;
using System.Collections.Generic;

namespace Task5.Models;

public partial class Department
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? NumberOfemployees { get; set; }

    public string? Code { get; set; }

    public string? IsActive { get; set; }
}
