using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmcvb1.Classes
{
    /*Здесь мы храним текст из файла, с которым работаем*/
    public class Text : Lexem
    {
        /*Замена символов конца строки на разделитель #*/
        public void ConvertEndOfStrings()
        {
            /*Создаем массив символов*/
            char[] tmp = new char[MyString.Length];
            tmp = MyString.ToCharArray();
            int i;
            /*Если встречается символ конца строки - меняет на #*/
            for (i = 0; i < MyString.Length; i++)
            {
                if (tmp[i] == '\n')
                    tmp[i] = '#';
            }

            /*Переписывает текущий текст с учетом изменений*/
            MyString = "";
            for (int j = 0; j < i; j++)
            {
                /*Убираем знаки табуляции*/
                if (!(tmp[j] == '\t'))
                    MyString += tmp[j];
            }
            MyString += "#\0";
        }

        /*Замена символов-разделителей # на символы конца строки*/
        public void ConvertSharps()
        {
            /*Создаем массив символов*/
            char[] tmp = new char[MyString.Length];
            tmp = MyString.ToCharArray();
            int i;
            /*Если встречается символ # - меняет на символ конца строки */
            for (i = 0; i < MyString.Length; i++)
            {
                if (tmp[i] == '#')
                    tmp[i] = '\n';
            }
            /*Переписывает текущий текст с учетом изменений*/
            MyString = "";
            for (int j = 0; j < i; j++)
                MyString += tmp[j];
            MyString += '\0';
        }

        /*Прибавить к тексту строку*/
        public void AddText(String str)
        {
            MyString += str;
        }

        /*Очистить текстовую переменную*/
        public void ClearText()
        {
            MyString = "";
        }
    }
}