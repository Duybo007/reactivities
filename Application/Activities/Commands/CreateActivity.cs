using System;
using Application.Activities.DTOs;
using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.Commands;

public class CreateActivity
{
    // public class Command : IRequest<Result<string>>
    public class Command : IRequest<string>
    {
        // public required CreateActivityDto ActivityDto { get; set; }
        public required CreateActivityDto CreateActivityDto { get; set; }
    }
    // public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, Result<string>>
    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, string>
    {
        // public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = mapper.Map<Activity>(request.CreateActivityDto);
            // context.Activities.Add(activity);
            context.Activities.Add(activity);

            var res = await context.SaveChangesAsync(cancellationToken) > 0;

            // if (!res) return Result<string>.Failure("Failed to CREATE Activity", 400);

            // return Result<string>.Success(activity.Id);
            return activity.Id;
        }
    }
}
