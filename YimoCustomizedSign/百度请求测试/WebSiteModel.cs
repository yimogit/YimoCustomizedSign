using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YimoCore;

namespace 百度请求测试
{
    public  class WebSiteModel
    {
        public string SiteName { get; set; }
        public string SiteDomin { get; set; }
        public string LoginUrl { get; set; }
        public string LoginName { get; set; }
        public string LoginPwd { get; set; }
        public string LoginCode { get; set; }
        public string SignRequestUrl { get; set; }
        public string CodeUrl { get; set; }
        public string Token { get; set; }
        public string Cookie { get; set; }
        public HttpResult Result { get; set; }
    }
}
