using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Skills.Queries.GetSkill;
public record GetSkillsQuery : IRequest<PaginatedList<SkillDto>>
{
    public int ListId { get; init; }
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetSkillsQueryHandler : IRequestHandler<GetSkillsQuery, PaginatedList<SkillDto>>
{
    private readonly IApplicationDbContext _context;
    public GetSkillsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<SkillDto>> Handle(GetSkillsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Skill> skillsQuery = _context.Skills.Where(s => s.ListId == request.ListId);

        var skillsResponseQuery = skillsQuery
            .Select(s => new SkillDto(
                s.Id, 
                s.ListId, 
                s.Name, 
                s.Description, 
                s.Level.ToString()));

        var skills = await PaginatedList<SkillDto>.CreateAsync(
            skillsResponseQuery, 
            request.Page, 
            request.PageSize);

        return skills ?? throw new Exception();
    }
}