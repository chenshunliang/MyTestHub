using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using MvcTest.Common;

namespace MvcTest.Models
{
    public class Person
    {
        [DisplayName("姓名")]
        public string Name { get; set; }

        [DisplayName("年龄")]
        public int Age { get; set; }

        [DisplayName("性别")]
        [Domain("M", "F", ErrorMessageResourceName = "Gender", ErrorMessageResourceType = typeof(Person))]
        public char Gender { get; set; }

        [DisplayName("简介")]
        public string Instroduction { get; set; }
    }
}