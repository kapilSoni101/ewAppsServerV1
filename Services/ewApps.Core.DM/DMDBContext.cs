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

namespace ewApps.Core.DMService {

    public class DMDBContext:DbContext {
        private string _connString;
        private DMServiceSettings _connOptions;
        private DateTime _intiTime;
        private IHttpContextAccessor _accessor;

        public DMDBContext(DbContextOptions<DMDBContext> context) : base(context) {
            _intiTime = DateTime.UtcNow;
        }

        public DMDBContext(DbContextOptions<DMDBContext> options, IOptions<DMServiceSettings> appSetting, IHttpContextAccessor accessor) : base(options) {
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
        public DMDBContext(DbContextOptions<DMDBContext> context, string connString) : base(context) {
            _connString = connString;
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<DMDocument>()
                             .Property(b => b.Deleted)
                             .HasDefaultValue(false);
            builder.Entity<DMDocument>()
                             .Property(b => b.ParentDeleted)
                             .HasDefaultValue(false);
            builder.Entity<DMFolder>()
                             .Property(b => b.Deleted)
                             .HasDefaultValue(false);
            builder.Entity<DMFolder>()
                             .Property(b => b.ParentDeleted)
                             .HasDefaultValue(false);
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            // string _connectionString = "Database=ewApps-iPayment; Data Source=CONNECT23;User ID=sa;Password=sql2k14@connect";
            optionsBuilder.UseSqlServer(_connString);
            base.OnConfiguring(optionsBuilder);
        }


        /// <summary>
        /// DbSet&lt;DMDocument&gt; can be used to query and save instances of DMDocument entity. 
        /// Linq queries can written using DbSet&lt;DMDocument&gt; that will be translated to sql query and executed against database DMDocument table. 
        /// </summary>
        public virtual DbSet<DMDocument> DMDocument {
            get;
            set;
        }

        /// <summary>
        /// DbSet&lt;DMDocumentFolderLinking&gt; can be used to query and save instances of DMDocumentFolderLinking entity. 
        /// Linq queries can written using DbSet&lt;DMDocumentFolderLinking&gt; that will be translated to sql query and executed against database DMDocumentFolderLinking table. 
        /// </summary>
        public virtual DbSet<DMDocumentFolderLinking> DMDocumentFolderLinking {
            get;
            set;
        }

        /// <summary>
        /// DbSet&lt;DMDocumentUser&gt; can be used to query and save instances of DMDocumentUser entity. 
        /// Linq queries can written using DbSet&lt;DMDocumentUser&gt; that will be translated to sql query and executed against database DMDocumentUser table. 
        /// </summary>
        public virtual DbSet<DMDocumentUser> DMDocumentUser {
            get;
            set;
        }


        /// <summary>
        /// DbSet&lt;DMDocumentVersion&gt; can be used to query and save instances of DMDocumentVersion entity. 
        /// Linq queries can written using DbSet&lt;DMDocumentVersion&gt; that will be translated to sql query and executed against database DMDocumentVersion table. 
        /// </summary>
        public virtual DbSet<DMDocumentVersion> DMDocumentVersion {
            get;
            set;
        }

        /// <summary>
        /// DbSet&lt;DMFileStorage&gt; can be used to query and save instances of DMFileStorage entity. 
        /// Linq queries can written using DbSet&lt;DMFileStorage&gt; that will be translated to sql query and executed against database DMFileStorage table. 
        /// </summary>
        public virtual DbSet<DMFileStorage> DMFileStorage {
            get;
            set;
        }

        /// <summary>
        /// DbSet&lt;DMFolder&gt; can be used to query and save instances of DMFolder entity. 
        /// Linq queries can written using DbSet&lt;DMFolder&gt; that will be translated to sql query and executed against database DMFolder table. 
        /// </summary>
        public virtual DbSet<DMFolder> DMFolder {
            get;
            set;
        }

        /// <summary>
        /// DbSet&lt;DMThumbnail&gt; can be used to query and save instances of DMThumbnail entity. 
        /// Linq queries can written using DbSet&lt;DMThumbnail&gt; that will be translated to sql query and executed against database DMThumbnail table. 
        /// </summary>
        public DbSet<DMThumbnail> DMThumbnail {
            get;
            set;
        }

        /// <summary>
        /// DbSet&lt;DMDocument&gt; can be used to query and save instances of DMDocument entity. 
        /// Linq queries can written using DbSet&lt;DMDocument&gt; that will be translated to sql query and executed against database DMDocument table. 
        /// </summary>
        public virtual DbSet<EntityThumbnail> EntityThumbnail {
            get;
            set;
        }

        /// <summary>
        /// DbSet&lt;DMDocument&gt; can be used to query and save instances of DMDocument entity. 
        /// Linq queries can written using DbSet&lt;DMDocument&gt; that will be translated to sql query and executed against database DMDocument table. 
        /// </summary>
        public virtual DbQuery<DocumentResponseModel> DocumentResponseDTOQuery {
            get;
            set;
        }
        


    }
}
