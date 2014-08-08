using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eventTest
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 fm = new Form1();
            //事件注册方法
            fm.myEvent += fm_myEvent;
            Application.Run(fm);
        }

        //事件触发处理函数
        static string fm_myEvent(string text)
        {
            if (text.Trim() == "0")
                return "信号为零";
            else if (text.Trim() == "1")
                return "信号为壹";
            else
                return "信号不对";
        }
    }
}
