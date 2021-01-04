using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
namespace ewApps.Core.Money {
  
    /// <summary>
    /// Context factory responsible to provide the Database instance
    /// </summary>
    public class DocumentCurrencyDbContextDesignTimeDbContextFactory:BaseDesignTimeDbContextFactory<DocumentCurrencyDBContext> {

      protected override DocumentCurrencyDBContext CreateNewInstance(DbContextOptions<DocumentCurrencyDBContext> options) {
        return new DocumentCurrencyDBContext(options, _conString);
      }

    }
  }
