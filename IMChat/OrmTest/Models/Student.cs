using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrmTest.MapAttr;

namespace OrmTest.Models
{
    [Table(Name = "Student")]
    public class Student
    {
        [Id(Name = "StuID", Strategy = GenerationType.INDENTITY)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }
}