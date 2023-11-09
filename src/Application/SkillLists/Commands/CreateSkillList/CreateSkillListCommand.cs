using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.SkillLists.Commands.CreateSkillList;

public record  CreateSkillListCommand : IRequest<SkillList>
{
    public string? Title { get; init; }
}
public class CreateSkillListCommandHandler : IRequestHandler<CreateSkillListCommand, SkillList>
{
    private readonly ISkillListRepository _skillListRepository;

    public CreateSkillListCommandHandler(ISkillListRepository skillListRepository)
    {
        _skillListRepository = skillListRepository;
    }

    public async Task<SkillList> Handle(CreateSkillListCommand request, CancellationToken cancellationToken)
    {
        var entity = new SkillList
        {
            Title = request.Title
        };

        _skillListRepository.Add(entity);

        await _skillListRepository.SaveChangesAsync(cancellationToken);

        return entity;
    }
}