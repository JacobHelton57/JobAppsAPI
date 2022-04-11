using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.JobCriterias.Commands;
using Application.JobCriterias.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobCriteriasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobCriteriasController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Gets a list of all job criteria.
        /// </summary>
        /// <returns>A list of job criteria.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobCriteria>>> GetJobCriterias()
        {
            var query = new GetAllJobCriteriaQuery();
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        /// <summary>
        /// Creates a new criteria for a job application
        /// </summary>
        /// <param name="CreateJobCriteriaCommand"></param>
        [HttpPost]
        public async Task<ActionResult<JobCriteria>> CreateJobCriteria([FromBody] CreateJobCriteriaCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetJobCriterias), result);
        }
    }
}
