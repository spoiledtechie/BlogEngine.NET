using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogEngine.Core.RSS
{
  public   class RSSFeedCategory
    {
      public long CategoryId { get; set; }
      public Guid CategoryRNId { get; set; }
      public string Title { get; set; }
    }
}
