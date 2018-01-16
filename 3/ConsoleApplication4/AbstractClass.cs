using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    abstract class AbstractClass<T>
    {
        protected T[] Array; //массив для хранения данных типа T
        protected const int defaultCapacity = 10; //вместимость по умолчанию, потом можно расширить
        protected static int CUT_RATE = 2; //Метка
        public int pointer = 0; // Размер

        public int Count //параметр для вывода размера 2
        {
            get
            {
                return this.size();
            }
        }

        public bool isEmpty() //проверка на пустоту
        {
            return this.size() == 0;
        }

        //Возвращает элемент списка по индексу
        public T get(int index)
        {
            return (T)Array[index];
        }

        /*Возвращает количество элементов в списке*/
        public int size()
        {
            return pointer;
        }

        /*метод изменения размера.*/
        public void resize(int newlength)
        {
            T[] newarray = new T[newlength];
            System.Array.Copy(Array, 0, newarray, 0, pointer);
            Array = newarray;
        }
    }
}
