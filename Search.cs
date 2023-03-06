using Microsoft.AspNetCore.Mvc;
using HtmlAgilityPack;
using Crawler.Services;

namespace Crawler;
[ApiController]
[Route("search")]
public class Search:ControllerBase
{
    UrlManager _urlManager;
    HtmlWeb _web;
    UrlFilter _urlFilter;

    public Search(UrlManager urlManager, HtmlWeb web, UrlFilter urlFilter)
    {
        _urlManager = urlManager;
        _web = web;
        _urlFilter = urlFilter;
    }


    [HttpGet("")]
    public ActionResult<IEnumerable<IUrl>>? getSearch([FromQuery] string url, [FromQuery] Boolean isBase)
    {  

        UpdateBaseUrl(url, isBase);

        IEnumerable<IUrl> ?urls = GetUrlSet(url);

        //urls = _urlFilter.ContainsHost(urls,_urlManager.GetBaseUrl());

        return Ok(urls);
        
    }

    private IEnumerable<IUrl>? GetUrlSet(string url)
    {
        return LoadUrls(url);
    }

    private IEnumerable<IUrl>? LoadUrls(string url)
    {

        try
        {
            HtmlDocument htmlDoc = _web.Load(url);
            HtmlNodeCollection nodes = htmlDoc.DocumentNode.SelectNodes("//a");

            _urlManager.AddUrlsFrom<HtmlNodeCollection>(nodes);

            return _urlManager.GetDifferenceFrom<HtmlNodeCollection>(nodes);
        }

        catch (Exception e)
        {
            Console.WriteLine("Url Just Throw This Message:-", e.Message);
        }

        return null;
    }

    private void UpdateBaseUrl(String url, Boolean isBase)
    {
        if(isBase) _urlManager.Update(url);
    }
    
}