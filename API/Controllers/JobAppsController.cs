using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.BindingModels;
using Application.JobApps.Commands;
using Application.JobApps.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class JobAppsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobAppsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets a list of all qualified job applications.
        /// </summary>
        /// <param name="qualifiedOnly"></param>
        /// <returns>A list of qualified Job Applications.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobApp>>> GetJobApps(bool qualifiedOnly = false)
        {
            var query = new GetAllJobAppsQuery(qualifiedOnly);
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        /// <summary>
        /// Creates a new job application
        /// </summary>
        /// <param name="CreateJobAppCommand"></param>
        [HttpPost]
        public async Task<ActionResult<JobAppSubmittedResponse>> CreateJobApp([FromBody] CreateJobAppCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetJobApps), result);
        }
    }
}
