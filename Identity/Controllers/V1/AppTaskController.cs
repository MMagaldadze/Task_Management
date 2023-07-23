using Application.AppTaskManagement.Commands.CreateAppTask;
using Application.AppTaskManagement.Commands.UpdateAppTaskStatus;
using Application.AppTaskManagement.Queries.GetAppTask;
using Application.AppTaskManagement.Queries.GetPendingAppTasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;

namespace Identity.Api.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AppTaskController : BaseApiController
    {
        [Authorize(Roles = UserRoleNames.Support)]
        [HttpGet("Pending")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<GetPendingAppTasksQueryRequest>> GetAllPendingsAsync()
        {
            var request = new GetPendingAppTasksQueryRequest();

            return Ok(await Mediator.Send(request));
        }

        [Authorize(Roles = UserRoleNames.Support)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetAppTaskQueryResponse>> GetAsync([FromRoute] Guid id)
        {
            var request = new GetAppTaskQueryRequest { Id = id };

            return Ok(await Mediator.Send(request));
        }

        [Authorize(Roles =UserRoleNames.User)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> CreateAsync([FromBody] CreateAppTaskCommand command)
        {
            _ = await Mediator.Send(command);

            return StatusCode(201);
        }

        [Authorize(Roles = UserRoleNames.Support)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateStatusAsync([FromRoute] Guid id, UpdateAppTaskStatusCommand command)
        {
            command.SetId(id);
            _ = await Mediator.Send(command);

            return Ok();
        }
    }
}
