using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMChat.Common;
using IMChat.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace chatNow.Common
{
    public class MongoDBHelper
    {
        //根据数据库名称获取数据库
        public static MongoDatabase GetDatabase(string baseName)
        {
            MongoClientSettings mcs = new MongoClientSettings();
            mcs.Server = new MongoServerAddress(XMLHelper.Instance[SettingEnum.DataSetting, "MongoDB"]);
            MongoClient mc = new MongoClient(mcs);
            MongoServer ms = mc.GetServer();
            return ms.GetDatabase(baseName);
        }

        //创建数据库
        public static MongoDatabase CreateMongoDatabase(string basename)
        {
            MongoClient mc = new MongoClient();
            MongoClientSettings mcs = new MongoClientSettings();
            mcs.Server = new MongoServerAddress(XMLHelper.Instance[SettingEnum.DataSetting, "MongoDB"]);
            MongoServer ms = mc.GetServer();
            MongoDatabaseSettings mds = ms.CreateDatabaseSettings(basename);
            return new MongoDatabase(ms, mds);
        }

        //创建集合
        public static CommandResult CreateDataCollection(string baseName, string collName)
        {
            MongoDatabase md = GetDatabase(baseName);
            return md.CreateCollection(collName);
        }
        //获取集合
        public static MongoCollection GetCollection(string basename, string collectionname)
        {
            MongoDatabase md = MongoDBHelper.GetDatabase(basename);
            return md.GetCollection(collectionname);
        }
        //验证登录用户
        public static Users GetUsers(string username, string pwd)
        {
            MongoCollection mc = GetCollection("Chatting", "Users");
            return mc.AsQueryable<Users>().Where(m => m.UserName == username).ToList()[0];
        }
        //通过id找到用户
        internal static Users GetUserByID(string userData)
        {
            MongoCollection mc = GetCollection("Chatting", "Users");
            return mc.AsQueryable<Users>().Where(m => m.UserId == ObjectId.Parse(userData)).ToList()[0];
        }
        //修改登录状态
        public static bool ChangeStatus(int status, ObjectId id)
        {
            MongoCollection mc = GetCollection("Chatting", "Users");
            WriteConcernResult wcr = mc.Update(Query.EQ("_id", id), new UpdateBuilder().Set("Status", status));
            return wcr.Ok;
        }
        //修改用户资料
        public static bool UpdateUser(Users user, ObjectId id)
        {
            //把id改成当前id
            user.UserId = id;
            MongoCollection mc = GetCollection("Chatting", "Users");
            if (mc.Remove(Query.EQ("_id", id)).Ok)
                return mc.Insert<Users>(user).Ok;
            return false;
        }

        //根据SSO标记获取用户信息
        public static SSOUserActive Get(string val)
        {
            MongoCollection mc = GetCollection("Chatting", "SSOUser");
            SSOUserActive active = mc.FindOneAs<SSOUserActive>(Query.EQ("_id", ObjectId.Parse(val)));
            //单点登录标记过期（过期时间20分钟）
            //momgo时间为utc时间，需转化为当地时间
            if (active != null)
                if (active.ActiveTime.ToLocalTime() < DateTime.Now)
                    return null;
            return active;
        }
        //删除用户资料
        public static bool RemoveUser(Users user)
        {
            MongoCollection mc = GetCollection("Chatting", "Users");
            return mc.Remove(Query.EQ("_id", user.UserId)).Ok;
        }
        //修改用户密码
        public static bool ChangePwd(string newpwd, string userid, string salt)
        {
            MongoCollection mc = GetCollection("Chatting", "Users");
            return mc.Update(Query.EQ("_id", ObjectId.Parse(userid)), new UpdateBuilder().Set("Hash", PwdSalt.EncodePassword(newpwd, salt))).Ok;
        }
    }
}