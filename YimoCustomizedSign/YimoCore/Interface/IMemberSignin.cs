using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YimoCore;

namespace YimoCore
{
    public interface IMemberSignin : IPlugin
    {
        SignModel ExecSign();
    }
}
