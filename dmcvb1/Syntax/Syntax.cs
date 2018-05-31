using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dmcvb1.Classes;
using System.Windows.Forms;
using dmcvb1.Lexic;

namespace dmcvb1.Syntax
{
    public partial class Syntax
    {
        /*Создаем таблицу стандартных символов*/
        //Хранить ее будем в массиве лексем
        private Lexem[] TSS;
        //Символ в лексеме для считывания
        private int TSSn;
        //Эту лексему будем передавать между функциями
        Lexem L = new Lexem();
        //Номер строки
        public int Strn = 1;
        //*
        //Контроль ошибок
        private Errors er = new Errors();
        private Text err = new Text();
        //Массив переменных для разбора сложных выражений
        private int[] mas = new int[10];
        private int masN = 0;

        //Заносим таблицу стандартных символов на анализ
        public void Set(Table t)
        {
            //Добавляем символ конца разбора в конец массива лексем
            Lexem s = new Lexem();
            s.Set("$");
            t.SetTableString(s);
            //Заполняем массив значениями из таблицы
            TSS = new Lexem[t.GetCountLexem()];
            TSS = t.GetTableString();
            TSSn = 0;
            //Запускаем анализ
            S();
        }

        //Получаем ошибки
        public void GetErrors(ref RichTextBox rtb)
        {
            if(err.Get() != "")
                rtb.Text += err.Get();
        }

        //Возврат массива для разбора сложных выражений
        public int[] returnMas(ref int t)
        {
            t = masN;
            return mas;
        }

        //Функции для выполнения анализа
        //
        //Считываем следующую лексему
        private Lexem Next()
        {
            TSSn++;
            return TSS[TSSn-1];
        }

        //Точка входа в разбор
        private int S()
        {
            L = Next();
            while (L.Get() == "#")
            {
                Strn++;
                L = Next();
            }
            //Лексемы, с которых может начинаться выражение
            if (L.Get() == "dim" || L.Get() == "select" || L.TableNumber == 4)
                //Передаем лексему в программу для продолжения разбора. При удаче, завершится на $
                if (program() == 1)
                    return 1;
                else
                    return 0;
            //Конец разбора
            if(L.Get() == "$")
                return 0;
            //Ошибка
            //Проверяемая строка не подходит под грамматику языка
            err.AddText(er.FindErrors(5, Strn));
            return 1;
        }

        //
        private int program()
        {
            //Пока конец таблицы не достигнут
            while(L.Get() != "$")
            {
                //Если в выражении ошибка
                if(statement() == 1)
                    return 1;
                //Если выражение не заканчивается символом конца строки
                if (L.Get() != "#")
                {
                    //Ошибка. Отсутствует знак конца строки в строке №
                    err.AddText(er.FindErrors(6, Strn));
                    return 1;
                }
                while (L.Get() == "#")
                {
                    Strn++;
                    L = Next();
                }
            }
            return 0;
        }

        //
        private int statement()
        {
            if (L.Get() == "dim")
                if (definition() == 1)
                    return 1;
                else
                    return 0;
            if(L.Get() == "select" || L.TableNumber == 4)
                if (action() == 1)
                    return 1;
                else
                    return 0;
            //7
            err.AddText(er.FindErrors(7, Strn));
            return 1;
        }

        //
        private int action()
        {
            if (L.Get() == "select")
                if (actionSelect() == 1)
                    return 1;
                else
                    return 0;
            else
                if (actionAssignment() == 1)
                    return 1;
                else
                    return 0;
        }
    }
}
