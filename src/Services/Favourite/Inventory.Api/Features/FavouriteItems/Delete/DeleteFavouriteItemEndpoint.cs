namespace Favourite.Api.Features.FavouriteItems.Delete;

public record CreateFavouriteItemRequest(Guid ProductId, int VariantId, DateTime LikedDate);
public record CreateFavouriteItemResponse
    (Guid Id,
    Guid ProductId,
    int VariantId,
    DateTime LikedDate);
public class CreateFavouriteItemEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/favourite", async (CreateFavouriteItemRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateFavouriteItemCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateFavouriteItemResponse>();
            return Results.Created($"api/favourite/{response.Id}", response);
        })
        .WithName("CreateFavouriteItem")
        .Produces<CreateFavouriteItemResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create favourite item.");
    }
}
