public interface IUrlConverterProvider
{
    IUrlConverter<tobject> GetUrlConverter<tobject>();
    
}