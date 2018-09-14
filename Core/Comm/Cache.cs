using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace Core.Comm
{
    public class Cache
    {
        /// <summary>
        /// key按键队列
        /// </summary>
        public static Queue<KeysQueue> KeyQueue = new Queue<KeysQueue>();
        
    }

    //声明键盘钩子的封送结构类型 
    [StructLayout(LayoutKind.Sequential)]
    public class KeyboardHookStruct
    {
        public int vkCode;   //表示一个在1到254间的虚似键盘码 
        public int scanCode;   //表示硬件扫描码 
        public int flags;
        public int time;
        public int dwExtraInfo;
    }
    public class KeysQueue
    {
        private Keys _keydata = new Keys();
        private DateTime _datatime = DateTime.Now;
        private KeyboardHookStruct _myKeyboardHookStruct = new KeyboardHookStruct();

        public Keys KeyData
        {
            get { return this._keydata; }
            set { this._keydata = value; }
        }

        public DateTime DataTime
        {
            get { return this._datatime; }
            set { this._datatime = value; }
        }

        public KeyboardHookStruct MyKeyboardHookStruct
        {
            get { return this._myKeyboardHookStruct; }
            set { this._myKeyboardHookStruct = value; }
        }


    }
}
