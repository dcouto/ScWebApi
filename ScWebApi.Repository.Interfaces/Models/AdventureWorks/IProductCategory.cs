using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScWebApi.Repositories.Interfaces.Models.AdventureWorks
{
    public interface IProductCategory
    {
        int ProductCategoryID { get; set; }

        [Required]
        [StringLength(50)]
        string Name { get; set; }

        //Guid rowguid { get; set; }

        //DateTime ModifiedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        ICollection<IProductSubcategory> ProductSubcategories { get; set; } 
    }
}