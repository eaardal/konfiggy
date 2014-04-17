using System.Collections.Specialized;

namespace Konfiggy
{
    public interface IConfigurationKeeper
    {
        NameValueCollection GetSection(string name);
    }
}
