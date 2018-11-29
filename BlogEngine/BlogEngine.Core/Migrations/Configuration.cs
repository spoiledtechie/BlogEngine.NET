namespace BlogEngine.Core.Migrations
{
    using Common.EmailServer.Library.Database.Context;
    using Common.Payment.Datamodels.Context;
    using Common.Site.Database.SiteConfigurations;
    using Common.Site.DataModels.Context;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;


    public class CPayment : DbMigrationsConfiguration<PaymentContext>
    {
        public CPayment()
        {
            AutomaticMigrationsEnabled = false;
        }
    }

    public class CEmail : DbMigrationsConfiguration<EmailServerContext>
    {
        public CEmail()
        {
            AutomaticMigrationsEnabled = false;
        }
    }

    public class CError : DbMigrationsConfiguration<ErrorContext>
    {
        public CError()
        {
            AutomaticMigrationsEnabled = false;
        }
    }

    public class CSite : DbMigrationsConfiguration<SiteContext>
    {
        /// <summary>
        /// 
        /// </summary>
        public CSite()
        {
            AutomaticMigrationsEnabled = false;

        }
        /// <summary>
        /// 
        /// </summary>
        protected override void Seed(SiteContext context)
        {
            try
            {
                var dc = new SiteContext();
                var siteConfigurationDbs = new List<SiteConfigurationDb>();
                if (!dc.SiteConfigurations.Any(x => x.Key == "Company"))
                {
                    // Company name 
                    var company = new SiteConfigurationDb()
                    {
                        Key = "Company",
                        Value = "MyOrange Inc., Walapa",
                        Description = "MyOrange Inc., Walapa",
                        Created = System.DateTime.Now,
                        IsRemoved = false
                    };
                    siteConfigurationDbs.Add(company);
                }
                if (!dc.SiteConfigurations.Any(x => x.Key == "EnableTiles"))
                {
                    //EnableTiles
                    var enableTiles = new SiteConfigurationDb()
                    {
                        Key = "EnableTiles",
                        Value = "1",
                        Description = "EnableTiles",
                        Created = System.DateTime.Now,
                        IsRemoved = false
                    };
                    siteConfigurationDbs.Add(enableTiles);
                }
                if (!dc.SiteConfigurations.Any(x => x.Key == "EnableLoader"))
                {
                    //EnableLoader
                    var enableLoader = new SiteConfigurationDb()
                    {
                        Key = "EnableLoader",
                        Value = "1",
                        Description = "EnableLoader",
                        Created = System.DateTime.Now,
                        IsRemoved = false
                    };
                    siteConfigurationDbs.Add(enableLoader);
                }
                if (!dc.SiteConfigurations.Any(x => x.Key == "Project"))
                {
                    //Project
                    var project = new SiteConfigurationDb()
                    {
                        Key = "Project",
                        Value = "1",
                        Description = "Store App",
                        Created = System.DateTime.Now,
                        IsRemoved = false
                    };
                    siteConfigurationDbs.Add(project);
                }
                if (!dc.SiteConfigurations.Any(x => x.Key == "CurrentTheme"))
                {
                    //CurrentTheme
                    var currentTheme = new SiteConfigurationDb()
                    {
                        Key = "CurrentTheme",
                        Value = "Default",
                        Description = "CurrentTheme",
                        Created = System.DateTime.Now,
                        IsRemoved = false
                    };
                    siteConfigurationDbs.Add(currentTheme);
                }
                if (!dc.SiteConfigurations.Any(x => x.Key == "SerilogFileDestination"))
                {
                    //SerilogFileDestination
                    var serilogFileDestination = new SiteConfigurationDb()
                    {
                        Key = "SerilogFileDestination",
                        Value = "~/app_data/Log-{Date}.txt",
                        Description = "Where serialog file will be saved ",
                        Created = System.DateTime.Now,
                        IsRemoved = false
                    };
                    siteConfigurationDbs.Add(serilogFileDestination);
                }
                if (!dc.SiteConfigurations.Any(x => x.Key == "StripeApiPublicKey"))
                {
                    //StripeApiPublicKey
                    var stripeApiPublicKey = new SiteConfigurationDb()
                    {
                        Key = "StripeApiPublicKey",
                        Value = "pk_test_vC6KZi9Gvgd5TvsdjRDBboy7",
                        Description = "StripeApiPublicKey",
                        Created = System.DateTime.Now,
                        IsRemoved = false
                    };
                    siteConfigurationDbs.Add(stripeApiPublicKey);
                }
                if (!dc.SiteConfigurations.Any(x => x.Key == "StripeApiPrivateKey"))
                {
                    //StripeApiPrivateKey
                    var stripeApiPrivateKey = new SiteConfigurationDb()
                    {
                        Key = "StripeApiPrivateKey",
                        Value = "sk_test_M8V2rmr3UxLea4Xpkg23R8lb",
                        Description = "StripeApiPrivateKey ",
                        Created = System.DateTime.Now,
                        IsRemoved = false
                    };
                    siteConfigurationDbs.Add(stripeApiPrivateKey);
                }
                if (!dc.SiteConfigurations.Any(x => x.Key == "EmailFrom"))
                {
                    //EmailFrom
                    var emailFrom = new SiteConfigurationDb()
                    {
                        Key = "EmailFrom",
                        Value = "EmailFrom@gmail.com",
                        Description = "EmailFrom",
                        Created = System.DateTime.Now,
                        IsRemoved = false
                    };
                    siteConfigurationDbs.Add(emailFrom);
                }


                if (!dc.SiteConfigurations.Any(x => x.Key == "StripeConnectClientId"))
                {
                    //StripeConnectClientId
                    var emailFrom = new SiteConfigurationDb()
                    {
                        Key = "StripeConnectClientId",
                        Value = "ca_E0APLIjbadhTXi5mqUXqgVntwDnTKZrX",
                        Description = "Stripe Connect Client Id",
                        Created = System.DateTime.Now,
                        IsRemoved = false
                    };
                    siteConfigurationDbs.Add(emailFrom);
                }

                if (siteConfigurationDbs != null && siteConfigurationDbs.Count > 0)
                {
                    context.SiteConfigurations.AddRange(siteConfigurationDbs);
                    base.Seed(context);
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {

            }
        }
    }

    public sealed class Configuration : DbMigrationsConfiguration<BlogEngine.Core.Data.Context.BlogEngineManagementContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BlogEngine.Core.Data.Context.BlogEngineManagementContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
