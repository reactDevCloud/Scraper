public interface IUrlConverter<tobject> : IBaseUrlConverter
{
    IEnumerable<IUrl> Convert(tobject obj); 
}