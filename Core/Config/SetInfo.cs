using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Config
{
    /// <summary>
    /// 配置文件
    /// </summary>
    [Serializable]
    public class SetInfo
    {
        public static string GetValue(string skey, string[] setting)
        {
            string ss = string.Empty;

            foreach (var s in setting)
                if (s.StartsWith(skey + "=")) ss = s.Split('=')[1];
            return ss;
        }
    }
}
