using Domain.Util;

namespace Domain.Core
{
    public class Root
    {
        protected internal Root() { }

        public static Root Default
        {
            get { return new Root(); }
        }

        public virtual T Execute<T>(Command<T> command)
        {
            return command.InternalExecute();
        }
    }
}