using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Util
{
    public abstract class Command<T> : ParameterObject, ICommand
    {
        public T Execute()
        {
            return Registry.Root.Execute(this);
        }

        internal abstract T InternalExecute();

        object ICommand.Execute()
        {
            return Execute();
        }

        // just syntax sugar
        protected Repository Repository { get { return Registry.Repository; } }
    }
}
