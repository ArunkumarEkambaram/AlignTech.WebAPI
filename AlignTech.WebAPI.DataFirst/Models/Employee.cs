using System;
using System.Collections.Generic;

namespace AlignTech.WebAPI.DataFirst.Models;

public partial class Employee
{
    public short EmployeeId { get; set; }

    public string Name { get; set; } = null!;

    public string EmailId { get; set; } = null!;

    public string Designation { get; set; } = null!;

    public short? ManagerId { get; set; }

    public virtual ICollection<Employee> InverseManager { get; set; } = new List<Employee>();

    public virtual Employee? Manager { get; set; }
}
