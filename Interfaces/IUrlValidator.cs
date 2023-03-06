public interface IUrlValidator
{
    Boolean Validate<tobject>(tobject obj) where tobject:IUrl;
    
}