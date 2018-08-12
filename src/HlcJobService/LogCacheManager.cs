using System.Collections.Generic;
using System.Linq;

namespace HlcJobService
{
    public class LogCacheManager
    {
        private const int LOG_SIZE = 100;

        private readonly Dictionary<string, Queue<string>> _logDict = new Dictionary<string, Queue<string>>();

        private static readonly LogCacheManager _instance = new LogCacheManager();

        private LogCacheManager() { }

        public static LogCacheManager Instance => _instance;

        public void CacheLog(string logId, string log)
        {
            if (!_logDict.ContainsKey(logId))
            {
                _logDict[logId] = new Queue<string>(LOG_SIZE);
            }

            if (_logDict[logId].Count >= LOG_SIZE)
            {
                _logDict[logId].Dequeue();
            }

            _logDict[logId].Enqueue(log);
        }

        public List<string> GetLog(string logId)
        {
            if (!_logDict.ContainsKey(logId))
            {
                return new List<string>();
            }

            var logs = _logDict[logId];
            if (logs == null)
            {
                return new List<string>();
            }

            return logs.ToList();
        }
    }
}