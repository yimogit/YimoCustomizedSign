using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YimoCore;

namespace Yimo.Sign.JobBole
{
    public class JobBoleService
    {
        static HttpHelper http = new HttpHelper();
        public SignModel BeginSign(string userName, string userPwd)
        {
            SignModel signModel = new SignModel();
            WebSiteModel model = new WebSiteModel()
            {
                SiteDomin = "http://www.jobbole.com",
                SiteName = "伯乐在线",
                LoginUrl = "http://www.jobbole.com/wp-admin/admin-ajax.php",
                LoginName = userName,
                LoginPwd = userPwd,
                SignRequestUrl = "http://www.jobbole.com/wp-admin/admin-ajax.php",
            };
            model.Cookie = GetCookie(model);
            model.Result = PostLogin(model);
            if (model.Result.Html.Contains("jb_result\":0"))//登陆成功
            {
                model.Cookie = model.Result.Cookie;
                signModel.Msg = "登陆成功";
                model.Result = PostSign(model);
                if (model.Result.Html.Contains("jb_result\"0"))
                {
                    signModel.Msg = "签到成功";
                    signModel.IsSuccess = true;
                }
                else if (model.Result.Html.Contains("jb_result\"-1"))
                {
                    signModel.Msg = RegexHelper.GeMidStringValue(model.Result.Html, "jb_msg\":\"", "\"}").ToGB2312();
                }
            }
            else
            {
                signModel.Msg = "登陆失败";
            }
            signModel.Msg = "[" + userName + "]" + signModel.Msg;

            return signModel;
        }

        /// <summary>
        /// 提交签到请求
        /// </summary>
        /// <param name="model"></param>
        private HttpResult PostSign(WebSiteModel model)
        {
            var result = http.GetHtml(new HttpItem()
            {
                URL = model.SignRequestUrl,
                Cookie = model.Cookie,
                Referer = model.SiteDomin,
                Method = "Post",
                Postdata = "action=get_login_point",
                Timeout = 100000,//连接超时时间     可选项默认为100000
                ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                //UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.94 Safari/537.36",//用户的浏览器类型，版本，操作系统     可选项有默认值
                Accept = "application/json, text/javascript, */*",//    可选项有默认值
                ContentType = "application/x-www-form-urlencoded",//返回类型    可选项有默认值    
            });
            return result;
        }
        private HttpResult PostLogin(WebSiteModel model)
        {
            string postData = string.Format("action=user_login&user_login={0}&user_pass={1}&remember_me=1&redirect_url={2}",
                model.LoginName, model.LoginPwd, model.SignRequestUrl.UrlEncode());
            HttpItem item = new HttpItem()
            {
                URL = model.LoginUrl,
                Postdata = postData,
                Cookie = model.Cookie,
                Referer = model.SiteDomin,
                Method = "POST",
                Accept = "application/json, text/javascript, */*",//    可选项有默认值
                ContentType = "application/x-www-form-urlencoded",//返回类型    可选项有默认值    
            };
            var result = http.GetHtml(item);
            return result;
        }
        /// <summary>
        /// 获取cookie值
        /// </summary>
        /// <param name="model"></param>
        private string GetCookie(WebSiteModel model)
        {
            var result = http.GetHtml(new HttpItem()
            {
                URL = model.SiteDomin
            });

            return result.Cookie;
        }
    }
}
