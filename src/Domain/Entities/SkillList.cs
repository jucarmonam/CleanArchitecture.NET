using Domain.Common;

namespace Domain.Entities;
public class SkillList : BaseEntity
{
    public string? Title { get; set; }

    public IList<Skill>? Skills { get; private set; } = new List<Skill>();
}
