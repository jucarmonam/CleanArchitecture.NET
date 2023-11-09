using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Skills.Commands.DeleteSkill;
public record DeleteSkillCommand(int SkillId) : IRequest;

public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand>
{
    private readonly ISkillRepository _skillRepository;

    public DeleteSkillCommandHandler(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }

    public async Task Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
    {
        var entity = await _skillRepository.GetByIdAsync(request.SkillId);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Skill), request.SkillId);
        }

        _skillRepository.Remove(entity);

        await _skillRepository.SaveChangesAsync(cancellationToken);
    }
}
