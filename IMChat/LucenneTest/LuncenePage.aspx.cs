using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LucenneTest
{
    public partial class LuncenePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAdd_Click1(object sender, EventArgs e)
        {
            string index = txtIndex.Text;
            string title = txtTitle.Text;
            string body = txtBody.Text;
            LuceneHandle.CreateIndex(index, title, body);
            Response.Write("OK");
        }

        protected void btnCombine_Click(object sender, EventArgs e)
        {
            LuceneHandle.Combine();
            Response.Write("OK");
        }

        protected void btnAnalys_Click(object sender, EventArgs e)
        {
            LuceneHandle.IndexReaderTest();
            Response.Write("OK");
        }
    }
}