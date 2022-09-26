﻿using Domain.Enums;

namespace Domain.EntitiesDto;

public class PositionDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool Enabled { get; set; }
    public PositionType PositionType { get; set; }
    public int DepartmentId { get; set; }
}