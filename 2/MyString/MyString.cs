using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyString
{
    class MyString
    {
        protected char[] str;
        public MyString()
        {
            str = new char[256];
        }
        public MyString(int init_size)
        {
            str = new char[init_size];
        }
        public MyString(String StdStr)
        {
            str = new char[StdStr.Length];
            str = StdStr.ToCharArray();
        }
        public MyString(char[] arr)
        {
            str = new char[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                str[i] = arr[i];
            }
        }
        public MyString(MyString previousMyString)
        {
            if (this == previousMyString)
            {
                throw new ArgumentException("Нельзя копировать строку саму в себя!");
            }
            str = new char[previousMyString.str.Length];
            for (int i = 0; i < previousMyString.str.Length; i++)
            {
                str[i] = previousMyString.str[i];
            }
        }

        public int Lenght
        {
            get
            {
                return str.Length;
            }
            set
            {
                char[] temp = new char[value];
                if (value > str.Length)
                {
                    value = str.Length;
                }
                for (int i = 0; i < value; i++)
                {
                    temp[i] = str[i];
                    str = temp;
                }
            }
        }
        public char this[int index]
        {
            get
            {
                return str[index];
            }
            set
            {
                str[index] = value;
            }
        }
        public static MyString operator +(MyString s1, MyString s2)
        {
            int L = s1.str.Length + s2.str.Length;
            var sumstr = new MyString(L);
            for (int i = 0; i < s1.str.Length; i++)
            {
                sumstr[i] = s1[i];
            }
            for (int i = 0; i < s2.str.Length; i++)
            {
                sumstr[s1.str.Length + i] = s2[i];
            }
            return sumstr;
        }
        public static bool operator ==(MyString s1, MyString s2)
        {
            if ((object)s1 == null || (object)s2 == null)
            {
                return false;
            }
            if (s1.str.Length != s2.str.Length)
            {
                return false;
            }
            for (int i = 0; i < s1.str.Length; i++)
            {
                if (s1[i] != s2[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator !=(MyString s1, MyString s2)
        {
            return !(s1 == s2);
        }


        // Преобразование типов "MyString" -> "string".
        public static explicit operator String(MyString MyStr)
        {
            return new String(ToArray(MyStr));
        }
        public static MyString ToMyString(char[] arr)
        {
            MyString res = new MyString(arr.Length);
            for (int i = 0; i < arr.Length; i++)
            {
                res.str[i] = arr[i];
            }
            return res;
        }

        // Преобразование типов "string" -> "MyString".
        public static implicit operator MyString(String Str)
        {
            return new MyString(ToMyString(Str.ToCharArray()));
        }

        public int Find(MyString findstr)
        {
            if (Lenght < findstr.Lenght)
            {
                return -1;
            } 0
            for (int i = 0; i < Lenght - findstr.Lenght; i++)
            {
                for (int j = 0; j < findstr.Lenght; j++)
                {
                    if (str[i + j] != findstr[j]) { goto P1; }
                }
                return i;
                P1:;
            }
            return -1;
        }
        public int Find(char symbol)
        {
            for (int i = 0; i < Lenght; i++)
            {
                if (str[i] == symbol)
                {
                    return i;
                }
            }
            return -1;
        }

        public static MyString ToArray(char[] arr)
        {
            int p;
            MyString res = new MyString(arr.Length);
            p = arr.Length;
            for (int i = 0; i < p; i++)
            {
                res.str[i] = arr[i];
            }
            return res;
        }

        public int Find(char symbol, int item)
        {
            for (int i = item; i < Lenght; i++)
            {
                if (str[i] == symbol)
                {
                    return i;
                }
            }
            return -1;
        }
        public MyString SubString(int i, int j)
        {
            MyString tmp = new MyString(j);
            for (int a = 0; a < j; a++)
                tmp.str[a] = this.str[a + i];
            return tmp;

        }

        public void Replace(char oldChar, char newChar)
        {

            for (int i = 0; i < this.Lenght; i++)
                if (this.str[i] == oldChar) this.str[i] = newChar;

        }
        public void Replace(char oldChar, char newChar, int e)
        {

            for (int i = e; i < this.Lenght; i++)
                if (this.str[i] == oldChar) this.str[i] = newChar;

        }
        unsafe public static MyString Copy(MyString str)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }


            MyString tmp = new MyString();
            tmp.str = str.str;
            return tmp;


        }
        public static char[] ToArray(MyString MyStr)
        {
            char[] res = new char[MyStr.Lenght];
            for (int i = 0; i < MyStr.Lenght; i++)
            {
                res[i] = MyStr.str[i];
            }
            return res;
        }

        public override int GetHashCode()
        {
            var hashCode = -165003458;
            hashCode = hashCode * -1521134295 + EqualityComparer<char[]>.Default.GetHashCode(str);
            hashCode = hashCode * -1521134295 + Lenght.GetHashCode();
            hashCode = hashCode * -1521134295 + this.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            var @string = obj as MyString;
            return @string != null &&
                   EqualityComparer<char[]>.Default.Equals(str, @string.str) &&
                   Lenght == @string.Lenght;
        }
        

    }
}
