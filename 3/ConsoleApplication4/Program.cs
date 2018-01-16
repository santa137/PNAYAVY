using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class KONSTRYCTOR
    {
        private string konstr;
        public KONSTRYCTOR(string kons)
        {
            this.konstr = kons;
        }
        public override string ToString()
        {
            return konstr;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("//////////////////////////");
            Console.WriteLine("//////СТЕК////////////////");
            Console.WriteLine("//////////////////////////");

            Stack<KONSTRYCTOR> stack = new Stack<KONSTRYCTOR>();
            stack.Push(new KONSTRYCTOR("34"));
            stack.Push(new KONSTRYCTOR("25"));
            stack.Push(new KONSTRYCTOR("78"));
            stack.Push(new KONSTRYCTOR("45"));
            stack.Push(new KONSTRYCTOR("34"));
            stack.Push(new KONSTRYCTOR("25"));
            stack.Push(new KONSTRYCTOR("78"));
            stack.Push(new KONSTRYCTOR("45"));
            Console.WriteLine(stack.get(2));
            Console.WriteLine(stack.Pop());
            Console.WriteLine(stack.Peek());

            Console.WriteLine("//////////////////////////");
            Console.WriteLine("//////ОЧЕРЕДЬ/////////////");
            Console.WriteLine("//////////////////////////");

            Queue <KONSTRYCTOR> queue = new Queue<KONSTRYCTOR>();
            queue.Push(new KONSTRYCTOR("1"));
            queue.Push(new KONSTRYCTOR("2"));
            queue.Push(new KONSTRYCTOR("3"));

            Console.WriteLine(queue.Pop());
            Console.WriteLine(queue.get(0));
            Console.WriteLine(queue.size());

            Console.WriteLine("//////////////////////////");
            Console.WriteLine("//////ARRAY///////////////");
            Console.WriteLine("//////////////////////////");

            Array<KONSTRYCTOR> array = new Array<KONSTRYCTOR> { };
            array.Add(new KONSTRYCTOR("13"));
            array.Add(new KONSTRYCTOR("46"));
            array.Add(new KONSTRYCTOR("10"));
            array.Add(new KONSTRYCTOR("32"));

            Console.WriteLine(array.get(0));
            Console.WriteLine(array.get(1));
            Console.WriteLine(array.get(2));
            Console.WriteLine(array.Count);
            Console.ReadKey();
        }
    }
}
