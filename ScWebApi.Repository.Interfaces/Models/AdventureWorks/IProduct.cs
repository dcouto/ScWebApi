using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScWebApi.Repositories.Interfaces.Models.AdventureWorks
{
    public interface IProduct
    {
        int ProductID { get; set; }

        [Required]
        [StringLength(50)]
        string Name { get; set; }

        //[Required]
        //[StringLength(25)]
        //string ProductNumber { get; set; }

        //bool MakeFlag { get; set; }

        //bool FinishedGoodsFlag { get; set; }

        //[StringLength(15)]
        //string Color { get; set; }

        //short SafetyStockLevel { get; set; }

        //short ReorderPoint { get; set; }

        [Column(TypeName = "money")]
        decimal StandardCost { get; set; }

        [Column(TypeName = "money")]
        decimal ListPrice { get; set; }

        //[StringLength(5)]
        //string Size { get; set; }

        [StringLength(3)]
        string SizeUnitMeasureCode { get; set; }

        [StringLength(3)]
        string WeightUnitMeasureCode { get; set; }

        decimal? Weight { get; set; }

        //int DaysToManufacture { get; set; }

        [StringLength(2)]
        string ProductLine { get; set; }

        [StringLength(2)]
        string Class { get; set; }

        [StringLength(2)]
        string Style { get; set; }

        int? ProductSubcategoryID { get; set; }

        //int? ProductModelID { get; set; }

        //DateTime SellStartDate { get; set; }

        //DateTime? SellEndDate { get; set; }

        //DateTime? DiscontinuedDate { get; set; }

        //Guid rowguid { get; set; }

        //DateTime ModifiedDate { get; set; }

        //virtual ProductModel ProductModel { get; set; }

        //virtual ProductSubcategory ProductSubcategory { get; set; }
    }
}