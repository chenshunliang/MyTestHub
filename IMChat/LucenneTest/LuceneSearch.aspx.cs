using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LucenneTest
{
    public partial class LuceneSerrch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string kw = Request["search"];
            string text = LuceneHandle.Search(kw);
            Response.Write(text);
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            string kw = Request["search"];
            if (LuceneHandle.DelIndex(kw))
                Response.Write("删除了");
        }
    }
}