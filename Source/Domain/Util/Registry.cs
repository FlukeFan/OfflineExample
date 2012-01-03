using System;
using Domain.Core;

namespace Domain.Util
{
    public class Registry
    {
        static Registry() { InitialiseDefaults(); }

        public static void InitialiseDefaults()
        {
            Root = Root.Default;
            Repository = new Repository();
            NowUtc = () => DateTime.UtcNow;
        }

        public static Root Root { get; set; }
        public static Repository Repository { internal get; set; }
        public static Func<DateTime> NowUtc;
    }
}