using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.SkillLists.Queries.GetSkillList;
public record GetSkillListQuery(int Id) : IRequest<SkillListDto>;

public class GetSkillListQueryHandler : IRequestHandler<GetSkillListQuery, SkillListDto>
{
    private readonly ISkillListRepository _skillListRepository;
    private readonly IMapper _mapper;
    public GetSkillListQueryHandler(ISkillListRepository skillListRepository, IMapper mapper) 
    {
        _skillListRepository = skillListRepository;
        _mapper = mapper;
    }

    public async Task<SkillListDto> Handle(GetSkillListQuery request, CancellationToken cancellationToken)
    {
        var skillListQuery = await _skillListRepository.GetListByIdWithSkills(request.Id);

        var skillList = _mapper.Map<SkillListDto>(skillListQuery);

        return skillList;
    }
}