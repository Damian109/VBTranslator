using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;
using dmcvb1.Classes;

namespace dmcvb1.Lexic
{
    /*Класс Tаблица*/
    public class Table
    {
        /*Столбец в виде массива ArrayList*/
        protected ArrayList MyLexems;
        /*Внутренняя строка для хранения имени типа*/
        protected String MyTypeName;
        //Функция для очистки таблицы
        /*******/
        public void Clear()
        {
            MyLexems = null;
        }
        /*Переопределение конструктора*/
        public Table()
        {
            MyLexems = new ArrayList();
            MyTypeName = "Table";
        }
        /*Вернуть тип*/
        public String GetTypeString()
        {
            return MyTypeName;
        }
        /*Запись строки в таблицу*/
        public void SetTableString(Lexem lit)
        {
            MyLexems.Add(lit);
        }
        /*Запись массива лексем в таблицу*/
        public void SetTable(Lexem[] lit)
        {
            for (int i = 0; i < lit.Length; i++)
            {
                MyLexems.Add(lit[i]);
            }
        }
        /*Изъятие значений строки из таблицы*/
        public virtual Lexem[] GetTableString()
        {
            //Вычисляем количество лексем в таблице для создания массива
            System.Collections.IEnumerator MyEnumenator = MyLexems.GetEnumerator();
            int tmp = 0;
            while (MyEnumenator.MoveNext())
            {
                tmp++;
            }
            //Создаем массив лексем для возврата
            Lexem[] tmpList = new Lexem[tmp];
            //Заполняем
            for (int i = 0; i < tmp; i++)
            {
                tmpList[i] = (Lexem)MyLexems[i];
            }

            return tmpList;
        }

        //Узнаем количество элементов в массиве
        public int GetCountLexem()
        {
            //Вычисляем количество лексем в таблице для создания массива
            System.Collections.IEnumerator MyEnumenator = MyLexems.GetEnumerator();
            int tmp = 0;
            while (MyEnumenator.MoveNext())
            {
                tmp++;
            }
            return tmp;
        }
        //
        //Функция для обработки listbox-ов
        public void FillList(ref ListBox lb, Byte param1, Byte param2)
        {
            //Используем лексемы из текущей таблицы
            Lexem[] m1 = new Lexem[this.GetCountLexem()];
            m1 = this.GetTableString();
            for (int i = 0; i < m1.Length; i++)
            {
                //Листбоксы заполняются по двум параметрам
                //********//
                //Первое условие
                //Выводим саму лексему и её тип
                if (param1 == 1 && param2 == 2)
                    lb.Items.Add(m1[i].Get() + "\t" + m1[i].GetTypeString());
                //Второе условие
                //Выводим номер лексемы и саму лексему
                if (param1 == 0 && param2 == 1)
                    lb.Items.Add((i + 1).ToString() + "\t" + m1[i].Get());
                //Третье условие
                //Выводим номер таблицы, в которой содержится лексема и ее номер в этой таблице
                if (param1 == 3 && param2 == 4)
                    lb.Items.Add(m1[i].TableNumber.ToString() + "\t" + (m1[i].ItemNumber+1).ToString());
            }
        }
    }
}