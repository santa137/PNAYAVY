using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyString
{
    class Program
    {
      
        static void Main(string[] args)
        {
            MyString f;
            

            f = new MyString("С Новым годом");

            //Replace
            //f.SubString(i, j);
            //f.Replace('ы','д');
            //Console.WriteLine(MyString.ToArray(t));
            //Console.ReadKey();


            //Substring
            //int i = 2;
            //int j = 7;
            //Console.WriteLine(MyString.ToArray(f.SubString(i,j)));
            //Console.ReadKey();


            //Copy
            MyString t;
            t = new MyString();
            // t = MyString.Copy(f);
            //Console.WriteLine(MyString.ToArray(t));
            //Console.ReadKey();
            //Условие равенства

            //if (t == f)
            //{
            //   Console.WriteLine(MyString.ToArray(t));
            //   Console.WriteLine(MyString.ToArray(f));

            //}
            //else Console.WriteLine("jopa");
            //Console.ReadKey();

            //Find

            //int e = f.Find('ы');
            //Console.WriteLine(e);
            //Console.ReadKey();


            // e = f.Find('ы',6);
            //Console.WriteLine(e);
            //Console.ReadKey();

            // Replace

            //f.Replace('г', 'д', 5);
            //Console.WriteLine(MyString.ToArray(f));
            //Console.ReadKey();


            f = "GHbdtn";
            String stroka =(string) f;
        }
    }
}
