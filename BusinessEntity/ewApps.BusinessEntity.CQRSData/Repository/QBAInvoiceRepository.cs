using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using Microsoft.EntityFrameworkCore;

namespace ewApps.BusinessEntity.QData {

    public class QBAInvoiceRepository:IQBAInvoiceRepository {

        private QBusinessEntityDbContext _context;

        #region Constructor 

        /// <summary>
        /// Initializes a new instance of the <see cref="QBAInvoiceRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public QBAInvoiceRepository(QBusinessEntityDbContext context) {
            _context = context;
        }

        #endregion


        /// <inheritdoc/>
        public async Task<IEnumerable<BusBAARInvoiceAttachmentDTO>> GetInvoiceAttachmentListByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default(CancellationToken)) {
            string sql = @"SELECT ID, ERPConnectorKey, ERPARInvoiceAttachmentKey, ARInvoiceId, ERPARInvoiceKey, Name, [FreeText],
                            AttachmentDate FROM be.BAARInvoiceAttachment as da
                            WHERE da.ARInvoiceId=@InvoiceId";

            SqlParameter invoiceIdParam = new SqlParameter("InvoiceId", invoiceId);
            return await _context.BusBAARInvoiceAttachmentDTOQuery.FromSql(sql, new object[] { invoiceIdParam }).ToListAsync(cancellationToken);
        }

    }
}
