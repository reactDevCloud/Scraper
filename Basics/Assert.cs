namespace Crawler;
public static class Assert
{
    public static void AssertNull(object ?obj, String ?message)
    {
        if(obj is null)
        {
            throw new NullReferenceException(message);
        }
    }
}