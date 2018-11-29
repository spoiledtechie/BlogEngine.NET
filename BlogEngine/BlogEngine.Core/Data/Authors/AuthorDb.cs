using Common.Utilities.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Data.Authors
{
    [Table("BE_CM_Authors")]
    public class AuthorDb : InheritDb
    {
        /// <summary>
        /// author/userid.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid UserId { get; set; }

        /// <summary>
        /// total times been pushed to facebook automatically.
        /// </summary>
        public bool DoesAuthorGenerateRevenue { get; set; }


    }
}
