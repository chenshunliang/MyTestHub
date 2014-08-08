using System;
using System.Threading;
using MemcachedProviders.Cache;

namespace MemcachedProviders
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string key = "myName";
            string value = "Dylan";
            bool result = false;
            string val = string.Empty;
            #region 存/取最简单的数据类型
            //如果缓存中没有，就尝试着去存入缓存
            if (DistCache.Get(key) == null)
            {
                //DistCache.DefaultExpireTime = 1200;//缓存时间
                result = DistCache.Add(key, value);           //存数据  
                if (result)
                {
                    //如果存入成功，就试着去取
                    Thread.Sleep(500);
                    string ret = (string)DistCache.Get(key);            //读数据  
                    //Assert.AreEqual(value, ret);                     //验证  
                    if (ret != null)
                    {
                        Response.Write(ret);
                        Response.Write("<br/>");
                    }
                    else
                    {
                        //取出来的值为null，直接移除该缓存对象
                        DistCache.Remove(key);//移除
                        // DistCache.RemoveAll();//移除所有                     
                    }
                }
            }
            else
            {
                //缓存中有，直接拿数据
                string ret = (string)DistCache.Get(key);
                if (ret != null)
                {
                    Response.Write(ret);
                    Response.Write("<br/>");
                }
                else
                {
                    DistCache.Remove(key);
                }

            }
            #endregion

            #region  存/取一个Person对象
            Person person = new Person() { Id = 007, Name = "Dylan" };//new 一个Person对象的实例
            //如果缓存中没有，则尝试着放入缓存
            if (DistCache.Get<Person>("myObj") == null)
            {
                result = DistCache.Add("myObj", person);
                if (result)
                {
                    Thread.Sleep(500);
                    val = DistCache.Get("myObj").ToString();
                    if (val != null)
                    {
                        Response.Write(val);
                        Response.Write("<br/>");
                    }
                    else
                    {
                        DistCache.Remove("myObj");
                    }

                }
            }
            else
            {
                //缓存中已经有该对象，就直接从缓存取
                Person p = DistCache.Get<Person>("myObj");
                val = person.ToString();
                //也可以直接这样取
                // val = DistCache.Get("myObj").ToString();

                if (val != null)
                {
                    Response.Write(val);
                    Response.Write("<br/>");
                }
                else
                {
                    DistCache.Remove("myObj");
                }

            }
            #endregion
        }

    }



    [Serializable]
    public class Person
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// 重写Tostring()，方便输出验证
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Person:" + "{name:" + Name + ",id:" + Id + "}";
        }
    }
}