using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YimoCore;

namespace Yimo.Sign.BaiDuTieBa
{
    public class MemberSignin : IMemberSignin
    {
        public PluginDescriptor PluginDescriptor { get; set; }


        public int DisplayOrder { get; set; }

        public SignModel ExecSign()
        {


            return new SignModel() { Msg = "签到失败" };
        }
    }
}
