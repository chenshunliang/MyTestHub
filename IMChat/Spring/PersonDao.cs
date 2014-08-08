using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring
{
    public class PersonDao : IPersonDao
    {
        public void Save()
        {
            Console.WriteLine("保存person");
        }
    }
}
