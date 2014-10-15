using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;

namespace ExcelImportToSql
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("开始导入。。。");
            //ExcelImport(false);
            Console.WriteLine("导入结束。。。");
            Console.ReadKey();
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="IsToSql"></param>
        static void ExcelImport(bool IsToSql)
        {
            FileStream fs = new FileStream("d:\\test.xls", FileMode.Open);
            byte[] fileContent = null;
            using (System.IO.Stream stream = fs)
            {
                fileContent = new byte[stream.Length];
                stream.Read(fileContent, 0, (int)stream.Length);

            };
            MemoryStream memstream = new MemoryStream(fileContent);
            HSSFWorkbook hssfworkbook = new HSSFWorkbook(memstream);
            HSSFSheet sheet = (HSSFSheet)hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            HSSFRow row = null;
            List<TreeModel> listTree = new List<TreeModel>();
            rows.MoveNext();
            HSSFRow hssfTitle = rows.Current as HSSFRow;
            string MaxCate = "0";
            string MidCate = "0";
            string MinCate = "0";
            string MaxCateCN = "";
            string MidCateCN = "";
            string MinCateCN = "";

            while (rows.MoveNext())
            {
                TreeModel model = new TreeModel();
                model.CreateDate = DateTime.Now;
                model.UpdateDate = DateTime.Now;
                model.IsValid = true;
                row = rows.Current as HSSFRow;
                //顶级类别
                if (row.Cells[0].ToString() != "")
                {
                    MaxCate = row.Cells[0].ToString();
                    MaxCateCN = row.Cells[3].ToString();
                    model.ChildrenID = MaxCate;
                    model.ParentID = "0";
                    model.IDPath = MaxCate;
                    model.IDPathCN = MaxCateCN;
                    model.JobName = MaxCateCN;
                }
                else if (row.Cells[1].ToString() != "")
                {
                    MidCate = row.Cells[1].ToString();
                    MidCateCN = row.Cells[3].ToString();
                    model.ChildrenID = MidCate;
                    model.ParentID = MaxCate;
                    model.IDPath = MaxCate + "," + MidCate;
                    model.IDPathCN = MaxCateCN + "," + MidCateCN;
                    model.JobName = MidCateCN;
                }
                else if (row.Cells[2].ToString() != "")
                {
                    MinCate = row.Cells[2].ToString();
                    MinCateCN = row.Cells[3].ToString();
                    model.ChildrenID = MinCate;
                    model.ParentID = MidCate;
                    model.IDPath = MaxCate + "," + MidCate + "," + MinCate;
                    model.IDPathCN = MaxCateCN + "," + MidCateCN + "," + MinCateCN;
                    model.JobName = MinCateCN;
                }
                else
                {
                    model.ChildrenID = "0";
                    model.ParentID = MinCate;
                    model.IDPath = MaxCate + "," + MidCate + "," + MinCate;
                    model.IDPathCN = MaxCateCN + "," + MidCateCN + "," + MinCateCN + "," + row.Cells[3].ToString();
                    model.JobName = row.Cells[3].ToString();
                }
                listTree.Add(model);
                Console.WriteLine("childrenID={0},parentID={1},idPath={2}", model.ChildrenID, model.ParentID, model.IDPath);
            }

            ImportSql(listTree);
        }

        /// <summary>
        /// 导入数据库
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        static bool ImportSql(IList<TreeModel> list)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

            DataTable dt = new DataTable();
            dt.Columns.Add("ChildrenID", typeof(string));
            dt.Columns.Add("ParentID", typeof(string));
            dt.Columns.Add("JobName", typeof(string));
            dt.Columns.Add("IsValid", typeof(bool));
            dt.Columns.Add("IDPath", typeof(string));
            dt.Columns.Add("IDPathCN", typeof(string));
            dt.Columns.Add("CreateDate", typeof(DateTime));
            dt.Columns.Add("UpdateDate", typeof(DateTime));

            foreach (var item in list)
            {
                DataRow dr = dt.NewRow();
                dr["ChildrenID"] = item.ChildrenID;
                dr["ParentID"] = item.ParentID;
                dr["JobName"] = item.JobName;
                dr["IsValid"] = item.IsValid;
                dr["IDPath"] = item.IDPath;
                dr["IDPathCN"] = item.IDPathCN;
                dr["CreateDate"] = item.CreateDate;
                dr["UpdateDate"] = item.UpdateDate;
                dt.Rows.Add(dr);
            }
            SqlParameter para = new SqlParameter("@TreeTable", dt);

            int count = SQLHelper.ExecuteNonQuery(connStr, CommandType.StoredProcedure, "Tree_Insert", para);

            return count > 0 ? true : false;
        }
    }
}
