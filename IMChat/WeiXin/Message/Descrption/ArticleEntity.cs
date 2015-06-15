using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXin.Message.Descrption
{
    /// <summary>
    /// 文章实体
    /// </summary>
    [System.Xml.Serialization.XmlRoot(ElementName = "xml")]
    public class ArticleEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string PicUrl { get; set; }

        /// <summary>
        /// 文章地址
        /// </summary>
        public string Url { get; set; }
    }
}