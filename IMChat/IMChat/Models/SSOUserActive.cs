using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using chatNow.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IMChat.Models
{
    public class SSOUserActive
    {
        public string UserName
        {
            get;
            set;
        }
        [BsonId]
        public ObjectId SignID { get; set; }
        //插入本地时间,将utc转换为当地时间
        //[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime ActiveTime { get; set; }

        public string UserID { get; set; }
    }
}