using FootCourtBooking.Api.Contracts;
using FootCourtBooking.Application.Abstractions.Persistence;
using FootCourtBooking.Application.Bookings.CreateBooking;
using FootCourtBooking.Domain.Common;
using FootCourtBooking.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<IBookingRepository, InMemoryBookingRepository>();
builder.Services.AddScoped<CreateBookingHandler>();

var app = builder.Build();

app.MapPost("/bookings", async (
    CreateBookingRequest request,
    CreateBookingHandler handler,
    CancellationToken cancellationToken
    ) =>
{
    try
    {
        var command = new CreateBookingCommand(
          request.CourtId,
    request.CustomerName,
    request.StartUtc
        );

        var result=await handler.Handle(command, cancellationToken);

        return Results.Created($"/bookings/{result.Id}", result);
    }
    catch (DomainException ex)
    {

        return Results.BadRequest(new {error=ex.Message});
    }
}
);

app.Run();