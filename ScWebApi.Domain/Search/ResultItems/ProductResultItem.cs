using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;

namespace ScWebApi.Domain.Search.ResultItems
{
    public class ProductResultItem : SearchResultItem
    {
        [IndexField("product_subcategory")]
        public ID ProductSubcategory { get; set; }
    }
}