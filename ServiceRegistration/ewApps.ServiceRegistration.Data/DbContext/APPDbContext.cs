using Microsoft.EntityFrameworkCore;
using  ewApps.ServiceRegistration.Entity;

namespace  ewApps.ServiceRegistration.Data
{

  public class AppDbContext : DbContext
  {

    #region Local Members

    /// <summary>
    /// Local parameter to hold the value of ServiceRegistry string.
    /// </summary>
    private string _connString;

    #endregion Local Members

    #region Constructor
   

    ///// <summary>
    ///// Default constructor to initialize member variables (if any).
    ///// </summary>
    ///// <param name="options">The DbContextOptions instance carries configuration information such as: 
    ///// (a) The database provider to use, UseSqlServer or UseSqlite
    ///// (b) Asha string or identifier of the database instance : its Type "SQLServer" "SQlite" etc..   
    ///// </param>
    ///// <param name="connString">The Core database ServiceRegistry string</param>
    //public AppDbContext(DbContextOptions options, string connString)
    //{
    //  _connString = connString;
    //}

    /// <summary>
    /// Constructor to initialize member variables (if any).
    /// </summary>
    /// <param name="options">The DbContextOptions instance carries configuration information such as: 
    /// (a) The database provider to use, UseSqlServer or UseSqlite
    /// (b) ServiceRegistry string or identifier of the database instance    
    /// </param>
    /// <param name="appSetting">Instance of Appsettings object that contains core database ServiceRegistry string
    /// </param>
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options) {
     // _connString = "Database=SAPDB; Data Source=EWP-DEV19;User ID=sa;Password=sql2k16@ewp";// appSetting.Value.ServiceRegistryString;
    }


    #endregion Constructor

    #region DbContext Override Methods

    /// <summary>
    /// Add any specific mapping of model and the database filed, or assign default values to the properties.
    /// </summary>
    /// <param name="builder"></param>
    protected override void OnModelCreating(ModelBuilder builder)
    {

      base.OnModelCreating(builder);
    }

    /// <summary>
    /// Configure the DBContext instance, Here SQLServer is specified with the ServiceRegistry to the Database specified in the constructor.
    /// </summary>
    /// <param name="optionsBuilder">Configuration options</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      //optionsBuilder.UseSqlServer(_connString);
      base.OnConfiguring(optionsBuilder);
    }

    #endregion DbContext Override Methods


   
    public DbSet<ServiceRegistry> ServiceRegistry
    {
      get; set;
    }

  }

}

