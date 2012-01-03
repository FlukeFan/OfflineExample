using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Util
{
    public interface ICommand
    {
        object Execute();
    }
}
