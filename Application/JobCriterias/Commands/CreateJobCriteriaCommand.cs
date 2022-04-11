using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Data;
using Domain.Entities;
using MediatR;

namespace Application.JobCriterias.Commands
{
    public class CreateJobCriteriaCommand : IRequest<JobCriteria>
    {
        public CreateJobCriteriaCommand(string question, string answer)
        {
            Question = question;
            Answer = answer;
        }

        public string Question { get; set; }

        public string Answer { get; set; }
    }

    public class CreateJobCriteriaCommandHandler : IRequestHandler<CreateJobCriteriaCommand, JobCriteria>
    {
        private readonly JobAppsDbContext _context;

        public CreateJobCriteriaCommandHandler(JobAppsDbContext context)
        {
            _context = context;
        }

        public async Task<JobCriteria> Handle(CreateJobCriteriaCommand request, CancellationToken cancellationToken)
        {
            // TODO - Abstract mapping out using Automapper or custom mappers
            var criteria = new JobCriteria
            {
                Question = request.Question,
                Answer = request.Answer
            };

            _context.JobCriterias.Add(criteria);
            await _context.SaveChangesAsync();

            return criteria;
        }
    }
}
