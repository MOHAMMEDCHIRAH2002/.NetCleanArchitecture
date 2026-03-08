using System.Reflection.Metadata;
using FootCourtBooking.Api.Contracts;
using FootCourtBooking.Application.Abstractions.Persistence;
using FootCourtBooking.Application.Bookings.CancelBooking;
using FootCourtBooking.Application.Bookings.ConfirmBooking;
using FootCourtBooking.Application.Bookings.CreateBooking;
using FootCourtBooking.Application.Bookings.GetBookingById;
using FootCourtBooking.Domain.Common;
using FootCourtBooking.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<IBookingRepository, InMemoryBookingRepository>();

builder.Services.AddScoped<CreateBookingHandler>();
builder.Services.AddScoped<GetBookingByIdHandler>();
builder.Services.AddScoped<ConfirmBookingHandler>();
builder.Services.AddScoped<CancelBookingHandler>();

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

        var result = await handler.Handle(command, cancellationToken);

        return Results.Created($"/bookings/{result.Id}", result);
    }
    catch (DomainException ex)
    {

        return Results.BadRequest(new { error = ex.Message });
    }
}
);

app.MapGet("/bookings/{id:guild}", async (
    Guid id,
    GetBookingByIdHandler handler,
    CancellationToken cancellationToken
    ) =>
{
try
{
    var query=new GetBookingByIdQuery(id);

    var result=await handler.Handle(query, cancellationToken);
    return Results.Ok(result);
}
catch (Exception ex)
{
    
    return HandleException(ex);
}
}
);


app.MapPut("/bookings/{id:guid}/confirm",async(
Guid guid,
ConfirmBookingHandler handler,
CancellationToken cancellationToken
)=>
{
try
{
    var command = new ConfirmBookingCommand(guid);
    var result = await handler.Handle(command, cancellationToken);
    return Results.Ok(result);
}
catch (Exception exe)
{
    
    return HandleException(exe);
}




});

app.MapPut("/bookings/{id:guid}/cancel",async(
Guid guid,CancelBookingHandler handler,
CancellationToken cancellationToken) =>
{

    try
    {
        var command=new CancelBookingCommand(guid);
        var result=await handler.Handle(command, cancellationToken);
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        return HandleException(ex);
    }
});

app.Run();

static IResult HandleException(Exception ex)
{
    if (ex is NotFoundException)
        return Results.NotFound(new { error = ex.Message });

    if (ex is DomainException)
        return Results.BadRequest(new { error = ex.Message });

    return Results.Problem("An unexpected error occurred.");
}