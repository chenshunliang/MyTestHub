using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebApi.Tool
{
    public class ErrorMsg
    {
        public void ResException()
        {
            //响应消息
            HttpResponseMessage msg = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
            {
                Content = new StringContent("this page is not found"),
                ReasonPhrase = "",
                StatusCode = System.Net.HttpStatusCode.NotFound
            };
            throw new HttpResponseException(msg);
        }
    }
}