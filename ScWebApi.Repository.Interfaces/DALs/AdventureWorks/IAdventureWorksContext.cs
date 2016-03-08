using System;
using System.Data.Entity;
using ScWebApi.Repository.Models.AdventureWorks;

namespace ScWebApi.Repository.Interfaces.DALs.AdventureWorks
{
    public interface IAdventureWorksContext : IDisposable
    {
        DbSet<Product> Products { get; set; }
        DbSet<ProductCategory> ProductCategories { get; set; }
        DbSet<vProductAndDescription> vProductAndDescriptions { get; set; }
    }
}