
using Application.Core;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Commands;

public class UpdateAttendence
{
    public class Command : IRequest<Result<Unit>>
    {
        public required string Id { get; set; }
    }

    public class Handler(IUseAccessor userAccessor, AppDbContext dbContext) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await dbContext.Activities
                .Include(a => a.Attendees)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (activity == null) return Result<Unit>.Failure("Activity not found", 404);

            var user = await userAccessor.GetUserAsync();
            // Check in the current Attendees list of this activity if the current user who making the request is in there or not
            var attendence = activity.Attendees.FirstOrDefault(x => x.UserId == user.Id);
            // isHost true if the current user is in the Attensees list and Ishost = true
            var isHost = activity.Attendees.Any(x => x.IsHost && x.User.Id == user.Id);
            // If current user in the Attendees list, check if user IsHost
            if (attendence != null)
            {
                if (isHost) activity.Iscanceled = !activity.Iscanceled; // Cancel activity if current user is host
                else activity.Attendees.Remove(attendence); // Remove current user from Attendees list if not host
            }
            else
            {
                activity.Attendees.Add(new Domain.ActivityAttendee
                {
                    UserId = user.Id,
                    ActivityId = activity.Id,
                    IsHost = false
                });
            }

            var result = await dbContext.SaveChangesAsync(cancellationToken) > 0;

            return result ?
                Result<Unit>.Success(Unit.Value)
                : Result<Unit>.Failure("Problem updating the DB", 400);
        }
    }
}
