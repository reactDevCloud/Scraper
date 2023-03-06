public class UrlFilter {

    public UrlFilter()
    {

    }

    public IEnumerable<IUrl> ContainsHost(IEnumerable<IUrl> source, String value)
    {

        Uri baseUrl = new Uri(value);
        Console.WriteLine(baseUrl);

        foreach(var sourceItem in source)
        {
            Uri subUrl = new Uri(sourceItem.url);

            Console.WriteLine(subUrl.Host, baseUrl.Host);

            if(subUrl.Host == baseUrl.Host)
            {
                yield return sourceItem;
            }
        }
    }
}