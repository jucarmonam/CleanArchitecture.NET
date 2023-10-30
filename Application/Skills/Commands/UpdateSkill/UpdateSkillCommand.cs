using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Skills.Commands.UpdateSkill;
public record UpdateSkillCommand : IRequest
{
    public int SkillId { get; set; }
    public int ListId { get; init; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public SkillLevel Level { get; set; }
}

public class UpdateSkillCommandHandler : IRequestHandler<UpdateSkillCommand>
{
    private readonly ISkillRepository _skillRepository;

    public UpdateSkillCommandHandler(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }

    public async Task Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
    {
        var entity = await _skillRepository.GetByIdAsync(request.SkillId);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Skill), request.SkillId);
        }

        entity.ListId = request.ListId;
        entity.Name = request.Name ?? entity.Name;
        entity.Description = request.Description ?? entity.Description;
        if((int)request.Level > 0 && (int)request.Level < 5)
        {
            entity.Level = request.Level;
        }

        _skillRepository.Update(entity);

        await _skillRepository.SaveChangesAsync(cancellationToken);

    }
}