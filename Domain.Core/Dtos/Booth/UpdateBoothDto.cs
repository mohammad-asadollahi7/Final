﻿
using Domain.Core.Entities;

namespace Domain.Core.Dtos.Booth;

public class UpdateBoothDto
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; }
    public virtual Picture Picture { get; set; } = null!;
}
