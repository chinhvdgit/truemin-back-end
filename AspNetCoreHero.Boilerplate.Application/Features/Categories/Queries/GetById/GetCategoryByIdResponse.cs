namespace AspNetCoreHero.Boilerplate.Application.Features.Categories.Queries.GetById
{
    public class GetCategoryByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Tax { get; set; }
        public string Description { get; set; }
    }
}
