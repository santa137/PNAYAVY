using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Singleton single = Singleton.GetInstance();
            single.flag = true;
            Console.WriteLine(single.flag);

            Singleton single2 = Singleton.GetInstance();
            single2.flag = false;
            Console.WriteLine(single.flag);

            Console.ReadKey();
        }
    }
}
