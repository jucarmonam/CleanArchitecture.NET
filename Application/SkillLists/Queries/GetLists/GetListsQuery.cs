using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.SkillLists.Queries.GetLists;
public record GetListsQuery : IRequest<List<SkillListDto>>;

public class GetListsQueryHandler : IRequestHandler<GetListsQuery, List<SkillListDto>>
{
    private readonly ISkillListRepository _skillListRepository;
    private readonly IMapper _mapper;

    public GetListsQueryHandler(ISkillListRepository skillListRepository, IMapper mapper)
    {
        _skillListRepository = skillListRepository;
        _mapper = mapper;
    }

    public async Task<List<SkillListDto>> Handle(GetListsQuery request, CancellationToken cancellationToken)
    {
        List<SkillList> listsQuery = await _skillListRepository.GetAllListsWithSkills();

        var lists = listsQuery.Select(_mapper.Map<SkillListDto>).ToList();

        return lists;
    }
}