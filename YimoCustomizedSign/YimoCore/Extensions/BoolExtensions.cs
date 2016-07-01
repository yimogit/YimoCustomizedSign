using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YimoCore
{
    public static class BoolExtensions
    {
        public static string ToCnValue(this bool value)
        {
            return value ? "是" : "否";
        }
        public static string ToStatusString(this bool r)
        {
            return r ? "成功" : "失败";
        }
    }
}
