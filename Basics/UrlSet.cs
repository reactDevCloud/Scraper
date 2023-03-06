public class UrlSet
{
    public HashSet<IUrl> urlSet;
    public UrlSet()
    {
        this.urlSet = new HashSet<IUrl>();
    }

    public HashSet<IUrl> getUrlSet{

        get {return urlSet;}

    }

    public void Add(IUrl url)
    {
        urlSet.Add(url);
    }

    public void ClearSet()
    {
        urlSet.Clear();
    }

}