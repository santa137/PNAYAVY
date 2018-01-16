using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Queue<T> : AbstractClass<T>
    {
        int capacity = defaultCapacity;
        int tail = 0;
        int head = -1;
        public Queue()
        {
            this.Array = new T[defaultCapacity];
        }

        public void test()
        {
            Console.WriteLine("Очередь:");
            for (int i = 0; i < Array.Length; i++)
            {
                Console.WriteLine(Array[i]);
            }
        }

        public void Push(T newElement) //добавление элемента в конец очереди
        {
            if (pointer == capacity)
            {
                T[] newQueue = new T[2 * capacity];
                System.Array.Copy(Array, 0, newQueue, 0, Array.Length);
                Array = newQueue;
                capacity *= 2;
            }
            pointer++;
            Array[tail++ % capacity] = newElement;
        }

        public T Pop() //удаляет объект из начала очереди и возвращает его
        {
            if (pointer == 0)
            {
                throw new InvalidOperationException();
            }
            pointer--;
            return Array[++head % capacity];
        }

        public T Peek() //Просмотр элемента на вершине очереди.
        {
            if (Count == 0)
                throw new InvalidOperationException();
            return Array[Count - 1];
        }

        public void Clear() //Очистка очереди
        {
            if (head < tail)
                System.Array.Clear(Array, head, base.Count);
            else
            {
                System.Array.Clear(Array, head, Array.Length - head);
                System.Array.Clear(Array, 0, tail);
            }
            head = 0;
            tail = 0;
            pointer = 0;
        }
    }
}
