using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SkillLists.Queries.GetSkillList;
public record GetListsQuery : IRequest<List<SkillListDto>>;

public class GetListsQueryHandler : IRequestHandler<GetListsQuery, List<SkillListDto>>
{
    private readonly IApplicationDbContext _context;

    public GetListsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<SkillListDto>> Handle(GetListsQuery request, CancellationToken cancellationToken)
    {
        var lists = await _context
            .SkillLists
            .AsNoTracking()
            .Select(p => new SkillListDto(
                p.Id,
                p.Title,
                p.Skills))
            .ToListAsync(cancellationToken);

        return lists ?? throw new Exception();
    }
}