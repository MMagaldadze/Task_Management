using Application.AppTaskManagement.Commands.CreateAppTask;
using Application.AppTaskManagement.Commands.UpdateAppTaskStatus;
using Application.AppTaskManagement.Queries.GetAppTask;
using Application.AppTaskManagement.Queries.GetAppTasks;
using Domain.AppTaskManagement.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Constants;

namespace Identity.Api.Controllers.V1
{
    [Route("api/v1/[controller]")]
    public class AppTaskController : BaseApiController
    {
        [Authorize(Roles = UserRoleNames.Support)]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<GetAppTasksQueryRequest>> GetAllAsync(Status? status)
        {
            var request = new GetAppTasksQueryRequest { Status = status };

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

        [Authorize(Roles = UserRoleNames.User)]
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
        [HttpPatch("{id}/change-status")]
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
