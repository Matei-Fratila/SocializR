﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MushroomsGuide.API.Model;
using MushroomsGuide.API.Repositories;
using MushroomsGuide.API.ViewModels;

namespace MushroomsGuide.API.Apis;

public static class MushroomsApi
{
    public static IEndpointRouteBuilder MapMushroomsApi(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/{id:int}", GetMushroomByIdAsync);
        builder.MapPut("/", UpdateMushroomAsync);
        builder.MapGet("/search", SearchMushroomsAsync);
        builder.MapGet("/filtersearch", FilterSearchMushroomsAsync);
        builder.MapGet("/", GetPaginatedAsync);

        return builder;
    }

    public static async Task<Results<Ok<Mushroom>, NotFound>> GetMushroomByIdAsync([FromServices] IMushroomsRepository repository, [FromRoute] int id)
    {
        if(id < 12)
        {
            return TypedResults.NotFound();
        }

        var mushroom = await repository.GetAsync(id);

        if(mushroom == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(mushroom);
    }

    public static async Task<IResult> UpdateMushroomAsync([FromServices] IMushroomsRepository repository, [FromBody] Mushroom mushroom)
    {
        if (mushroom.Id < 12)
        {
            return TypedResults.BadRequest();
        }

        var isSuccessful = await repository.UpdateAsync(mushroom);

        return isSuccessful ? TypedResults.Ok(mushroom) : TypedResults.Problem(new ProblemDetails { Status = 500, Detail = "Something went wrong"});
    }

    public static async Task<IResult> SearchMushroomsAsync([FromServices] IMushroomsRepository repository, 
        [FromQuery] string term)
    {
        var mushrooms = await repository.SearchAsync(term);
        return TypedResults.Ok(mushrooms.Select(x => new
        { 
            Id = x.Id, 
            Nume = !string.IsNullOrEmpty(x.DenumirePopulara) ? $"{x.DenumirePopulara} ({x.Denumire})" : x.Denumire
        }));
    }

    public static async Task<IResult> FilterSearchMushroomsAsync([FromServices] IMushroomsRepository repository,
        [AsParameters] SearchFilters filters)
    {
        var mushrooms = await repository.FilterSearchAsync(filters);
        return TypedResults.Ok(mushrooms);
    }

    public static async Task<IResult> GetPaginatedAsync([FromServices] IMushroomsRepository repository,
    [FromQuery] int pageIndex, 
    [FromQuery] int pageSize)
    {
        var mushrooms = await repository.GetPaginatedAsync(pageIndex, pageSize);
        return TypedResults.Ok(mushrooms);
    }
}
