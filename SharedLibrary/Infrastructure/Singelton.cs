
namespace SharedLibrary.Infrastructure
{
    public abstract class Singleton<T>
    {
        protected static T sharedInstance;
        static readonly object padlock = new object();
        
        public static T SharedInstance
        {
            get
            {
                lock (padlock)
                {
                    if (sharedInstance == null)
                        sharedInstance = default(T);
                    return sharedInstance;
                }
            }
        }
    }
}
