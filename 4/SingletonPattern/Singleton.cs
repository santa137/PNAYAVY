using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingletonPattern
{
    // Класс должен быть запечатаннным (модификатор sealed).
    // Запрет создания наследников.
    sealed class Singleton
    {  

        // Закроем конструктор для доступа из вне.
        private Singleton() { }

        // Один экземпляр класса, который будет создаваться при первом обращении к классу.
        private static Singleton single = new Singleton();

        // Метод, который будет возвращать ссылку.
        public static Singleton GetInstance()
        {
            return single;
        }

        public bool flag;

    }
}
