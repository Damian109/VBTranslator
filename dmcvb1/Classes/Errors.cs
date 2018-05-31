using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmcvb1.Classes
{
    //Класс, предназначенный для обрботки ошибок.
    //Работает по кодам ошибок
    //Выдает сообщение об ошибке в виде текста
    public class Errors
    {
        //Сообщение об ошибке
        String Error;
        //Текущий код
        Byte ErrorCode;
        //Конструктор
        public Errors()
        {
            Error = "Всё в порядке";
            ErrorCode = 0;
        }
        //Обработка кода ошибки, в качестве параметров принимает код ошибки и номер строки
        public String FindErrors(Byte code, int di)
        {
            ErrorCode = code;
            if (code != 0)
                Error = "Ошибка в ";
            if (code < 5)
                Error += "лексическом анализаторе. ";
            if (code > 4 && code <= 20)
                Error += "синтаксическом анализаторе. ";
            if (code > 20)
                Error += "разборе сложного арифм.выражения. ";
            switch (code)
            {
                    //Лексика
                case 1:
                    Error += "Неизвестный символ в строке " + di.ToString() + "\n";
                    break;
                case 2:
                    Error += "Идентификатор в строке " + di.ToString() + " превышает максимально допустимую длину\n";
                    break;
                case 3:
                    Error += "Идентификатор в строке " + di.ToString() + " некорректен\n";
                    break;
                case 4:
                    Error += "Разделитель находится в начале строки " + di.ToString() + "\n";
                    break;
                    //Синтаксис
                case 5:
                    Error += "Проверяемая строка " + di.ToString() + " не подходит под грамматику языка\n";
                    break;
                case 6:
                    Error += "Отсутствует знак конца выражения в строке " + di.ToString() + "\n";
                    break;
                case 7:
                    Error += "Выражения с такой конструкцией как в строке " + di.ToString() + " не поддерживаются\n";
                    break;
                case 8:
                    Error += "Ошибка в объявлении имен переменных в строке " + di.ToString() + "\n";
                    break;
                case 9:
                    Error += "Задан неизвестный тип переменной в строке " + di.ToString() + "\n";
                    break;
                case 10:
                    Error += "Ошибка в конструкции оператора присваивания в строке " + di.ToString() + "\n";
                    break;
                case 11:
                    Error += "Выражение справа не является идентификатором, литералом или сложным выражением в строке " + di.ToString() + "\n";
                    break;
                case 12:
                    Error += "Конструкция присваивания в строке " + di.ToString() + " не должна содержать ключевых слов\n";
                    break;
                case 13:
                    Error += "После Select отсутствует ключевое слово case в строке " + di.ToString() + "\n";
                    break;
                case 14:
                    Error += "Оператор Select case задан без идентификатора в строке " + di.ToString() + "\n";
                    break;
                case 15:
                    Error += "После Select case отсутствует символ конца строки в строке " + di.ToString() + " не поддерживаются\n";
                    break;
                case 16:
                    Error += "Конструкция select case неверно завершена в строке " + di.ToString() + "\n";
                    break;
                case 17:
                    Error += "Значение в конструкции case не является числом или идентификатором в строке " + di.ToString() + "\n";
                    break;
                case 18:
                    Error += "Отсутствует знак конца выражения после конструкции case в строке " + di.ToString() + "\n";
                    break;
                case 19:
                    Error += "Ошибка построения конструкции case в строке " + di.ToString() + "\n";
                    break;
                case 20:
                    Error += "Присутствуют конструкции case после case else в строке " + di.ToString() + "\n";
                    break;
                    //Разбор сложных выражений
                case 21:
                    Error += "Отсутствует одна или несколько закрывающих скобок";
                    break;
                case 22:
                    Error += "Присутствует одна или несколько лишних закрывающих скобок";
                    break;
                case 23:
                    Error += "В выражении присутствует символ, не учавствующий в разборе";
                    break;
            }
            return Error;
        }
    }
}
