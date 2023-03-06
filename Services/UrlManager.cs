
/// <summary>
/// Class to perform many operation on Urls
/// <summary>
namespace Crawler.Services;
public class UrlManager 
{  
    private IUrlConverterProvider _urlConverterProvider;
    private UrlSet _urlSet;
    private IUrlValidator _urlValidator;
    private UrlManagerOptions _options;
    public String BaseUrl {get; set;}

    public UrlManager(UrlManagerOptions options, IUrlConverterProvider urlConverterProvider, IUrlValidator urlValidator)
    {
        _urlConverterProvider = urlConverterProvider;
        _urlValidator = urlValidator;
        _options = options;
        _urlSet = options.UrlSet;
    }

    /// <summary>
    /// Add Url object to UrlSet
    /// by validating the url first
    /// </summary>
    public void AddUrl(IUrl url)
    {
        if(ValidateUrl(url))
        {
            _urlSet?.Add(url);
        }
    }

    /// <summary>
    /// Add more than one Url object to UrlSet
    /// by validating them
    /// </summary>
    public void AddUrls(IEnumerable<IUrl> urls)
    {
        foreach(var url in urls)
        {
            AddUrl(url);
        }
    }

    /// <summary>
    /// Convert tobject to url and add them to UrlSet using 
    /// implementation of IObjectToUrlConverter for that object 
    /// </summary>
    public Boolean AddUrlsFrom<tobject>(tobject obj)
    {
        IEnumerable<IUrl> ?urls = GetUrlsFromConverter<tobject>(obj);
        if(urls == null) return false;

        AddUrls(urls);
        return true;
    }

    /// <summary>
    /// get urls using specified converter
    /// </summary>
    private IEnumerable<IUrl> GetUrlsFromConverter<tobject>(tobject obj)
    {
        IUrlConverter<tobject> _urlConverter = GetUrlConverterOf<tobject>() ?? throw new ArgumentException();
        return _urlConverter.Convert(obj);
    }

    /// <summary>
    /// Get urls that are not contained in UrlSet
    /// </summary>
    public IEnumerable<IUrl> GetDifference(IEnumerable<IUrl> urls)
    {
        var diffUrls = GetValidatedUrls(urls);
        diffUrls.ToHashSet<IUrl>().ExceptWith(_urlSet.getUrlSet);
        return diffUrls;
    }

    /// <summary>
    /// Get Urls from tobject that are not contained in UrlSet 
    /// </summary>
    public IEnumerable<IUrl> GetDifferenceFrom<tobject>(tobject obj)
    {
        return GetDifference(GetUrlsFromConverter<tobject>(obj));
    }

    /// <summary>
    /// getting specific IUrlConverter from 
    /// urlConverterProvider
    /// </summary>
    private IUrlConverter<tobject>? GetUrlConverterOf<tobject>()
    {
        IUrlConverter<tobject> urlConverter  =  _urlConverterProvider.GetUrlConverter<tobject>();

        Assert.AssertNull(urlConverter, "IUrlConverter implementation that you specify is not registered");

        return urlConverter;
    }

    /// <summary>
    /// validate the url using IUrlValidator implementation
    /// </summary>
    private Boolean ValidateUrl(IUrl url)
    {
        Assert.AssertNull(url, "Empty url");

        
        // validate url
        if(_urlValidator.Validate(url))
        {
            return true;
        } 
        // validate url by appending base to url. example :- baseUrl + /blogs
        else if(BaseUrl != null && _urlValidator.Validate(GetAbsoluteUrl(url)))
        {
            return true;
        }

        return false;
    }

    /// <scheme>
    /// Get all validated urls from provided urls list
    /// </scheme>
    private IEnumerable<IUrl> GetValidatedUrls(IEnumerable<IUrl> urls)
    {
        foreach(var url in urls)
        {
            if(ValidateUrl(url))
            {
                yield return url;
            }
        }
    }

    private IUrl GetAbsoluteUrl(IUrl url)
    {
        var absoluteUrl = new Url{
            url = BaseUrl + ((Url)url)?.url?.ToString(),
            resType = "200"
        };
        return absoluteUrl;
    }

    public void Update(String url)
    {
        _options.baseUrl = url;
        _urlSet.ClearSet();
    }

    public String GetBaseUrl()
    {
        return _options.baseUrl;
    }

}