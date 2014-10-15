using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelImportToSql;

namespace WebTest
{
    public partial class DataLinkage : System.Web.UI.Page
    {
        protected string conStr = System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData("0", this.first);
            }
        }


        private void LoadData(string pid, DropDownList instance)
        {
            SqlParameter[] dbparams = new SqlParameter[] { 
            SQLHelper.MakeParam("@PID", System.Data.SqlDbType.VarChar,20,pid)
            };
            SqlDataReader reader = SQLHelper.ExecuteReader(conStr, System.Data.CommandType.StoredProcedure, "Tree_GetByID", dbparams);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            while (reader.Read())
            {
                string str = reader.GetString(reader.GetOrdinal("JobName"));
                string cid = reader.GetString(reader.GetOrdinal("ChildrenID"));
                dic.Add(str, cid);
            }
            instance.DataSource = dic.Keys;
            instance.DataBind();
        }

        protected void first_TextChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;

            LoadData("0", this.second);
        }
    }
}