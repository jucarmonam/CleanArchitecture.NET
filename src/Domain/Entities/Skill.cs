﻿using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;
public class Skill : BaseEntity
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public SkillLevel Level { get; set; }
    public int ListId { get; set; }

    public SkillList? SkillList { get; set; }
}
