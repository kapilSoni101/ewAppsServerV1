﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using ewApps.Core.Common;
using ewApps.Payment.Common;
using ewApps.Payment.Data;
using ewApps.Payment.Entity;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Payment.DTO;
using ewApps.Core.BaseService;

namespace ewApps.Payment.DS {

  public interface IPaymentDataService : IBaseDS<ewApps.Payment.Entity.Payment> {        

    }
}

