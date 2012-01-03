using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Util
{
    public class Entity
    {
        public int Id { get; protected internal set; }

        public override string ToString()
        {
            return string.Format("{0}({1})", GetType().Name, Id);
        }
    }
}
