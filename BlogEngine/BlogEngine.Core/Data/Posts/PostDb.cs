using BlogEngine.Core.Data.RSS;
using Common.Site.DataModels.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Data.Posts
{
    [Table("BE_CM_Posts")]
    public class PostDb : InheritDb
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid PostId { get; set; }

        /// <summary>
        /// total times been pushed to facebook automatically.
        /// </summary>
        public long TotalFacebookPosts { get; set; }
        public DateTime? LastTimePostedToFacebook { get; set; }

        public long CurrentMonthlyViews { get; set; }
        public long TotalViews { get; set; }
        public bool DisabledAutoPosting { get; set; }

        public bool DisablePaymentsForPost { get; set; }

        public virtual RSSFeedDb Feed { get; set; }
    }
}
