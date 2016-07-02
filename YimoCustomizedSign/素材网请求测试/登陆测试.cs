using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YimoCore;
using System.Text;

namespace 素材网请求测试
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
                SiteDomin = "http://www.17sucai.com/",
                SiteName = "17素材网",
                LoginUrl = "http://www.17sucai.com/auth",
                LoginName = "1280160831@qq.com",
                LoginPwd = "*************",
                SignRequestUrl = "http://www.17sucai.com/member/signin",
            };
            model.LoginPwd = Encrypt.SHA1Encrypt(model.LoginPwd).ToLower();
            GetToken(model);
            //登陆
            PostLogin(model);
            if (model.Result.Html.Contains("ret\":1"))//登陆成功
            {
                PostSign(model);
            }
        }

        private void PostSign(WebSiteModel model)
        {
            var result = http.GetHtml(new HttpItem()
            {
                URL = model.SignRequestUrl,
                Cookie = model.Result.Cookie,
                Referer = model.SiteDomin,
                Method = "Get",
                Timeout = 100000,//连接超时时间     可选项默认为100000
                ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                //UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.94 Safari/537.36",//用户的浏览器类型，版本，操作系统     可选项有默认值
                Accept = "application/json, text/javascript, */*",//    可选项有默认值
                ContentType = "application/x-www-form-urlencoded",//返回类型    可选项有默认值    
            });
            model.Result = result;
        }

        private void PostLogin(WebSiteModel model)
        {
            model.Token = System.Web.HttpUtility.UrlEncode(model.Token);
            model.LoginName = System.Web.HttpUtility.UrlEncode(model.LoginName);
            string postData = string.Format("email={0}&password={1}&token={2}", model.LoginName, model.LoginPwd, model.Token);
            HttpItem item = new HttpItem()
            {
                URL = model.LoginUrl,
                Postdata = postData,
                Cookie = model.Cookie,
                Referer = model.SiteDomin,
                Method = "POST",
                Timeout = 100000,//连接超时时间     可选项默认为100000
                ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                //UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.94 Safari/537.36",//用户的浏览器类型，版本，操作系统     可选项有默认值
                Accept = "application/json, text/javascript, */*",//    可选项有默认值
                ContentType = "application/x-www-form-urlencoded",//返回类型    可选项有默认值                
            };
            var result = http.GetHtml(item);
            model.Result = result;
        }

        private static void GetToken(WebSiteModel model)
        {
            var result = http.GetHtml(new HttpItem()
            {
                URL = model.SiteDomin
            });
            model.Result = result;
            model.Token = RegexHelper.GeMidStringValue(result.Html, "var token = '", "';");
            model.Cookie = result.Cookie;
        }
    }
}
