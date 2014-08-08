using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace IMChat.Common
{
    /// <summary>
    /// 操作配置文件
    /// </summary>
    public class XMLHelper
    {
        //单例
        public readonly static XMLHelper Instance = new XMLHelper();
        private XMLHelper()
        { }

        //获取xml配置的值，索引
        public string this[SettingEnum sets, string index]
        {
            get
            {
                return GetDataValue(sets, index);
            }
        }


        //获取的方法
        /// <summary>
        /// XML操作
        /// </summary>
        /// <param name="sets"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        private string GetDataValue(SettingEnum sets, string str)
        {
            switch (sets)
            {
                case SettingEnum.DataSetting:
                    return XMLGet("Data", "constr", str, "DataSetting");
                case SettingEnum.AppSetting:
                    return "";
                default:
                    LogHelper.WriteLog("无此枚举");
                    return "找不到此枚举";
            }
        }

        /// <summary>
        /// XML操作类
        /// </summary>
        /// <param name="configStr">节点名称</param>
        /// <param name="configAttr">节点属性名称</param>
        /// <param name="str">节点属性值</param>
        /// <param name="configName">配置文件名</param>
        /// <returns></returns>
        private string XMLGet(string configStr, string configAttr, string str, string configName)
        {
            XElement xe = XElement.Load(System.AppDomain.CurrentDomain.BaseDirectory + "/AppSettings/" + configName + ".config");
            XElement ele = xe.Elements(configStr).Where(xml => xml.Attribute(configAttr).Value == str).ToList()[0];
            return ele.Attribute("value").Value;
        }
    }
}