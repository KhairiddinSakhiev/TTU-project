﻿namespace Domain.EntitiesDto;

public class TeacherPositionDto
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsCurrent { get; set; }
    public int PositionId { get; set; }
    public int TeacherId { get; set; }
}