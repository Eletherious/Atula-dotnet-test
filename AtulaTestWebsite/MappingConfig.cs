using Mapster;
using AtulaTestWebsite.Models.DTOs;
using AtulaTestWebsite.Models.Modles;

public static class MappingConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<Product, ProductDTO>.NewConfig();
    }
}
