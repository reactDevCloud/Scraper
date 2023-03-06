namespace Crawler.Services;
public class UrlConverterProvider : IUrlConverterProvider
{
    IServiceProvider _serviceProvider;
    public UrlConverterProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public IUrlConverter<tobject> GetUrlConverter<tobject>()
    {
        IUrlConverter<tobject> ?urlConverter = (IUrlConverter<tobject>?)_serviceProvider.GetRequiredService<IBaseUrlConverter>();
        Assert.AssertNull(urlConverter, "there's no converters registered");
        return urlConverter;
    }
}