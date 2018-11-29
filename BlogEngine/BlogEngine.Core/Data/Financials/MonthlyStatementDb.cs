using Common.Utilities.Data;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Data.Financials
{
    [Table("BE_CM_Monthly_Statement")]
    public class MonthlyStatementDb : InheritDb
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long StatementId { get; set; }

        public DateTime StatementDateTime { get; set; }
        public long TotalPageViews { get; set; }
        public double TotalProfitMade { get; set; }
        public double TotalWritersPayoutProfit { get; set; }
        public long TotalWritersPaid { get; set; }

    }
}
