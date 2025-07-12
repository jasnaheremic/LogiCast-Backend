﻿namespace LogiCast.Domain.Models;

public abstract class BaseModel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}