using Domain.Enums;

namespace Domain.EntitiesDto;

public class TeacherDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? MiddleName { get; set; }
    public string? Description { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Image { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public Status Status { get; set; }
    public Degree Degree { get; set; }
}