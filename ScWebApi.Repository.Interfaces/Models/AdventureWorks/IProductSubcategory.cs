using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScWebApi.Repositories.Interfaces.Models.AdventureWorks
{
    public interface IProductSubcategory
    {
        int ProductSubcategoryID { get; set; }

        //public int ProductCategoryID { get; set; }

        [Required]
        [StringLength(50)]
        string Name { get; set; }

        //Guid rowguid { get; set; }

        //DateTime ModifiedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        ICollection<IProduct> Products { get; set; }

        IProductCategory ProductCategory { get; set; }
    }
}