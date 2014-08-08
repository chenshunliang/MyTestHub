using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Enyim.Caching;

namespace MemcachedTestEnyim
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MemcachedClient mc = new MemcachedClient();

            mc.Store(Enyim.Caching.Memcached.StoreMode.Set, "Name", "chen");

            Response.Write(mc.Get("Name"));
        }
    }
}