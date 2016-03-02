namespace ScWebApi.Repositories.Models.SitecoreEntityService
{
    public class Product : BaseEntity
    {
        public Product()
        {
        }

        public Product(string id, string url, string legacyName, double listPrice, string description)
            : base(id, url, legacyName)
        {
            ListPrice = listPrice;
            Description = description;
        }

        public double ListPrice { get; set; }
        public string Description { get; set; }
    }
}