using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YimoCore;
using System.Text.RegularExpressions;

namespace 百度请求测试
{
    [TestClass]
    public class 登陆测试
    {
        static HttpHelper http = new HttpHelper();
        [TestMethod]
        public void TestMethod1()
        {
            WebSiteModel model = new WebSiteModel()
            {
                SiteDomin = "http://www.baidu.com/",
                SiteName = "百度",
                LoginUrl = "https://passport.baidu.com/v2/api/?login",
                LoginName = "",
                LoginPwd = "********",
                SignRequestUrl = "http://wenku.baidu.com/task/submit/signin",
            };
            GetToken(model);
            bool r = PostLogin(model);
            if (r)
            {
                //开始签到
                string signHtml = PostSign(model);
            }
            else
            {
                //登陆失败
            }
        }

        private string PostSign(WebSiteModel model)
        {
            //获取cookie
            //model.Cookie BAIDUID
            //model.Result.Cookie  BDUSS
            string cookie1 = HttpCookieHelper.GetCookieValue("BAIDUID", model.Cookie);
            string cookie2 = HttpCookieHelper.GetCookieValue("BDUSS", model.Result.Cookie);
            string cookie = string.Format("BAIDUID={0};BDUSS={1}", cookie1, cookie2);
            var result = http.GetHtml(new HttpItem()
            {
                URL = model.SignRequestUrl,
                Cookie = cookie
            });

            return result.Html;
        }

        private bool PostLogin(WebSiteModel model)
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
                return false;
            }
            pattern = "(?<=userName=)[^&]*?(?=&)";
            string name = RegexHelper.GetString(result.Html, pattern).UrlDecode();
            return true;
        }

        private void GetToken(WebSiteModel model)
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
            string token = Regex.Matches(result2.Html, pattern)[0].Value;
            model.Token = token;

        }
    }
}
