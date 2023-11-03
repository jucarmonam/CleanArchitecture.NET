using Application.Common.Interfaces;
using Domain.Enums;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Application.Skills.Queries.GetSkillsByLevel;
public record GetAllSkillsByLevelQuery(SkillLevel Level) : IRequest<List<LevelResponse>>;

public class GetAllSkillsByLevelQueryHandler : IRequestHandler<GetAllSkillsByLevelQuery, List<LevelResponse>>
{
    private readonly ISkillRepository _skillRepository;
    public GetAllSkillsByLevelQueryHandler(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }

    public async Task<List<LevelResponse>> Handle(GetAllSkillsByLevelQuery request, CancellationToken cancellationToken)
    {
        return await _skillRepository.GetAllSkillsByLevel(request.Level);
    }
}
