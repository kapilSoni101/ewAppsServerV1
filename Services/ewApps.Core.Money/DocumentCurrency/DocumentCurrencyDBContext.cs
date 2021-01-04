using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ewApps.Core.Money{
  public class DocumentCurrencyDBContext :DbContext {    
    private string _connString;
    private ILogger<DocumentCurrencyDBContext> _logger;


    public DocumentCurrencyDBContext(DbContextOptions<DocumentCurrencyDBContext> context) : base(context) {
      }

    public DocumentCurrencyDBContext(DbContextOptions<DocumentCurrencyDBContext> options, IOptions<MoneyAppSettings> appSetting) : base(options) {
      _connString = appSetting.Value.ConnectionString;
    }
    /// <summary>
    /// Constructor with Context Options
    /// </summary>
    /// <param name="context"></param>
    /// <param name="loggingService"></param>
    public DocumentCurrencyDBContext(DbContextOptions<DocumentCurrencyDBContext> context, string connString) : base(context) {
      _connString = connString;
    }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);
      }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      optionsBuilder.UseSqlServer(_connString);//Use Sql Server as Backend
      base.OnConfiguring(optionsBuilder);
    }

      public virtual DbSet<DocumentCurrency> DocumentCurrency {
        get;
        set;
      }

     


  }
}
