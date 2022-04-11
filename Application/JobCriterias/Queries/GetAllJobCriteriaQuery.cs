using System;
using Domain.Entities;
using System.Collections.Generic;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Application.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.JobCriterias.Queries
{
    public class GetAllJobCriteriaQuery : IRequest<List<JobCriteria>>
    {
        public GetAllJobCriteriaQuery()
        {
        }
    }

    public class GetAllJobCriteriaQueryHandler : IRequestHandler<GetAllJobCriteriaQuery, List<JobCriteria>>
    {
        private readonly JobAppsDbContext _context;

        public GetAllJobCriteriaQueryHandler(JobAppsDbContext context)
        {
            _context = context;
        }

        public async Task<List<JobCriteria>> Handle(GetAllJobCriteriaQuery request, CancellationToken cancellationToken)
        {
            return await _context.JobCriterias.ToListAsync();
        }
    }
}
