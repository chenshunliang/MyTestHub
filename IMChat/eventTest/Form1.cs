using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eventTest
{
    public partial class Form1 : Form
    {
        //自定义事件
        public event MyEvent myEvent;

        public Form1()
        {
            InitializeComponent();
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            //触发事件
            if (myEvent != null)
                MessageBox.Show(myEvent(txt.Text.Trim()));
        }
    }
}
