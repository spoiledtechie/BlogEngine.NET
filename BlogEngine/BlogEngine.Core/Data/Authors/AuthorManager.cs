using System;
using System.Collections.Generic;
using System.Web.Security;

namespace BlogEngine.Core.Data.Communication
{
    public class AuthorsManager
    {
        //public static bool SendAutomatedPostingEmailToAuthors(List<Guid> userIds)
        //{

        //    for (int i = 0; i < userIds.Count; i++)
        //    {
        //        var mem = User.GetMemberWithUserId(userIds[i]);

        //        var user = Membership.Providers[MultiTenantManager.GetCurrentTenant().ProviderName].GetUser(userIds[i], false);



        //        Dictionary<string, string> emailProps = new Dictionary<string, string>(){
        //        {"Name",mem.GetSiteName()}   ,
        //        {"link",RDN.Library.Classes.Config.LibraryConfig.PublicSite  + "admin/#/"}   
        //       };

        //        EmailServer.EmailServer.SendEmail(RDN.Library.Classes.Config.LibraryConfig.SiteEmail, RDN.Library.Classes.Config.LibraryConfig.FromEmailName, user.UserName, "Rollin News Reminder", emailProps, EmailServerLayoutsEnum.RNAutomatedEmailForWriter, EmailPriority.Important);
        //    }

        //    return true;
        //}
    }
}
