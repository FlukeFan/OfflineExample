using System.Collections.Generic;
using Domain.Core;
using Domain.Util;

namespace OfflineExample.Test
{
    public class RootStub : Root
    {
        private IDictionary<object, object> _stubResults = new Dictionary<object, object>();

        public override T Execute<T>(Command<T> command)
        {
            if (_stubResults.ContainsKey(command))
                return (T)_stubResults[command];
            else
                return default(T);
        }

        public void StubExecute(object command, object result)
        {
            _stubResults.Add(command, result);
        }
    }
}
