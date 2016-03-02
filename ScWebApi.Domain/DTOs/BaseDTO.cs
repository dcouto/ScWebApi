namespace ScWebApi.Domain.DTOs
{
    public class BaseDTO
    {
        public BaseDTO()
        {
        }

        public BaseDTO(int sortOrder)
        {
            SortOrder = sortOrder;
        }

        public int SortOrder { get; set; }
    }
}