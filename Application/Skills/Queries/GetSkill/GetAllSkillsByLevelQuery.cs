using Application.Common.Interfaces;
using Domain.Enums;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Application.Skills.Queries.GetSkill;
public record GetAllSkillsByLevelQuery(SkillLevel Level) : IRequest<List<LevelResponse>>;

public class GetAllSkillsByLevelQueryHandler : IRequestHandler<GetAllSkillsByLevelQuery, List<LevelResponse>>
{
    private readonly IApplicationDbContext _context;
    public GetAllSkillsByLevelQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<LevelResponse>> Handle(GetAllSkillsByLevelQuery request, CancellationToken cancellationToken)
    {
        var level = new SqlParameter("@Level", request.Level);
        var result = await _context.Set<LevelResponse>().FromSqlRaw("uspExpertSkills @Level", level).ToListAsync(cancellationToken);

        return result ?? throw new Exception();
    }
}
