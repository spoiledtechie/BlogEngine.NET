<%@ Application Language="C#" %>
<%@ Import Namespace="BlogEngine.NET.App_Start" %>

<script RunAt="server">
    void Application_BeginRequest(object sender, EventArgs e)
    {
        var app = (HttpApplication)sender;
        BlogEngineConfig.Initialize(app.Context);
    }

    void Application_PreRequestHandlerExecute(object sender, EventArgs e)
    {
        BlogEngineConfig.SetCulture(sender, e);
    }
    void Application_Error(object sender, EventArgs e)
    {
        Exception ex = Server.GetLastError();
        Common.Site.Classes.Exception.ErrorDatabaseManager.AddException(ex, ex.GetType());
    }

    void Application_Start()
    {
        try
        {
            System.Data.Entity.Database.SetInitializer(new System.Data.Entity.MigrateDatabaseToLatestVersion<BlogEngine.Core.Data.Context.BlogEngineManagementContext, BlogEngine.Core.Migrations.Configuration>());
            var configuration = new BlogEngine.Core.Migrations.Configuration();
            configuration.AutomaticMigrationsEnabled = true;
            configuration.AutomaticMigrationDataLossAllowed = true;
            var migrator = new System.Data.Entity.Migrations.DbMigrator(configuration);
            migrator.Update();
        }
        catch (Exception exception)
        {
            Common.Site.Classes.Exception.ErrorDatabaseManager.AddException(exception, exception.GetType());
        }

        try
        {
            var configurationEmail = new BlogEngine.Core.Migrations.CEmail();
            configurationEmail.AutomaticMigrationsEnabled = true;
            configurationEmail.AutomaticMigrationDataLossAllowed = true;
            var migratorEmail = new System.Data.Entity.Migrations.DbMigrator(configurationEmail);
            migratorEmail.Update();
        }
        catch (Exception exception)
        {
            Common.Site.Classes.Exception.ErrorDatabaseManager.AddException(exception, exception.GetType());
        }
        try
        {

            var configurationError = new BlogEngine.Core.Migrations.CError();
            configurationError.AutomaticMigrationsEnabled = true;
            configurationError.AutomaticMigrationDataLossAllowed = true;
            var migratorError = new System.Data.Entity.Migrations.DbMigrator(configurationError);
            migratorError.Update();
        }
        catch (Exception exception)
        {
            Common.Site.Classes.Exception.ErrorDatabaseManager.AddException(exception, exception.GetType());
        }
        try
        {

            var configurationSite = new BlogEngine.Core.Migrations.CSite();
            configurationSite.AutomaticMigrationsEnabled = true;
            configurationSite.AutomaticMigrationDataLossAllowed = true;
            var migratorSite = new System.Data.Entity.Migrations.DbMigrator(configurationSite);
            migratorSite.Update();
        }
        catch (Exception exception)
        {
            Common.Site.Classes.Exception.ErrorDatabaseManager.AddException(exception, exception.GetType());
        }
        try
        {
            var configurationPayment = new BlogEngine.Core.Migrations.CPayment();
            configurationPayment.AutomaticMigrationsEnabled = true;
            configurationPayment.AutomaticMigrationDataLossAllowed = true;
            var migratorPayment = new System.Data.Entity.Migrations.DbMigrator(configurationPayment);
            migratorPayment.Update();
        }
        catch (Exception exception)
        {
            Common.Site.Classes.Exception.ErrorDatabaseManager.AddException(exception, exception.GetType());
        }

        //Logger.Info("Web Start");
    }
</script>
