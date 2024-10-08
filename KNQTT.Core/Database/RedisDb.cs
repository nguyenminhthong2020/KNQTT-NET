using StackExchange.Redis;

namespace Infrastructure.Core.Database
{
    public class RedisDb
    {
        /// <summary>
        /// SortedSetRemoveRangeByScore
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public static void SortedSetRemoveRangeByScore(string key, double score)
        {

            IDatabase database = RedisConnection.GetDatabase();
            database.SortedSetRemoveRangeByScore(key, 0, score);
        }

        /// <summary>
        /// SortedSetRemove
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member"></param>
        public static void SortedSetRemove(string key, List<string> member)
        {
            IDatabase database = RedisConnection.GetDatabase();
            database.SortedSetRemove(key, member.Select(x => new RedisValue(x)).ToArray());
        }


        /// <summary>
        /// SortedSetRange
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<string> SortedSetRange(string key)
        {
            var result = new List<string>();
            IDatabase database = RedisConnection.GetDatabase();
            result = database.SortedSetRangeByScore(key).Select(x => x.ToString()).ToList();
            return result;
        }

        /// <summary>
        /// StringSet
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="time"></param>
        public static void StringSet(string key, string value, int time)
        {
            IDatabase database = RedisConnection.GetDatabase();
            var timeSpan = new TimeSpan(0, 0, time);
            database.StringSet(key, value, timeSpan);
        }

        /// <summary>
        /// StringGet
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string StringGet(string key)
        {
            IDatabase database = RedisConnection.GetDatabase();
            return database.StringGet(key);
        }

        /// <summary>
        /// KeyDelete
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool KeyDelete(string key)
        {
            IDatabase database = RedisConnection.GetDatabase();
            return database.KeyDelete(key);
        }

        /// <summary>
        /// KeyDelete
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool KeyDelete(List<string> key)
        {
            IDatabase database = RedisConnection.GetDatabase();
            var deletedKey = database.KeyDelete(key.Select(x => new RedisKey(x)).ToArray());
            return deletedKey == key.Count();
        }

        /// <summary>
        /// ListRightPush
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long ListRightPush(string key, string value)
        {
            IDatabase database = RedisConnection.GetDatabase();
            return database.ListRightPush(key, value);
        }

        /// <summary>
        /// ListRemove
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="time"></param>
        public static long ListRemove(string key, string value)
        {
            IDatabase database = RedisConnection.GetDatabase();
            return database.ListRemove(key, value);
        }

        /// <summary>
        /// SortedSetAdd
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public static bool SortedSetAdd(string key, string value, double score)
        {
            IDatabase database = RedisConnection.GetDatabase();
            return database.SortedSetAdd(key, value, score);
        }

        /// <summary>
        /// SortedSetAdd
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public static long SortedSetAdd(string key, Dictionary<string, double> values)
        {
            IDatabase database = RedisConnection.GetDatabase();
            var temp = values.Select(x => new SortedSetEntry(x.Key, x.Value)).ToArray();
            return database.SortedSetAdd(key, temp);
        }

        /// <summary>
        /// SetAdd
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetAdd(string key, string value)
        {
            IDatabase database = RedisConnection.GetDatabase();
            return database.SetAdd(key, value);
        }

        /// <summary>
        /// SetContains
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetContains(string key, string value)
        {
            IDatabase database = RedisConnection.GetDatabase();
            if (database.KeyExists(key))
            {
                return database.SetContains(key, value);
            }

            return false;
        }

        /// <summary>
        /// SetRemove
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetRemove(string key, string value)
        {
            IDatabase database = RedisConnection.GetDatabase();
            return database.SetRemove(key, value);
        }

        /// <summary>
        /// HashSet
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static bool HashSet(string key, string name, string value)
        {
            IDatabase database = RedisConnection.GetDatabase();
            return database.HashSet(key, name, value);
        }

        /// <summary>
        /// HashDelete
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool HashDelete(string key, string name)
        {
            IDatabase database = RedisConnection.GetDatabase();
            if (database.HashExists(key, name))
            {
                return database.HashDelete(key, name);
            }
            return true;
        }

        /// <summary>
        /// HashGet
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string HashGet(string key, string name)
        {
            IDatabase database = RedisConnection.GetDatabase();
            var result = database.HashGet(key, name);
            if (result.HasValue)
            {
                return result.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// HashExists
        /// </summary>
        /// <param name="key"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool HashExists(string key, string name)
        {
            IDatabase database = RedisConnection.GetDatabase();
            return database.HashExists(key, name);
        }
    }
}
