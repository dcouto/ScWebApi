namespace ScWebApi.Domain.DTOs
{
    public class SearchFacetDTO : BaseDTO
    {
        public SearchFacetDTO()
        {
        }

        public SearchFacetDTO(string name, int count, int sortOrder) : base(sortOrder)
        {
            Name = name;
            Count = count;
        }

        public string Name { get; set; }
        public int Count { get; set; }
    }
}