using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Stack<T> : AbstractClass<T>
    {
        public Stack()
        {
            this.Array = new T[defaultCapacity];
        }

        public Stack(int Capacity)
        {
            this.Array = new T[Capacity];
        }

        //Возвращает объект в верхней части Stack без его удаления.
        public T Peek()
        {
            if (pointer == 0)
            {
                throw new InvalidOperationException();
            }
            return this.Array[pointer - 1];
        }

        public T Pop() //Удаляет и возвращает объект, находящийся в начале Stack.
        {
            if (this.size() == 0)
            { //вброс ошибки при взятии с пустого стека (Overflow)
                throw new InvalidOperationException();
            }
            return this.Array[--pointer];
        }

        public void Push(T obj)
        {
            if (pointer == Array.Length) //если у нас переполнение...
            {
                T[] tmp = Array;
                Array = null;
                Array = new T[2 * tmp.Length];
                for (int i = 0; i <= tmp.Length; i++)
                {
                    Array[i] = tmp[i];
                }
            }
            else if ((pointer * 3) >= Array.Length)
            {
                T[] tmp = Array;
                Array = new T[Array.Length / 2];
                Array = tmp;
            }
            Array[pointer] = obj;
            pointer++;
        }
    }
}
