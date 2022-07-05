namespace Domain.EntitiesDto;

public class TeacherPosition
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsCurrent { get; set; }
    public int TeacherId { get; set; }
    public int PositionId { get; set; }
}