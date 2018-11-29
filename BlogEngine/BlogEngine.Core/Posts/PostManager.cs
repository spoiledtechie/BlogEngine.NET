using BlogEngine.Core.Data.Context;
using BlogEngine.Core.Data.Posts;
using Common.Site.Classes.Cache;
using Common.Site.Classes.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Core.Posts
{
    public class PostManager
    {
        public static bool AddPostForFeed(Guid id, long feedId)
        {
            try
            {
                var dc = new BlogEngineManagementContext();
                var p = dc.PostsDb.Where(x => x.PostId == id).FirstOrDefault();
                if (p != null)
                {
                    p.Feed = dc.RssFeeds.Where(x => x.RssId == feedId).FirstOrDefault();
                }
                else
                {
                    p = new PostDb();
                    p.PostId = id;
                    p.Feed = dc.RssFeeds.Where(x => x.RssId == feedId).FirstOrDefault();
                    dc.PostsDb.Add(p);
                }

                int c = dc.SaveChanges();
                return c > 0;
            }
            catch (Exception exception)
            {
                ErrorDatabaseManager.AddException(exception, exception.GetType());
            }

            return false;
        }


        public static Post GetPostsViews(Guid id)
        {
            var postss = Common.Site.Classes.Cache.CacheManager.GetCache<Post>("GetPostsViews" + id);

            if (postss == null)
            {

                var dc = new BlogEngineManagementContext();
                var posts = (from xx in dc.PostsDb.Include("Feed")
                             where xx.PostId == id
                             select new Post
                             {
                                 TotalMonthlyViews = xx.CurrentMonthlyViews,
                                 Id = xx.PostId,
                                 TotalViews = xx.TotalViews,
                                 DisabledAutoPosting = xx.DisabledAutoPosting,
                                 DisablePaymentsForPost = xx.DisablePaymentsForPost,
                                 FromFeed = xx.Feed == null ? false : true,
                                 FeedName = xx.Feed == null ? string.Empty : xx.Feed.NameOfSite,
                                 FeedUrl = xx.Feed == null ? string.Empty : xx.Feed.UrlOfSite
                             }).FirstOrDefault();

                postss = posts;
                if (posts != null)
                    CacheManager.AddCache<Post>(posts, "GetAllPostsViews" + id);
            }
            return postss;
        }

        public static List<Post> GetAllPostsViews()
        {
            var postss = Common.Site.Classes.Cache.CacheManager.GetCache<List<Post>>("GetAllPostsViews");

            if (postss == null || postss.Count == 0)
            {
                postss = new List<Post>();

                var dc = new BlogEngineManagementContext();
                var posts = (from xx in dc.PostsDb
                             select new
                             {
                                 xx.CurrentMonthlyViews,
                                 xx.PostId,
                                 xx.TotalViews,
                                 xx.DisabledAutoPosting,
                                 xx.DisablePaymentsForPost,
                                 xx.Feed
                             }).AsParallel().ToList();
                for (int i = 0; i < posts.Count; i++)
                {
                    Post p = new Post();
                    p.TotalMonthlyViews = posts[i].CurrentMonthlyViews;
                    p.Id = posts[i].PostId;
                    p.TotalViews = posts[i].TotalViews;
                    p.DisabledAutoPosting = posts[i].DisabledAutoPosting;
                    p.DisablePaymentsForPost = posts[i].DisablePaymentsForPost;
                    if (posts[i].Feed != null)
                    {
                        p.FromFeed = true;
                        p.FeedName = posts[i].Feed.NameOfSite;
                        p.FeedUrl = posts[i].Feed.UrlOfSite;
                    }
                    postss.Add(p);
                }

                CacheManager.AddCache<List<Post>>(postss, "GetAllPostsViews");
            }
            return postss;
        }
        public static bool AddViewToPost(Guid id)
        {
            try
            {
                var dc = new BlogEngineManagementContext();
                var p = dc.PostsDb.Where(x => x.PostId == id).FirstOrDefault();
                if (p != null)
                {
                    p.TotalViews += 1;
                    p.CurrentMonthlyViews += 1;
                }
                else
                {
                    p = new PostDb();
                    p.PostId = id;
                    p.CurrentMonthlyViews = 1;
                    p.TotalViews = 1;
                    dc.PostsDb.Add(p);
                }
                int c = dc.SaveChanges();
                return c > 0;
            }
            catch (Exception exception)
            {
                ErrorDatabaseManager.AddException(exception, exception.GetType());
            }

            return false;
        }
        public static bool SavePostToDb(Guid id, bool isAutoSharingDisabled, bool disablePaymentsForPost)
        {
            try
            {
                var dc = new BlogEngineManagementContext();
                var p = dc.PostsDb.Where(x => x.PostId == id).FirstOrDefault();
                if (p != null)
                {
                    p.DisabledAutoPosting = isAutoSharingDisabled;
                    p.DisablePaymentsForPost = disablePaymentsForPost;
                    p.LastTimePostedToFacebook = DateTime.UtcNow;
                    p.TotalFacebookPosts += 1;
                }
                else
                {
                    p = new PostDb();
                    p.PostId = id;

                    p.DisabledAutoPosting = isAutoSharingDisabled;
                    p.DisablePaymentsForPost = disablePaymentsForPost;
                    p.LastTimePostedToFacebook = DateTime.UtcNow;
                    p.TotalFacebookPosts = 1;
                    dc.PostsDb.Add(p);
                }
                int c = dc.SaveChanges();
                return c > 0;
            }
            catch (Exception exception)
            {
                ErrorDatabaseManager.AddException(exception, exception.GetType());
            }

            return false;
        }
        public static bool DeletePost(Guid id)
        {
            try
            {
                var dc = new BlogEngineManagementContext();
                var p = dc.PostsDb.Where(x => x.PostId == id).FirstOrDefault();
                dc.PostsDb.Remove(p);
                int c = dc.SaveChanges();
                return c > 0;
            }
            catch (Exception exception)
            {
                ErrorDatabaseManager.AddException(exception, exception.GetType());
            }

            return false;
        }

        public static bool DeletePosts(List<Guid> id)
        {
            try
            {
                var dc = new BlogEngineManagementContext();
                var posts = dc.PostsDb.Where(x => !id.Contains(x.PostId)).Select(x => x.PostId).ToList();

                for (int i = 0; i < posts.Count; i++)
                {
                    dc.PostsDb.Remove(dc.PostsDb.Where(x => x.PostId == posts[i]).FirstOrDefault());
                }
                int c = dc.SaveChanges();
                return c > 0;
            }
            catch (Exception exception)
            {
                ErrorDatabaseManager.AddException(exception, exception.GetType());
            }

            return false;
        }
    }
}
