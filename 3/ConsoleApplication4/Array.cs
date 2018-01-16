using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Array<T> : AbstractClass<T>
    {
        public Array()
        {
            this.Array = new T[defaultCapacity];
        }

        public Array(int Capacity)
        {
            this.Array = new T[Capacity];
        }

        public void Add(T item) //Добавляет новый элемент в список.
        {
            if (pointer == Array.Length - 1)
                resize(Array.Length * 2);
            Array[pointer++] = item;
        }

        public void Remove(int index)
        {
            for (int i = index; i < pointer; i++)
            {
                Array[i] = Array[i + 1];
                Array[pointer] = default(T);
                pointer--;
            }
            if (Array.Length > defaultCapacity && pointer < Array.Length / CUT_RATE)
                resize(Array.Length / 2); // если элементов в CUT_RATE раз меньше чем длина массива, то уменьшим в два раза
        }
    }
}
