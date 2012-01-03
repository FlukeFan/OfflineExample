using System.IO;

namespace OfflineExample.Util
{
    public static class AssemblyUtil
    {
        private readonly static long _lastModifiedTicks;

        static AssemblyUtil()
        {
            var assembly = new FileInfo(typeof(AssemblyUtil).Assembly.Location);
            _lastModifiedTicks = assembly.LastWriteTimeUtc.Ticks;
        }

        public static long LastModifiedTicks { get { return _lastModifiedTicks; } }
    }
}