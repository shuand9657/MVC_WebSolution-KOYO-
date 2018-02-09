using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.EnumData
{
    public enum CultureEnum
    {
        /// <summary>
        /// 英文
        /// </summary>
        [Description("en-US")]
        enUS = 0,

        /// <summary>
        /// 中文(繁體)
        /// </summary>
        [Description("zh-TW")]
        zhTW = 1,

        ///<summary>
        ///日文
        ///</summary>
        [Description("jp-JP")]
        jpJP = 2,

        /// <summary>
        /// 中文(简体)
        /// </summary>
        [Description("zh-CN")]
        zhCN = 3
    }
}
