namespace ScWebApi.Domain.DTOs
{
    public class SearchResultDTO : BaseDTO
    {
        public SearchResultDTO()
        {
        }

        public SearchResultDTO(string itemId, string name, int sortOrder) : base(sortOrder)
        {
            ItemId = itemId;
            Name = name;
        }

        public string ItemId { get; set; }
        public string Name { get; set; }
    }
}