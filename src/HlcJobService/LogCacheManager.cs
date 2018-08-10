using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HlcJobService
{
    public class LogCacheManager
    {
        private readonly Dictionary<string, Queue<string>> _logDict = new Dictionary<string, Queue<string>>();

        private static readonly LogCacheManager _instance = new LogCacheManager();

        private LogCacheManager() { }

        public static LogCacheManager Instance => _instance;

        public void CacheLog(string logId, string log)
        {
            int maxLogSize = 50;

            if (!_logDict.ContainsKey(logId))
            {
                _logDict[logId] = new Queue<string>(maxLogSize);
            }

            if (_logDict[logId].Count >= maxLogSize)
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