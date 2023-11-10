using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.SkillLists.Commands.CreateSkillList;

public record  CreateSkillListCommand : IRequest<(Result result, int? listId)>
{
    public required string Title { get; init; }
}
public class CreateSkillListCommandHandler : IRequestHandler<CreateSkillListCommand, (Result result, int? listId)>
{
    private readonly ISkillListRepository _skillListRepository;

    public CreateSkillListCommandHandler(ISkillListRepository skillListRepository)
    {
        _skillListRepository = skillListRepository;
    }

    public async Task<(Result result, int? listId)> Handle(CreateSkillListCommand request, CancellationToken cancellationToken)
    {
        var isUnique = await _skillListRepository.BeUniqueTitle(request.Title, cancellationToken);
        if (isUnique is false)
        {
            return (Result.Failure(new []{"There is already a list with this title"}), null);
        }
        
        var entity = new SkillList
        {
            Title = request.Title
        };

        _skillListRepository.Add(entity);

        await _skillListRepository.SaveChangesAsync(cancellationToken);

        return (Result.Success(), entity.Id);
    }
}