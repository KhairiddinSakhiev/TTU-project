using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

public class Teacher
{
    public int Id { get; set; }
    [MaxLength(30),Required]
    public string? FirstName { get; set; }
    [MaxLength(30),Required]
    public string? LastName { get; set; }
    [MaxLength(30),Required]
    public string? MiddleName { get; set; }
    public string? Description { get; set; }
    [Required]
    public string? Address { get; set; }
    [Required]
    public string? PhoneNumber { get; set; }
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Image { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public Status Status { get; set; }
    public Degree Degree { get; set; }
    public virtual List<Position>? Positions { get; set; }
}