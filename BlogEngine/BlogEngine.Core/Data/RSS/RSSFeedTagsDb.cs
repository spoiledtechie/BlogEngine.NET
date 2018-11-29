using Common.Site.DataModels.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace BlogEngine.Core.Data.RSS
{
    [Table("BE_RSS_Feed_Tags")]
    public class RSSFeedTagDb: InheritDb
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TagId { get; set; }

        public string TagName { get; set; }

        public virtual RSSFeedDb Feed { get; set; }
    }
}
