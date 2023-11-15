using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.Skills.Queries.GetSkillsWithPagination;
public record GetSkillsQuery : IRequest<PaginatedList<SkillDto>>
{
    public int ListId { get; init; }
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetSkillsQueryHandler : IRequestHandler<GetSkillsQuery, PaginatedList<SkillDto>>
{
    private readonly ISkillRepository _skillRepository;
    private readonly IMapper _mapper;
    public GetSkillsQueryHandler(ISkillRepository skillRepository, IMapper mapper)
    {
        _skillRepository = skillRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<SkillDto>> Handle(GetSkillsQuery request, CancellationToken cancellationToken)
    {
        var skillsQuery = _skillRepository.GetAllAsync(s => s.ListId == request.ListId);

        //var skillsResponseQuery = skillsQuery.Select(sq => _mapper.Map<SkillDto>(sq));
        //This way only works in Queryables is faster because works in db level but with large amounts of data
        var skillsResponseQuery = skillsQuery.ProjectTo<SkillDto>(_mapper.ConfigurationProvider);

        var skills = await PaginatedList<SkillDto>.CreateAsync(
            skillsResponseQuery, 
            request.Page, 
            request.PageSize);

        return skills;
    }
}