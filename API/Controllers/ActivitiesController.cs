using Application.Activities.Commands;
using Application.Activities.DTOs;
using Application.Queries;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ActivitiesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<ActivityDto>>> GetActivities()
    {
        return await Mediator.Send(new GetActivityList.Query());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ActivityDto>> GetActivityDetail(string id)
    {
        return HandleResult(await Mediator.Send(new GetActivityDetail.Query { Id = id }));
    }

    [HttpPost]
    // public async Task<ActionResult<string>> CreateActivity(CreateActivityDto activityDto)
    public async Task<ActionResult<string>> CreateActivity(CreateActivityDto activityDto)
    {
        // return HandleResult(await Mediator.Send(new CreateActivity.Command { ActivityDto = activityDto }));
        return await Mediator.Send(new CreateActivity.Command { CreateActivityDto = activityDto });
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "IsActivityHost")]
    public async Task<ActionResult> EditActivity(string id, EditActivityDto editActivityDto)
    {
        editActivityDto.Id = id;
        await Mediator.Send(new EditActivity.Command { ActivityDto = editActivityDto });

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "IsActivityHost")]
    public async Task<ActionResult> DeleteActivity(string id)
    {
        return HandleResult(await Mediator.Send(new DeleteActivity.Command { Id = id }));
    }

    [HttpPost("{id}/attend")]
    public async Task<ActionResult> Attend(string id)
    {
        return HandleResult(await Mediator.Send(new UpdateAttendence.Command { Id = id }));
    }
}
