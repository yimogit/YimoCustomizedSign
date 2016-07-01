using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YimoCore;
using System.Configuration;
namespace Yimo.Sign._17Sucai
{
    public class MemberSignin : IMemberSignin
    {

        public SignModel ExecSign()
        {
            SuCaiWangService service = new SuCaiWangService();
            var users = PluginDescriptor.Data.FromJsonString<List<UserModel>>();
            SignModel model = new SignModel();
            foreach (var user in users)
            {
                var sign = service.BeginSign(user.UserName, user.UserPwd);
                model.Msg += sign.Msg;
                model.IsSuccess = sign.IsSuccess;
                model.Count++;
            }
            return model;
        }
        #region 签到

        #endregion


        public PluginDescriptor PluginDescriptor { get; set; }


        public int DisplayOrder { get; set; }
    }
}
