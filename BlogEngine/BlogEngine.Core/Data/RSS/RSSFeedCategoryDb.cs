using Common.Utilities.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace BlogEngine.Core.Data.RSS
{
    [Table("BE_RSS_Feed_Categories")]
    public class RSSFeedCategoryDb : InheritDb
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CategoryId { get; set; }

        public Guid CategoryRNId { get; set; }

        public virtual RSSFeedDb Feed { get; set; }
    }
}
