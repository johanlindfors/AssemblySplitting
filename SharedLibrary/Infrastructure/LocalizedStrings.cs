using SharedLibrary.Resources;

namespace SharedLibrary.Infrastructure
{
    /// <summary>
    /// Provides access to string resources.
    /// </summary>
    public class LocalizedStrings
    {
        private static Strings _localizedResources = new Strings();

        public Strings Strings { get { return _localizedResources; } }
    }
}