using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ewApps.Payment.Entity;

namespace ewApps.Payment.DS {
  /// <summary>
  /// Mapper class to map entity to model object.
  /// </summary>
  public class PaymentAutoMapperProfileConf :Profile {

    /// <summary>
    /// Mapping constructor
    /// </summary>
    public PaymentAutoMapperProfileConf() {      
    }
  }
}
