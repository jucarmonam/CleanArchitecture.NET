using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SkillLists.Commands.UpdateSkillList;
public record UpdateSkillListCommand : IRequest
{
    public int ListId { get; init; }

    public string? Title { get; init; }
}

public class UpdateSkillListCommandHandler : IRequestHandler<UpdateSkillListCommand>
{
    private readonly ISkillListRepository _skillListRepository;

    public UpdateSkillListCommandHandler(ISkillListRepository skillListRepository)
    {
        _skillListRepository = skillListRepository;
    }

    public async Task Handle(UpdateSkillListCommand request, CancellationToken cancellationToken)
    {
        var entity = await _skillListRepository.GetByIdAsync(request.ListId);

        if (entity == null)
        {
            throw new NotFoundException(nameof(SkillList), request.ListId);
        }

        entity.Title = request.Title;

        _skillListRepository.Update(entity);//Technically this is unnecessary

        await _skillListRepository.SaveChangesAsync(cancellationToken);

    }
}