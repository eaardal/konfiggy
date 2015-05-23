using System.Globalization;

namespace KonfiggyFramework.Settings
{
    public class KonfiggySettings
    {
        public static CultureInfo Culture { get { return CultureInfo.InvariantCulture; } }
        public static char ConfigListValueSeparator { get { return ';'; } }
        public static char ConfigDictionarySeparator { get { return ':'; } }
    }
}
