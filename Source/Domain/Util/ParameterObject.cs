using System;
using System.Collections;
using System.Reflection;

namespace Domain.Util
{
    /// <summary>
    /// For classes that want to pass list of parameters, and have value-like comparison
    /// </summary>
    public abstract class ParameterObject
    {
        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != GetType())
                return false;

            foreach (FieldInfo field in GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                object source = field.GetValue(this);
                object target = field.GetValue(obj);

                if (source == null)
                    if (target != null)
                        return false;
                    else
                        continue;

                if (source is IEnumerable && target is IEnumerable)
                {
                    IEnumerator enum1 = ((IEnumerable)source).GetEnumerator();
                    IEnumerator enum2 = ((IEnumerable)target).GetEnumerator();

                    while (true)
                    {
                        if (enum1.MoveNext())
                        {
                            if (!enum2.MoveNext())
                                return false;
                        }
                        else
                        {
                            if (enum2.MoveNext())
                                return false;
                            break;
                        }

                        if (enum1.Current == null && enum2.Current != null)
                            return false;

                        if (enum1.Current != null)
                        {
                            if (!enum1.Current.Equals(enum2.Current))
                                return false;
                        }
                    }
                }
                else
                {
                    if (!source.Equals(target))
                        return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            string value = GetType().Name + "(";

            foreach (FieldInfo field in GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                value += field.Name + "=" + FormatValue(field.GetValue(this)) + " ";

            return value.TrimEnd() + ")";
        }

        private string FormatValue(object value)
        {
            if (value == null)
                return "<null>";

            if (!(value is IEnumerable) || value is string)
                return value.ToString();

            string formatted = "{";

            foreach (object enumerable in (value as IEnumerable))
                formatted += FormatValue(enumerable) + " ";

            return formatted.TrimEnd() + "}";
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public static bool operator ==(ParameterObject value1, ParameterObject value2)
        {
            if ((object)value1 == null)
                return ((object)value2 == null);

            return value1.Equals(value2);
        }

        public static bool operator !=(ParameterObject value1, ParameterObject value2)
        {
            return !(value1 == value2);
        }
    }
}
