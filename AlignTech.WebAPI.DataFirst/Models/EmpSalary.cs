using System;
using System.Collections.Generic;

namespace AlignTech.WebAPI.DataFirst.Models;

public partial class EmpSalary
{
    public string? EmpName { get; set; }

    public string? Department { get; set; }

    public string? Category { get; set; }

    public decimal? Salary { get; set; }
}
