﻿using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

public class Position
{
    public int Id { get; set; }
    [MaxLength(30), Required]
    public string? Name { get; set; }
    public bool Enabled { get; set; }
    public PositionType PositionType { get; set; }
}