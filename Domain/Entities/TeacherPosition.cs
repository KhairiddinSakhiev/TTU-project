using System.Diagnostics.SymbolStore;

namespace Domain.Entities;

public class TeacherPosition
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsCurrent { get; set; }
    public Department? Department { get; set; }
    public int DepartmentId { get; set; }
    public Position? Position { get; set; }
    public int PositionId { get; set; }
    public Teacher? Teacher { get; set; }
    public int TeacherId { get; set; }
    
}