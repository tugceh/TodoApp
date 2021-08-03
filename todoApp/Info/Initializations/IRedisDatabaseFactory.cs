using StackExchange.Redis;

namespace Info.Initializations
{
    public interface IRedisDatabaseFactory
    {
        IDatabase GetDatabase();
    }
}
