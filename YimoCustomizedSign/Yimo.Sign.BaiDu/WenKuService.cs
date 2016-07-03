using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YimoCore;

namespace Yimo.Sign.BaiDu
{
    public class WenKuService
    {
        static HttpHelper http = new HttpHelper();
        public SignModel BeginSign(string userName, string userPwd)
        {
            SignModel signModel = new SignModel();
            WebSiteModel model = new WebSiteModel()
            {
                SiteDomin = "http://www.baidu.com/",
                SiteName = "百度",
                LoginUrl = "https://passport.baidu.com/v2/api/?login",
                LoginName = "507382865@qq.com",
                LoginPwd = "ycq0729,.",
                SignRequestUrl = "http://wenku.baidu.com/task/submit/signin",
            };
            SetCookieToken(model);
            string uname = PostLogin(model);
            if (uname != null)//登陆成功
            {
                signModel.Msg = "登陆成功";
                string cookie1 = HttpCookieHelper.GetCookieValue("BAIDUID", model.Cookie);
                string cookie2 = HttpCookieHelper.GetCookieValue("BDUSS", model.Result.Cookie);
                model.Cookie = string.Format("BAIDUID={0};BDUSS={1}", cookie1, cookie2);
                model.Result = PostSign(model);
                if (!model.Result.Html.Contains("errno\":\"109") && !model.Result.Html.Contains("errno\":\"0"))
                {
                    signModel.Msg = "签到成功";
                }
                else if (model.Result.Html.Contains("errno\":\"0") && model.Result.Html.Contains("error_no\":\"0"))
                {
                    signModel.Msg = "百度文库已签到";
                }
                else
                {
                    signModel.Msg = "签到失败：" + model.Result.Html;
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
                Cookie = model.Cookie
            });

            return result;
        }
        private string PostLogin(WebSiteModel model)
        {
            string postData = string.Format("staticpage=http%3A%2F%2Fwww.baidu.com%2Fcache%2Fuser%2Fhtml%2Fv3Jump.html&charset=utf-8&token={0}&tpl=mn&apiver=v3&tt=1385516118111&codestring={1}&safeflg=0&u=http%3A%2F%2Fwww.baidu.com%2F&isPhone=false&quick_user=0&logintype=dialogLogin&splogin=newuser&username={2}&password={3}&verifycode={4}&mem_pass=on&ppui_logintime=96181&callback=parent.bd__pcbs__r03do0", model.Token, model.CodeUrl, model.LoginName, model.LoginPwd, model.LoginCode);
            var result = http.GetHtml(new HttpItem()
            {
                URL = model.LoginUrl,
                Method = "POST",
                Postdata = postData,
                Cookie = model.Cookie,
                Accept = "application/json, text/javascript, */*",//    可选项有默认值
                ContentType = "application/x-www-form-urlencoded",//返回类型    可选项有默认值    
            });
            model.Result = result;
            string pattern = "(?<=codeString=)[^&]+?(?=&)";
            model.CodeUrl = RegexHelper.GetString(result.Html, pattern);
            if (!model.CodeUrl.IsNullOrEmpty())
            {
                return null;
            }
            pattern = "(?<=userName=)[^&]*?(?=&)";
            string name = RegexHelper.GetString(result.Html, pattern).UrlDecode();
            if (name.IsNullOrEmpty())
            {
                return null;
            }
            return name;
        }
        /// <summary>
        /// 获取cookie值
        /// </summary>
        /// <param name="model"></param>
        private void SetCookieToken(WebSiteModel model)
        {

            var result = http.GetHtml(new HttpItem()
            {
                URL = model.SiteDomin
            });
            model.Cookie = result.Cookie;
            var result2 = http.GetHtml(new HttpItem()
            {
                URL = "https://passport.baidu.com/v2/api/?getapi&tpl=mn&apiver=v3&tt=1385512949190&class=login&logintype=dialogLogin&callback=bd__cbs__j3jwk9",
                Cookie = result.Cookie

            });
            string pattern = "(?<=\"token\" : \")\\S+?(?=\")";
            string token = RegexHelper.GetString(result2.Html, pattern);
            model.Token = token;
        }
    }
}
