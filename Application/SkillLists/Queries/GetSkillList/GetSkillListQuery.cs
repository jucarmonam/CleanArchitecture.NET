using Application.Common.Interfaces;
using Application.SkillLists.Commands.CreateSkillList;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SkillLists.Queries.GetSkillList;
public record GetSkillListQuery(int Id) : IRequest<SkillListDto>;

public class GetSkillListQueryHandler : IRequestHandler<GetSkillListQuery, SkillListDto>
{
    private readonly IApplicationDbContext _context;
    public GetSkillListQueryHandler(IApplicationDbContext context) 
    {
        _context = context; 
    }

    public async Task<SkillListDto> Handle(GetSkillListQuery request, CancellationToken cancellationToken)
    {
        var skillList = await _context
            .SkillLists
            .Where(p => p.Id == request.Id)
            .Select(p => new SkillListDto(
                p.Id,
                p.Title,
                p.Skills))
            .FirstOrDefaultAsync(cancellationToken);

        return skillList ?? throw new Exception();
    }
}