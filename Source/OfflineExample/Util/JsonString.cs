﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace OfflineExample.Util
{
    // hack described here:  http://blog.calyptus.eu/seb/2011/12/custom-datetime-json-serialization/
    public class JsonString : Uri, IDictionary<string, object>
    {
        public JsonString(string value) : base(value, UriKind.Relative) { }

        public void Add(string key, object value) { throw new NotImplementedException(); }
        public bool ContainsKey(string key) { throw new NotImplementedException(); }
        public ICollection<string> Keys { get { throw new NotImplementedException(); } }
        public bool Remove(string key) { throw new NotImplementedException(); }
        public bool TryGetValue(string key, out object value) { throw new NotImplementedException(); }
        public ICollection<object> Values { get { throw new NotImplementedException(); } }
        public object this[string key] { get { throw new NotImplementedException(); } set { throw new NotImplementedException(); } }
        public void Add(KeyValuePair<string, object> item) { throw new NotImplementedException(); }
        public void Clear() { throw new NotImplementedException(); }
        public bool Contains(KeyValuePair<string, object> item) { throw new NotImplementedException(); }
        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex) { throw new NotImplementedException(); }
        public int Count { get { throw new NotImplementedException(); } }
        public bool IsReadOnly { get { throw new NotImplementedException(); } }
        public bool Remove(KeyValuePair<string, object> item) { throw new NotImplementedException(); }
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() { throw new NotImplementedException(); }
        IEnumerator IEnumerable.GetEnumerator() { throw new NotImplementedException(); }
    }
}
