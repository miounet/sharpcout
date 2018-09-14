using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Comm
{
    public class Enum
    {
        public enum RunOS:int
        {
            Windowns=0,
            Linux=1,
            Mac=2,
        }

        public enum OsVersion : int
        {
            WinXp=0,
            Win2008=1,
            Win7=2,
            Win8=3,
        }

        public enum OutType : short
        {
            /// <summary>
            /// 中文
            /// </summary>
            CN=0,
            /// <summary>
            /// 英文
            /// </summary>
            EN=1,
        }



    }
}
