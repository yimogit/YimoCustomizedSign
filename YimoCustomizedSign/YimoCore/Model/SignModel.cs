using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YimoCore
{
    public class SignModel
    {
        public SignModel()
        {
            this.Count = 0;
            this.IsSuccess = false;
            this.BeginSignTime = DateTime.Now;
        }
        /// <summary>
        /// 成员名称
        /// </summary>
        public string MemberName { get; set; }

        /// <summary>
        /// 尝试次数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 尝试状态
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 开始签到时间
        /// </summary>
        public DateTime BeginSignTime { get; set; }
    }
}
