using System;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using Domain.Util;

namespace Domain.Test
{
    public class Builder
    {
        /// <summary>
        ///  Utility method to create objects with protected constructor
        ///  e.g., Apple apple = ObjectBuilder.Create&lt;Apple&gt;();
        /// </summary>
        public static T Create<T>()
        {
            ConstructorInfo[] constructors = typeof(T).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);

            if ((constructors == null) || (constructors.Length == 0))
                throw new System.Exception("Couldn't find protected default constructor on type " + typeof(T).Name);

            return (T)constructors[0].Invoke(null);
        }

        /// <summary>
        ///  Utility method to allow tests to access protected member variables
        /// </summary>
        public static object Get<T>(Expression<Func<T>> propertyFunction)
        {
            object instance = GetInstance(propertyFunction.Body);
            PropertyInfo propertyInfo = GetPropertyInfo(propertyFunction.Body);

            if (propertyInfo.GetSetMethod() != null)
                throw new System.Exception("Property '" + propertyInfo.Name + "' is not protected on " + instance.ToString());

            return propertyInfo.GetValue(instance, null);
        }

        /// <summary>
        ///  Utility method to allow tests to set protected member variables
        ///  e.g., ObjectBuilder.Set(() =&gt; myApple.Colour, "Red");
        /// </summary>
        public static void Set<T>(Expression<Func<T>> propertyFunction, T value)
        {
            object instance = GetInstance(propertyFunction.Body);
            PropertyInfo propertyInfo = GetPropertyInfo(propertyFunction.Body);

            if (propertyInfo.GetSetMethod() != null)
                throw new System.Exception("Property '" + propertyInfo.Name + "' is not protected on " + instance.ToString());

            propertyInfo.SetValue(instance, value, null);
        }

        public static Builder<T> Modify<T>(T instance)
        {
            return new Builder<T>(instance);
        }

        protected static PropertyInfo GetPropertyInfo(Expression body)
        {
            MemberExpression me;
            if (body is UnaryExpression)
            {
                body = (body as UnaryExpression).Operand;
            }
            me = (MemberExpression)body;
            return (PropertyInfo)me.Member;
        }

        protected static object GetInstance(Expression body)
        {
            MemberExpression me;
            if (body is UnaryExpression)
            {
                body = (body as UnaryExpression).Operand;
            }
            me = (MemberExpression)body;
            var valueExpression = Expression.Lambda(me.Expression).Compile();
            object value = valueExpression.DynamicInvoke();
            return value;
        }

        protected static object Get(object source, string propertyName)
        {
            PropertyInfo propertyInfo = source.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic);

            if (propertyInfo.GetSetMethod() != null)
                throw new System.Exception("Property '" + propertyInfo.Name + "' is not protected on " + source.ToString());

            return propertyInfo.GetValue(source, null);
        }
    }

    public class Builder<T> : Builder
    {
        protected T _target;

        public Builder() : this(Create<T>()) { }

        public Builder(T instance)
        {
            _target = instance;
        }

        public Builder<T> With<U>(Expression<Func<T, U>> propertyFunction, U value)
        {
            PropertyInfo propertyInfo = GetPropertyInfo(propertyFunction.Body);
            if (propertyInfo.GetSetMethod() != null)
                throw new System.Exception("Property '" + propertyInfo.Name + "' is not protected on " + _target.ToString());

            propertyInfo.SetValue(_target, value, null);
            return this;
        }

        public T Build()
        {
            return _target;
        }

        public T BuildAndPersist()
        {
            DomainTestBase.Repository.Save((Entity)(object)_target);
            return _target;
        }
    }
}