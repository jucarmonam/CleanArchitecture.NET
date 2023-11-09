using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.SkillLists.Commands.DeleteSkillList;
public record DeleteSkillListCommand(int ListId) : IRequest;

public class DeleteSkillListCommandHandler : IRequestHandler<DeleteSkillListCommand>
{
    private readonly ISkillListRepository _skillListRepository;

    public DeleteSkillListCommandHandler(ISkillListRepository skillListRepository)
    {
        _skillListRepository = skillListRepository;
    }

    public async Task Handle(DeleteSkillListCommand request, CancellationToken cancellationToken)
    {
        var entity = await _skillListRepository.GetByIdAsync(request.ListId);

        if (entity == null)
        {
            throw new NotFoundException(nameof(SkillList), request.ListId);
        }

        _skillListRepository.Remove(entity);

        await _skillListRepository.SaveChangesAsync(cancellationToken);
    }
}
