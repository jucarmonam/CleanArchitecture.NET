using Application.Common.Interfaces;
using Application.Skills.Queries.GetSkill;
using MediatR;

namespace Application.SkillLists.Queries.GetSkillList;
public record GetSkillListQuery(int Id) : IRequest<SkillListDto>;

public class GetSkillListQueryHandler : IRequestHandler<GetSkillListQuery, SkillListDto>
{
    private readonly ISkillListRepository _skillListRepository;
    public GetSkillListQueryHandler(ISkillListRepository skillListRepository) 
    {
        _skillListRepository = skillListRepository;
    }

    public async Task<SkillListDto> Handle(GetSkillListQuery request, CancellationToken cancellationToken)
    {
        var skillListQuery = await _skillListRepository.GetListByIdWithSkills(request.Id);

        var skillList = new SkillListDto(
                skillListQuery.Id,
                skillListQuery.Title,
                skillListQuery.Skills!
                .Select(s => new SkillDto(
                    s.Id,
                    s.ListId,
                    s.Name,
                    s.Description,
                    s.Level.ToString()
                    )).ToList());

        return skillList;
    }
}