using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShare.VideoPluggins.Core.Common.Loger
{
    /// <summary>
    /// 日志接口
    /// </summary>
    public interface ILoger
    {
        /// <summary>
        /// 写入普通信息
        /// </summary>
        /// <param name="msg">信息</param>
        void Info(string msg);

        /// <summary>
        /// 写入调试信息
        /// </summary>
        /// <param name="msg">信息</param>
        void Dedug(string msg);

        /// <summary>
        /// 写入错误信息
        /// </summary>
        /// <param name="msg">信息</param>
        void Error(string msg);
    }
}
