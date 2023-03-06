namespace Crawler;
public class UrlManagerOptions
{
    public UrlManagerOptions(IServiceProvider service)
    {
        _service = service;
    }
    private IServiceProvider _service;

    public UrlSet UrlSet{
        get 
        {
            var urlSet = _service.GetService<UrlSet>();

            Assert.AssertNull(urlSet, "UrlSet is not registered");

            return urlSet;
        }
    }

    public String baseUrl {get; set;}
}