//using ewApps.Core.CommonService;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ewApps.Core.UserSessionService {

  public class UserSessionDBContext :DbContext {
    private string _connString;
    private UserSessionAppSettings _connOptions;
    private DateTime _intiTime;   
    private IHttpContextAccessor _accessor;

    public UserSessionDBContext(DbContextOptions<UserSessionDBContext> context) : base(context) {
      _intiTime = DateTime.UtcNow;
     }

    public UserSessionDBContext(DbContextOptions<UserSessionDBContext> options, IOptions<UserSessionAppSettings> appSetting, IHttpContextAccessor accessor) : base(options) {
      _connOptions = appSetting.Value;
      _connString = _connOptions.ConnectionString;    
      _intiTime = DateTime.UtcNow;
      _accessor = accessor;
    }
        /// <summary>
        /// Constructor with Context Options
        /// </summary>
        /// <param name="context"></param>
        /// <param name="loggingService"></param>
        public UserSessionDBContext(DbContextOptions<UserSessionDBContext> context, string connString) : base(context) {
            _connString = connString;
        }

        protected override void OnModelCreating(ModelBuilder builder) {
      base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
      // string _connectionString = "Database=ewApps-iPayment; Data Source=CONNECT23;User ID=sa;Password=sql2k14@connect";
      optionsBuilder.UseSqlServer(_connString);
      base.OnConfiguring(optionsBuilder);
    }

    public virtual DbSet<UserSession> UserSessions {
      get;
      set;
    }

    //public async virtual Task<UserSession> Get(Guid id) {

    //  return await this.UserSessions.FindAsync(id);
    //  //.Set<UserSession>().Find(id);
    //}

    public UserSession GetSession(Guid token) {
      // _loggingService.LogInfo("UserSessionDBContext.GetSession()-Start-" + _intiTime.ToString() + " Url-" + _accessor.HttpContext.Request.Path.Value.ToString());
      //   return this.Set<UserSession>().FirstOrDefault<UserSession>(i => i.UserSessionToken == token);
      // _loggingService.LogInfo("UserSessionDBContext.GetSession()-End-" + _intiTime.ToString() + " Url-" + _accessor.HttpContext.Request.Path.Value.ToString());
      UserSession userSession = UserSessions.Where(i => i.ID == token).AsNoTracking().FirstOrDefault();
      return userSession;
    }

    public bool HasSession(Guid token) {
      //try {
      //_context.get
      return this.UserSessions.Any(i => i.ID.Equals(token));
      //return this.Set<UserSession>().FirstOrDefault<UserSession>(i => i.UserSessionToken == token);
      //
      //catch (Exception ex) {

      //  throw;
      //}
    }

        public List<UserSession> GetUserSessionByIdentityServerId(string identityServerId) {
            List<UserSession> userSessionsList = UserSessions.Where(us => us.IdentityServerId == identityServerId).AsNoTracking().ToList();
            return userSessionsList;
        }


  }
}
