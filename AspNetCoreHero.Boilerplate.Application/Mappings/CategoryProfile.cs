
using AspNetCoreHero.Boilerplate.Application.Features.Categories.Commands.Create;
using AspNetCoreHero.Boilerplate.Application.Features.Categories.Queries.GetAllCached;
using AspNetCoreHero.Boilerplate.Application.Features.Categories.Queries.GetById;
using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using AutoMapper;

namespace AspNetCoreHero.Boilerplate.Application.Mappings
{
    internal class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryCommand, Category>().ReverseMap();
            CreateMap<GetCategoryByIdResponse, Category>().ReverseMap();
            CreateMap<GetAllCategoriesCachedResponse, Category>().ReverseMap();
        }
    }
}