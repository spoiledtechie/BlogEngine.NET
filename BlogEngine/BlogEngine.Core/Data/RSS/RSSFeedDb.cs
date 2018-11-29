using BlogEngine.Core.Data.Posts;
using BlogEngine.Core.Data.RSS;
using Common.Utilities.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Data.RSS
{
    /// <summary>
    /// rss feeds of the RN persuasion to import into RN.
    /// </summary>
    [Table("BE_CM_RSS_Feeds")]
    public class RSSFeedDb : InheritDb
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RssId { get; set; }

        public string NameOfSite { get; set; }
        public string UrlOfSite { get; set; }

        public string UrlOfRss { get; set; }
        public DateTime LastCheck { get; set; }
        public long TotalPostsImported { get; set; }
        public bool IsRemoved { get; set; }
        public bool CanNotScanFeed { get; set; }

        public Guid UserIdToAssignPostsTo { get; set; }

        public string InitialImageUrl { get; set; }
        public string MainImageUrl { get; set; }


        public virtual List<PostDb> Posts { get; set; }
        public virtual List<RSSFeedCategoryDb> InitialCategories { get; set; }
        public virtual List<RSSFeedTagDb> InitialTags { get; set; }

        public RSSFeedDb()
        {
            Posts = new List<PostDb>();
            InitialCategories = new List<RSSFeedCategoryDb>();
            InitialTags = new List<RSSFeedTagDb>();
        }



    }
}
