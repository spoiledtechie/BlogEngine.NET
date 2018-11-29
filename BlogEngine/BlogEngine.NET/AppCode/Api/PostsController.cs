using App_Code;
using BlogEngine.Core.Data.Contracts;
using BlogEngine.Core.Data.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

public class PostsController : ApiController
{
    readonly IPostRepository repository;

    public PostsController(IPostRepository repository)
    {
        this.repository = repository;
    }

    public IEnumerable<PostItem> Get(int take = 10, int skip = 0, string filter = "", string order = "")
    {
        return repository.Find(take, skip, filter, order);
    }

    public HttpResponseMessage Get(string id)
    {
        var result = repository.FindById(new Guid(id));
        if (result == null)
            return Request.CreateResponse(HttpStatusCode.NotFound);

        return Request.CreateResponse(HttpStatusCode.OK, result);
    }

    public HttpResponseMessage Post([FromBody]PostDetail item)
    {
        var result = repository.Add(item);

        //if (action == "publishSelf" || action == "publish")
        //{
        //    FacebookData.RecordNewFBShare(result.Id);

        //    Task<bool> t1 = Task.Run(async () =>
        //    {
        //        await Task.Delay(10000);

        //        var token = FacebookData.GetLatestAccessToken();
        //        string baseUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath.TrimEnd('/');
        //        DateTime dateCreated = DateTime.Parse(result.DateCreated);
        //        if (dateCreated > DateTime.UtcNow)
        //        {
        //            FacebookFactory.Initialize(token)
        //                    .GetPageAuthorization(ConfigurationManager.AppSettings["FacebookPageName"].ToString(), ConfigurationManager.AppSettings["FacebookPageId"].ToString())
        //                            .PostToFanPage(result.Title, baseUrl + result.RelativeLink, baseUrl + result.MainImageUrl, result.Title, result.Title, result.DateCreated);

        //            FacebookFactory.Initialize(token).GetPageAuthorization(ConfigurationManager.AppSettings["FacebookPageName2"].ToString(), ConfigurationManager.AppSettings["FacebookPageId2"].ToString())
        //                         .PostToFanPage(result.Title, baseUrl + result.RelativeLink, baseUrl + result.MainImageUrl, result.Title, result.Title, result.DateCreated);
        //        }
        //        else
        //        {
        //            FacebookFactory.Initialize(token)
        //                .GetPageAuthorization(ConfigurationManager.AppSettings["FacebookPageName"].ToString(), ConfigurationManager.AppSettings["FacebookPageId"].ToString())
        //                        .PostToFanPage(result.Title, baseUrl + result.RelativeLink, baseUrl + result.MainImageUrl, result.Title, result.Title, dateCreated.AddHours(1).ToShortDateString());

        //            FacebookFactory.Initialize(token)
        //            .GetPageAuthorization(ConfigurationManager.AppSettings["FacebookPageName2"].ToString(), ConfigurationManager.AppSettings["FacebookPageId2"].ToString())
        //                .PostToFanPage(result.Title, baseUrl + result.RelativeLink, baseUrl + result.MainImageUrl, result.Title, result.Title, dateCreated.AddHours(1).ToShortDateString());
        //        }

        //        if (dateCreated < DateTime.UtcNow)
        //            TwitterManager.Initialize(ConfigurationManager.AppSettings["TwitterConsumerKey"].ToString(), ConfigurationManager.AppSettings["TwitterConsumerSecret"].ToString(), ConfigurationManager.AppSettings["TwitterToken"].ToString(), ConfigurationManager.AppSettings["TwitterTokenSecret"].ToString())
        //                        .SendMessage(result.Title + " #rollerderby " + baseUrl + result.RelativeLink);
        //        return true;
        //    });
        //}




        if (result == null)
            return Request.CreateResponse(HttpStatusCode.NotModified);

        return Request.CreateResponse(HttpStatusCode.Created, result);
    }

    [HttpPut]
    public HttpResponseMessage Update([FromBody]PostDetail item)
    {
        repository.Update(item, "update");


        //if (action == "publishSelf" || action == "publish")
        //{

        //    var token = FacebookData.GetLatestAccessToken();
        //    string baseUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath.TrimEnd('/');
        //    DateTime dateCreated = DateTime.Parse(item.DateCreated);

        //    string initialImage = string.Empty;
        //    if (!String.IsNullOrEmpty(item.MainImageUrl))
        //        initialImage = baseUrl + item.MainImageUrl;

        //    try
        //    {

        //        Task<bool> t1 = Task.Run(async () =>
        //        {
        //            try
        //            {
        //                await Task.Delay(10000);

        //                if (dateCreated > DateTime.UtcNow)
        //                {
        //                    FacebookFactory.Initialize(token)
        //                            .GetPageAuthorization(ConfigurationManager.AppSettings["FacebookPageName"].ToString(), ConfigurationManager.AppSettings["FacebookPageId"].ToString())
        //                                    .PostToFanPage(item.Title, baseUrl + item.RelativeLink, initialImage, item.Title, item.Title, item.DateCreated);

        //                    FacebookFactory.Initialize(token).GetPageAuthorization(ConfigurationManager.AppSettings["FacebookPageName2"].ToString(), ConfigurationManager.AppSettings["FacebookPageId2"].ToString())
        //                    .PostToFanPage(item.Title, baseUrl + item.RelativeLink, initialImage, item.Title, item.Title, item.DateCreated);
        //                }
        //                else
        //                {
        //                    FacebookFactory.Initialize(token)
        //                        .GetPageAuthorization(ConfigurationManager.AppSettings["FacebookPageName"].ToString(), ConfigurationManager.AppSettings["FacebookPageId"].ToString())
        //                                .PostToFanPage(item.Title, baseUrl + item.RelativeLink, initialImage, item.Title, item.Title, string.Empty);

        //                    FacebookFactory.Initialize(token).GetPageAuthorization(ConfigurationManager.AppSettings["FacebookPageName2"].ToString(), ConfigurationManager.AppSettings["FacebookPageId2"].ToString())
        //                        .PostToFanPage(item.Title, baseUrl + item.RelativeLink, initialImage, item.Title, item.Title, string.Empty);

        //                }
        //            }
        //            catch (Exception exception)
        //            {
        //                ErrorDatabaseManager.AddException(exception, GetType(), additionalInformation: initialImage);
        //            }
        //            return true;
        //        });


        //    }
        //    catch (Exception exception)
        //    {
        //        ErrorDatabaseManager.AddException(exception, GetType());
        //    }

        //    if (dateCreated < DateTime.UtcNow)
        //        TwitterManager.Initialize(ConfigurationManager.AppSettings["TwitterConsumerKey"].ToString(), ConfigurationManager.AppSettings["TwitterConsumerSecret"].ToString(), ConfigurationManager.AppSettings["TwitterToken"].ToString(), ConfigurationManager.AppSettings["TwitterTokenSecret"].ToString())
        //                    .SendMessage(item.Title + " #rollerderby " + baseUrl + item.RelativeLink);
        //}





        return Request.CreateResponse(HttpStatusCode.OK);
    }
	
	[HttpPut]
    public HttpResponseMessage ProcessChecked([FromBody]List<PostDetail> items)
    {
        if (items == null || items.Count == 0)
            throw new HttpResponseException(HttpStatusCode.ExpectationFailed);

        var action = Request.GetRouteData().Values["id"].ToString().ToLowerInvariant();
      
        foreach (var item in items)
        {
            if (item.IsChecked)
            {
                if (action == "delete")
                {
                    repository.Remove(item.Id);
                }
                //else if (action.ToLower() == "share")
                //{
                //    PostRepository repo = new PostRepository();
                //    var post = repo.FindById(item.Id);
                //    string baseUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath.TrimEnd('/');

                //    Task<bool> t1 = Task.Run(async () =>
                //    {
                //        try
                //        {
                //            await Task.Delay(10000);

                //            var token = FacebookData.GetLatestAccessToken();
                //            FacebookFactory.Initialize(token)
                //                    .GetPageAuthorization(ConfigurationManager.AppSettings["FacebookPageName"].ToString(), ConfigurationManager.AppSettings["FacebookPageId"].ToString())
                //                            .PostToFanPage(post.Title, baseUrl + post.RelativeLink, baseUrl + post.MainImageUrl, post.Title, post.Title, string.Empty);

                //            FacebookFactory.Initialize(token).GetPageAuthorization(ConfigurationManager.AppSettings["FacebookPageName2"].ToString(), ConfigurationManager.AppSettings["FacebookPageId2"].ToString())
                //                            .PostToFanPage(post.Title, baseUrl + post.RelativeLink, baseUrl + post.MainImageUrl, post.Title, post.Title, string.Empty);

                //            shared = shared.AddMinutes(5);

                //            TwitterManager.Initialize(ConfigurationManager.AppSettings["TwitterConsumerKey"].ToString(), ConfigurationManager.AppSettings["TwitterConsumerSecret"].ToString(), ConfigurationManager.AppSettings["TwitterToken"].ToString(), ConfigurationManager.AppSettings["TwitterTokenSecret"].ToString())
                //                .SendMessage(post.Title + " #rollerderby " + baseUrl + post.RelativeLink);

                //            FacebookData.RecordNewFBShare(post.Id);
                //        }
                //        catch (Exception exception)
                //        {
                //            ErrorDatabaseManager.AddException(exception, GetType());
                //        }
                //        return true;
                //    });
                //}
                else
                {
                    repository.Update(item, action);
                }
            }
        }

        return Request.CreateResponse(HttpStatusCode.OK);
    }

    public HttpResponseMessage Delete(string id)
    {
        Guid gId;
        if (Guid.TryParse(id, out gId))
            repository.Remove(gId);

        return Request.CreateResponse(HttpStatusCode.OK);
    }
}