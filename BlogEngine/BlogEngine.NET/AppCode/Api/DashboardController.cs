using BlogEngine.Core;
using BlogEngine.Core.Data.Contracts;
using BlogEngine.Core.Data.ViewModels;
using System.Web.Http;

public class DashboardController : ApiController
{
    readonly IDashboardRepository repository;

    public DashboardController(IDashboardRepository repository)
    {
        this.repository = repository;
    }

    public DashboardVM Get()
    {
        if (!Security.IsAuthorizedTo(Rights.ViewDashboard))
            throw new System.UnauthorizedAccessException();

        if (Security.IsWriter && !Security.IsAdministrator)
            return repository.Get(true);
        else
            return repository.Get(false);


    }
}