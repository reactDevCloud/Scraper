using System.Runtime;
namespace Crawler.Services;
public class UrlValidator : IUrlValidator
{
    public bool Validate<tobject>(tobject obj) where tobject : IUrl
    {
        if(Uri.TryCreate(obj.url, new UriCreationOptions{DangerousDisablePathAndQueryCanonicalization = true}, out Uri uri)){
            return true;
        }
        return false;
    }
}