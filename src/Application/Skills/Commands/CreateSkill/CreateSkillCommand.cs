using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Skills.Commands.CreateSkill;
public record CreateSkillCommand : IRequest<Skill>
{
    public int ListId { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public SkillLevel Level { get; init; }
}

public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, Skill>
{
    private readonly ISkillRepository _skillRepository;

    public CreateSkillCommandHandler(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }

    public async Task<Skill> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
    {
        var entity = new Skill
        {
            ListId = request.ListId,
            Name = request.Name,
            Description = request.Description,
            Level = request.Level
        };

        _skillRepository.Add(entity);

        await _skillRepository.SaveChangesAsync(cancellationToken);

        return entity;
    }
}
