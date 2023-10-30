using Application.Common.Interfaces;
using Application.Skills.Queries.GetSkill;
using AutoMapper;
using Domain.Entities;
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
    private readonly ISkillListRepository _skillListRepository;

    public GetListsQueryHandler(ISkillListRepository skillListRepository)
    {
        _skillListRepository = skillListRepository;
    }

    public async Task<List<SkillListDto>> Handle(GetListsQuery request, CancellationToken cancellationToken)
    {
        List<SkillList> listsQuery = await _skillListRepository.GetAllListsWithSkills();

        var lists = listsQuery
            .Select(p => new SkillListDto(
                p.Id,
                p.Title,
                p.Skills!.Select(s => new SkillDto(
                    s.Id,
                    s.ListId,
                    s.Name,
                    s.Description,
                    s.Level.ToString())
                ).ToList()
                )).ToList();

        return lists;
    }
}