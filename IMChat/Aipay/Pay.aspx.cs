﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Aipay.log;

namespace Aipay
{
    public partial class Pay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 功能：合并交易支付接口接入页
        /// 版本：3.3
        /// 日期：2012-07-05
        /// 说明：
        /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
        /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
        /// 
        /// /////////////////注意///////////////////////////////////////////////////////////////
        /// 如果您在接口集成过程中遇到问题，可以按照下面的途径来解决
        /// 1、商户服务中心（https://b.alipay.com/support/helperApply.htm?action=consultationApply），提交申请集成协助，我们会有专业的技术工程师主动联系您协助解决
        /// 2、商户帮助中心（http://help.alipay.com/support/232511-16307/0-16307.htm?sh=Y&info_type=9）
        /// 3、支付宝论坛（http://club.alipay.com/read-htm-tid-8681712.html）
        /// 
        /// 如果不想使用扩展功能请把扩展功能参数赋空值。
        /// </summary>
        protected void BtnAlipay_Click(object sender, EventArgs e)
        {

            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("partner", "2088801249109600");// Config.Partner
            sParaTemp.Add("_input_charset", "utf-8");//Config.Input_charset.ToLower()
            sParaTemp.Add("service", "create_direct_pay_by_user");

            sParaTemp.Add("seller_email", "1083987149@qq.com");
            sParaTemp.Add("sign_type", "RSA");
            //sParaTemp.Add("notify_url","http://www.aaa.com");
            //sParaTemp.Add("return_url", "http://www.aaa.com");
            sParaTemp.Add("out_trade_no", "532836525123");
            sParaTemp.Add("subject", "笔记本");
            sParaTemp.Add("payment_type", "1");
            sParaTemp.Add("defaultbank", "CMB");

            //string str = Submit.BuildRequestMysign

            //建立请求
            string sHtmlText = Submit.BuildRequest(sParaTemp, "post", "确定支付");

            Response.Write(sHtmlText);

            //请在这里加上商户的业务逻辑程序代码



        }
    }
}