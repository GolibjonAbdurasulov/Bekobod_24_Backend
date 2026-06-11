using Core.Entities;
using WebAPI.DTOs;

namespace WebAPI.Mapping;

public class StoreMapper
{
    public static StoreDto ToDto(Store store)
    {
        return new StoreDto
        {
            Id = store.Id,
            Name = store.Name,
            Type = store.Type.ToString(),
            ImageUrl = store.ImageUrl
        };
    }
}