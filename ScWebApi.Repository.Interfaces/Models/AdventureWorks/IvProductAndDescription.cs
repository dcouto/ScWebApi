using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScWebApi.Repositories.Interfaces.Models.AdventureWorks
{
    public interface IvProductAndDescription
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        int ProductID { get; set; }

        //[Key]
        //[Column(Order = 1)]
        //[StringLength(50)]
        //string Name { get; set; }

        //[Key]
        //[Column(Order = 2)]
        //[StringLength(50)]
        //string ProductModel { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(6)]
        string CultureID { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(400)]
        string Description { get; set; } 
    }
}