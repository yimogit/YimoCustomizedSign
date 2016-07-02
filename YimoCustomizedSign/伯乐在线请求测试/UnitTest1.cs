using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yimo.Sign.JobBole;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using YimoCore;

namespace 伯乐在线请求测试
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            JobBoleService service = new JobBoleService();
            var sign = service.BeginSign("用户名", "密码");  
        }
        [TestMethod]
        public void TestMethod2()
        {
            string str = "%E4%BB%80%E4%B9%88%E9%83%BD%E4%B8%8D%E5%BF%85+%E5%86%99%E4%BB%A3%E7%A0%81%E6%9D%A5%E7%9A%84%E6%84%89%E5%BF%AB";
            str = str.UrlDecode();
            string source = "我爱";
            string s = source.ToUnicode();
        }

    }
}
