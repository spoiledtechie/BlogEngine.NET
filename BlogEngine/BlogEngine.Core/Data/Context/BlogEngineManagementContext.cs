using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Runtime.Caching;
using Common.Utilities.Data;
using BlogEngine.Core.Data.Financials;
using BlogEngine.Core.Data.RSS;
using BlogEngine.Core.Data.Posts;
using BlogEngine.Core.Data.Communication;
using BlogEngine.Core.Data.Authors;

namespace BlogEngine.Core.Data.Context
{
    public class BlogEngineManagementContext : DbContext
    {
        // Database.SetInitializer<BlogEngineManagementContext>(new CreateDatabaseIfModelChanges<BlogEngineManagementContext>());
        // Database.SetInitializer<BlogEngineManagementContext>(new CreateDatabaseIfNotExists<BlogEngineManagementContext>());                                                            
        // Database.SetInitializer<DataContext>(new DropCreateDatabaseAlways<DataContext>());
        public DbSet<PostDb> PostsDb { get; set; }

        public DbSet<FundsForWriterDb> Funds { get; set; }
        public DbSet<MonthlyStatementDb> MonthlyStatements { get; set; }
        public DbSet<RSSFeedDb> RssFeeds { get; set; }
        public DbSet<AuthorDb> Authors { get; set; }

        public DbSet<ContactDb> Contacts { get; set; }

        private static MemoryCache memCache = MemoryCache.Default;


        public BlogEngineManagementContext()
            : base(System.Web.HttpContext.Current == null ?
                    (memCache.Get("RNManagementDatabaseConnectionName") == null || (String.IsNullOrEmpty(memCache.Get("RNManagementDatabaseConnectionName").ToString()) == true) ?
                  "RN" : memCache.Get("RNManagementDatabaseConnectionName").ToString()) : (System.Web.HttpContext.Current.Items["RNManagementDatabaseConnectionName"] == null ?
                  "DefaultConnection" : System.Web.HttpContext.Current.Items["RNManagementDatabaseConnectionName"].ToString()))
        {
        }

        public static void SetDataContext(string databaseConnectionName)
        {
            if (System.Web.HttpContext.Current != null)
                System.Web.HttpContext.Current.Items["RNManagementDatabaseConnectionName"] = databaseConnectionName;
            else
                memCache.Set("RNManagementDatabaseConnectionName", databaseConnectionName, new CacheItemPolicy() { Priority = CacheItemPriority.NotRemovable });

        }


        public BlogEngineManagementContext(string connectionStringName)
            : base(connectionStringName)
        {
        }



        // Automatically add the times the entity got created/modified
        public override int SaveChanges()
        {
            string tempInfo = String.Empty;
            try
            {
                try
                {
                    var entries = ChangeTracker.Entries().ToList();
                    for (int i = 0; i < entries.Count; i++)
                    {
                        if (entries[i].State == EntityState.Unchanged || entries[i].State == EntityState.Detached || entries[i].State == EntityState.Deleted) continue;

                        var hasInterfaceInheritDb = entries[i].Entity as InheritDb;
                        if (hasInterfaceInheritDb == null) continue;

                        if (entries[i].State == EntityState.Added)
                        {
                            var created = entries[i].Property("Created");
                            if (created != null)
                            {
                                created.CurrentValue = DateTime.UtcNow;
                            }
                        }
                        if (entries[i].State == EntityState.Modified)
                        {
                            var modified = entries[i].Property("LastModified");
                            if (modified != null)
                            {
                                modified.CurrentValue = DateTime.UtcNow;
                            }
                        }
                    }
                }
                catch (System.Exception dbEx)
                {
                    string additionalInfo = string.Empty;


                    //ErrorDatabaseManager.AddException(dbEx, dbEx.GetType(), additionalInformation: additionalInfo);
                }
                return base.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                string additionalInfo = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        additionalInfo += "Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage + "";
                    }
                }
                //ErrorDatabaseManager.AddException(dbEx, dbEx.GetType(), additionalInformation: additionalInfo);
                return 0;
            }

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {




            //modelBuilder.Entity<Group>().HasMany(x => x.Members).WithMany(c => c.Groups)
            //    .Map(y =>
            //             {
            //                 y.MapLeftKey("GroupId");
            //                 y.MapRightKey("MemberId");
            //                 y.ToTable("RDN_Team_Group_to_Member");
            //             });            
        }
    }
}
