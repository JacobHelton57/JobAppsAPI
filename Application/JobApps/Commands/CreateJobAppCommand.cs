using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.BindingModels;
using Application.Data;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.JobApps.Commands
{
    public class CreateJobAppCommand : IRequest<JobAppSubmittedResponse>
    {
        public CreateJobAppCommand(string name, List<QuestionRequest> questions)
        {
            Name = name;
            Questions = questions;
        }

        public string Name { get; set; }

        public List<QuestionRequest> Questions { get; set; }
    }

    public class QuestionRequest
    {
        public int Id { get; set; }

        public string Answer { get; set; }
    }

    public class CreateJobAppCommandHandler : IRequestHandler<CreateJobAppCommand, JobAppSubmittedResponse>
    {
        private readonly JobAppsDbContext _context;

        public CreateJobAppCommandHandler(JobAppsDbContext context)
        {
            _context = context;
        }

        public async Task<JobAppSubmittedResponse> Handle(CreateJobAppCommand request, CancellationToken cancellationToken)
        {
            var response = new JobApp();

            response.Name = request.Name;

            var criterias = await _context.JobCriterias.ToListAsync(cancellationToken);
            var questions = request.Questions;

            response.Answers = questions.Select(q => new FormInput
            {
                Answer = q.Answer,
                Criteria = criterias.FirstOrDefault(c => c.Id == q.Id)
            }).ToList();

            var criteriaAndResponses = from criteria in criterias
                                       join answer in response.Answers
                                       on criteria equals answer.Criteria into joined
                                       from a in joined.DefaultIfEmpty()
                                       select new
                                       {
                                           Response = a?.Answer,
                                           Correct = criteria.Answer
                                       };

            var isQualified = criteriaAndResponses.All(x => x.Response == x.Correct);

            _context.JobApps.Add(response);
            await _context.SaveChangesAsync(cancellationToken);

            return new JobAppSubmittedResponse { isQualified = isQualified, JobApp = response };
        }
    }
}
