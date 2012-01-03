using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using Domain.Util;

namespace Domain.Test.Util
{
    [TestFixture]
    public class TestParameterObject : DomainTestBase
    {
        public class StringValue : ParameterObject
        {
            public StringValue() {}
            public StringValue(string value) { Value = value; }

            public string Value = "abc";
        }

        public class IntValue : ParameterObject
        {
            public int Value = 123;
        }

        public class ObjectValue : ParameterObject
        {
            public TestEntity Entity = Builder.Create<TestEntity>();
        }

        public class ListValue : ParameterObject
        {
            public ListValue() { Values.Add(1); Values.Add(2); Values.Add(3); }

            public IList<int> Values = new List<int>();
        }

        public class ListStringValue : ParameterObject
        {
            public ListStringValue() { Values.Add("string1");  Values.Add(null); }
            public IList<string> Values = new List<string>();
        }

        public class DictionaryValue : ParameterObject
        {
            public DictionaryValue() { Values.Add("a", "1"); Values.Add("null", null); }

            public IDictionary<string, string> Values = new Dictionary<string, string>();
        }

        public class TestEntity : Entity
        {
            protected TestEntity() { }
        }

        public class Combined : ParameterObject
        {
            public object obj1;
            public object obj2;
        }

        [Test]
        public void StringConversion()
        {
            Assert.That(new StringValue().ToString(), Is.EqualTo("StringValue(Value=abc)"));
            Assert.That(new IntValue().ToString(), Is.EqualTo("IntValue(Value=123)"));
            Assert.That(new ObjectValue().ToString(), Is.EqualTo("ObjectValue(Entity=TestEntity(0))"));
            Assert.That(new ListValue().ToString(), Is.EqualTo("ListValue(Values={1 2 3})"));
            Assert.That(new ListStringValue().ToString(), Is.EqualTo("ListStringValue(Values={string1 <null>})"));
            Assert.That(new DictionaryValue().ToString(), Is.EqualTo("DictionaryValue(Values={[a, 1] [null, ]})"));
            Assert.That(new StringValue(null).ToString(), Is.EqualTo("StringValue(Value=<null>)"));
        }

        [Test]
        public void Strings()
        {
            StringValue value1 = new StringValue();
            StringValue value2 = new StringValue();
            StringValue value3 = new StringValue();

            value1.Value = "abc";
            value2.Value = "abc";
            value3.Value = "bcd";

            Assert.That(value1 == value2);
            Assert.That(value1 != value3);
        }

        [Test]
        public void Ints()
        {
            IntValue value1 = new IntValue();
            IntValue value2 = new IntValue();
            IntValue value3 = new IntValue();

            value1.Value = 1;
            value2.Value = 1;
            value3.Value = 2;

            Assert.That(value1 == value2);
            Assert.That(value1 != value3);
        }

        [Test]
        public void Objects()
        {
            ObjectValue value1 = new ObjectValue();
            ObjectValue value2 = new ObjectValue();
            ObjectValue value3 = new ObjectValue();
            ObjectValue value4 = new ObjectValue();
            ObjectValue value5 = new ObjectValue();

            value1.Entity = Builder.Create<TestEntity>();
            value2.Entity = value1.Entity;
            value3.Entity = Builder.Create<TestEntity>();
            value4.Entity = null;
            value5.Entity = null;

            Assert.That(value1 == value2);
            Assert.That(value1 != value3);
            Assert.That(value4 == value5);
            Assert.That(value1 != value4);
            Assert.That(value4 != value1);
            Assert.That(value1 != null);
            Assert.That(null != value1);
        }

        [Test]
        public void Lists()
        {
            IList<int> values1 = new List<int>();
            IList<int> same = new List<int>();
            IList<int> longer = new List<int>();
            IList<int> shorter = new List<int>();
            IList<int> different = new List<int>();

            values1.Add(1); values1.Add(2); values1.Add(3);
            same.Add(1); same.Add(2); same.Add(3);
            longer.Add(1); longer.Add(2); longer.Add(3); longer.Add(4);
            shorter.Add(1); shorter.Add(2);
            different.Add(1); different.Add(3); different.Add(2);
            
            ListValue value1 = new ListValue();
            ListValue value2 = new ListValue();
            ListValue value3 = new ListValue();
            ListValue value4 = new ListValue();
            ListValue value5 = new ListValue();
            ListValue value6 = new ListValue();

            value1.Values = values1;
            value2.Values = same;
            value3.Values = longer;
            value4.Values = shorter;
            value5.Values = different;
            value6.Values = null;

            Assert.That(value1 == value2);
            Assert.That(value1 != value3);
            Assert.That(value1 != value4);
            Assert.That(value1 != value5);
            Assert.That(value1 != value6);
        }

        [Test]
        public void Dictionaries()
        {
            IDictionary<string, string> values1 = new Dictionary<string, string>();
            IDictionary<string, string> same = new Dictionary<string, string>();
            IDictionary<string, string> longer = new Dictionary<string, string>();
            IDictionary<string, string> shorter = new Dictionary<string, string>();
            IDictionary<string, string> different1 = new Dictionary<string, string>();
            IDictionary<string, string> different2 = new Dictionary<string, string>();
            IDictionary<string, string> different3 = new Dictionary<string, string>();

            values1.Add("a", "1");  values1.Add("b", ""); values1.Add("c", null);
            same.Add("a", "1");  same.Add("b", ""); same.Add("c", null);
            longer.Add("a", "1");  longer.Add("b", ""); longer.Add("c", null); longer.Add("d", "4");
            shorter.Add("a", "1");  shorter.Add("b", "");
            different1.Add("a", "1");  different1.Add("b", "2"); different1.Add("c", null);
            different2.Add("a", "1");  different2.Add("b", ""); different2.Add("c", "3");
            different3.Add("a", "1");  different3.Add("b", null); different3.Add("c", null);
            
            DictionaryValue value1 = new DictionaryValue();
            DictionaryValue value2 = new DictionaryValue();
            DictionaryValue value3 = new DictionaryValue();
            DictionaryValue value4 = new DictionaryValue();
            DictionaryValue value5 = new DictionaryValue();
            DictionaryValue value6 = new DictionaryValue();
            DictionaryValue value7 = new DictionaryValue();
            DictionaryValue value8 = new DictionaryValue();

            value1.Values = values1;
            value2.Values = same;
            value3.Values = longer;
            value4.Values = shorter;
            value5.Values = different1;
            value6.Values = different2;
            value7.Values = different3;
            value8.Values = null;

            Assert.That(value1 == value2);
            Assert.That(value1 != value3);
            Assert.That(value1 != value4);
            Assert.That(value1 != value5);
            Assert.That(value1 != value6);
            Assert.That(value1 != value7);
            Assert.That(value1 != value8);
        }

        [Test]
        public void References()
        {
            Combined value1 = new Combined();
            Combined value2 = new Combined();
            Combined value3 = new Combined();

            value1.obj2 = new object();
            value2.obj2 = value1.obj2;
            value3.obj2 = new object();

            Assert.That(value1 == value2);
            Assert.That(value1 != value3);
        }

        [Test]
        public void TestNull()
        {
            ParameterObject obj1 = null;
            ParameterObject obj2 = null;

            Assert.That(obj1 == obj2);
            Assert.That(obj1 == null);
            Assert.That(null == obj1);
        }
    }
}
