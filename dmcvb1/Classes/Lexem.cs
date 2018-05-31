using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmcvb1.Classes
{
    /*Класс Лексема - родительский*/
    public class Lexem
    {
        /*Внутренняя строка для хранения лексемы*/
        protected String MyString;
        /*Внутренняя строка для хранения имени типа*/
        protected String MyTypeName;
        /*Внутреннее значение для хранения кода типа*/
        protected Byte MyCodeType;
        //Номер таблицы
        public Byte TableNumber;
        //Номер элемента в таблице
        public Int32 ItemNumber;
        //Переопределение конструктора
        public Lexem()
        {
            MyString = "";
            MyTypeName = "Lexem";
            MyCodeType = TableNumber = 0;
            ItemNumber = 0;
        }
        //Занесение значения в лексему
        public void Set(String str)
        {
            MyString = str;
        }
        //Запрос значения из лексемы
        public String Get()
        {
            return MyString;
        }
        //Получение типа в виде строки
        public String GetTypeString()
        {
            return MyTypeName;
        }
        //Получение кода типа
        public Byte GetCodeType()
        {
            return MyCodeType;
        }
    }

    /*Класс Терминал(ключевое слово)*/
    public class Terminal : Lexem
    {
        public Terminal()
        {
            MyTypeName = "Terminal";
            MyCodeType = 1;
        }
    }

    /*Класс Разделитель*/
    public class Delimiter : Lexem
    {
        public Delimiter()
        {
            MyTypeName = "Delimiter";
            MyCodeType = 2;
        }
    }

    /*Класс Число*/
    public class Literal : Lexem
    {
        public Literal()
        {
            MyTypeName = "Literal";
            MyCodeType = 3;
        }
    }

    /*Класс Идентификатор*/
    public class Identifier : Lexem
    {
        public Identifier()
        {
            MyTypeName = "Identifier";
            MyCodeType = 4;
        }
    }

    /*Класс стандартный символ*/
    public class StandartSymbol : Lexem
    {
        public StandartSymbol()
        {
            MyTypeName = "StandartSymbol";
            MyCodeType = 5;
        }
    }
}