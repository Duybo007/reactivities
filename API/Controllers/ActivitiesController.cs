using Application.Activities.Commands;
using Application.Activities.DTOs;
using Application.Queries;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ActivitiesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<List<Activity>>> GetActivities()
    {
        return await Mediator.Send(new GetActivityList.Query());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivityDetail(string id)
    {
        return HandleResult(await Mediator.Send(new GetActivityDetail.Query { Id = id }));
    }

    [HttpPost]
    // public async Task<ActionResult<string>> CreateActivity(CreateActivityDto activityDto)
    public async Task<ActionResult<string>> CreateActivity(CreateActivityDto activityDto)
    {
        // return HandleResult(await Mediator.Send(new CreateActivity.Command { ActivityDto = activityDto }));
        return await Mediator.Send(new CreateActivity.Command{ CreateActivityDto = activityDto });
    }

    [HttpPut]
    public async Task<ActionResult> EditActivity(EditActivityDto activity)
    {
        await Mediator.Send(new EditActivity.Command { ActivityDto = activity });

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteActivity(string id)
    {
        return HandleResult(await Mediator.Send(new DeleteActivity.Command { Id = id }));
    }
}
