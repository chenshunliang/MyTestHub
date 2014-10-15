using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelImportToSql
{
    /// <summary>
    /// 四级联动树实体
    /// </summary>
    public class TreeModel
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// 类别ID
        /// </summary>
        public string ChildrenID { get; set; }

        /// <summary>
        /// 父类别ID
        /// </summary>
        public string ParentID { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public string JobName { get; set; }

        /// <summary>
        /// 是否有效（1有效，0无效）
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// ID树
        /// </summary>
        public string IDPath { get; set; }

        /// <summary>
        /// ID树中文名称
        /// </summary>
        public string IDPathCN { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}
