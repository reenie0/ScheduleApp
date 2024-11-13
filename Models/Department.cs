

namespace ScheduleApp.Models;

public class Department
{
    public int DepartmentId { get; set; }
    public required string DepartmentName { get; set; }
    public required string HeadOfDepartment {  get; set; }


    public required virtual ICollection<Lecturer> Lecturer { get; set; }
}
