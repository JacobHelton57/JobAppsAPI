using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Application.JobApps.Queries
{
    public class GetAllJobAppsQuery : IRequest<List<JobApp>>
    {
        public GetAllJobAppsQuery(bool qualifiedOnly = true)
        {
            QualifiedOnly = qualifiedOnly;
        }

        public bool QualifiedOnly { get; set; }
    }

    public class GetAllJobAppsQueryHandler : IRequestHandler<GetAllJobAppsQuery, List<JobApp>>
    {
        private readonly JobAppsDbContext _context;
        // private readonly IMapper _mapper;

        public GetAllJobAppsQueryHandler(JobAppsDbContext context)
        {
            _context = context;
        }

        public async Task<List<JobApp>> Handle(GetAllJobAppsQuery request, CancellationToken cancellationToken)
        {
            var applicants = await _context.JobApps.Include(applicant => applicant.Answers).ToListAsync();

            if (!request.QualifiedOnly)
            {
                return applicants;
            }

            var criterias = await _context.JobCriterias.ToListAsync();

            // TODO - DRY up this logic - duplicated in create job app command as well
            var qualifiedApplicants = applicants.Where(applicant =>
            {
                var criteriaAndResponses = from criteria in criterias
                                           join answer in applicant.Answers
                                           on criteria equals answer.Criteria into joined
                                           from a in joined.DefaultIfEmpty()
                                           select new
                                           {
                                               Response = a?.Answer,
                                               Correct = criteria.Answer
                                           };

                bool isQualified = criteriaAndResponses.All(x => x.Response == x.Correct);

                return isQualified;
            }).ToList();

            return qualifiedApplicants;
        }
    }
}
