using HtmlAgilityPack;

namespace Crawler.Services;
public class NodeToUrlConverter : IUrlConverter<HtmlNodeCollection>
{
    public IEnumerable<IUrl> Convert(HtmlNodeCollection node)
    {
        Assert.AssertNull(node, "node is null");
        return GetUrlsFromNode(node);
    }

    private IEnumerable<IUrl> GetUrlsFromNode(HtmlNodeCollection nodes)
    {
        foreach (var node in nodes)
        {
            if(node != null && node.Attributes["href"] != null)
            {
                var url = new Url() {
                    url = node.Attributes["href"].Value,
                    resType = "200"
                };
                yield return url;
            }       
        }
    }
}