using SoftTest.Models;
using Microsoft.EntityFrameworkCore;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SoftTest.Data;

public class DataContext : IdentityDbContext<IdentityUser>
{
    public DataContext()
    {

    }
    public DataContext(DbContextOptions<DataContext>options):base(options) 
    {

    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(
            "server=HAYAMI\\SQLEXPRESS;database=SoftTest;trusted_connection=true;TrustServerCertificate=True;");
    }
    public virtual DbSet<PreOrderModel> preOrders { get; set; }
    public virtual  DbSet<IdentityUser> IdentityUsers { get; set; }
    //public virtual DbSet<UserModel> UserModels { get; set; }
}
