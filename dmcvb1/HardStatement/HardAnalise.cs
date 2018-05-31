using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dmcvb1.Classes;
using dmcvb1.Lexic;

namespace dmcvb1.HardStatement
{
    public class HardAnalise
    {
        //Таблица для возврата
        public TravellerTable trTbl;
        //Наличие ошибок
        private Errors err;
        String Errors = "";
        //Выражение
        public Statement stt;
        //Стеки для операндов и операторов
        private Stack T, E;
        //Переменная, предназначенная для обозачения конца разбора
        private bool End = false;
        //Для дополнительных переменных
        private Byte nk = 1;
        
        //
        public HardAnalise()
        {
            //Первоначальные определения
            trTbl = new TravellerTable();
            err = new Errors();
            stt = new Statement();
            T = new Stack();
            E = new Stack();
        }

        public void HardSet(Table tbl, int k)
        {
            //Устанавливаем сложное выражение
            stt.Set(tbl, k);
        }
        //Перегруженный конструктор
        public HardAnalise(Table tbl, int k)
        {
            //Первоначальные определения
            trTbl = new TravellerTable();
            err = new Errors();
            stt = new Statement();
            T = new Stack();
            E = new Stack();
            //Устанавливаем сложное выражение
            stt.Set(tbl, k);
        }

        public void HAnalise()
        {
            Analise(stt);
        }
        //Записать элемент в стек Е
        private void WriteE(String str)
        {
            E.Set(str);
        }
        //Записать элемент в стек Т
        private void WriteT(String str)
        {
            T.Set(str);
        }
        //Изъять элемент из стека Е
        private String GetE()
        {
            return E.Get();
        }
        //Изъять элемент из стека Т
        private String GetT()
        {
            return T.Get();
        }
        //Удалить элемент из стека Т
        private void DeleteT()
        {
            T.Get();
        }
        //Считать следующий элемент
        private Lexem ReadCh()
        {
            Lexem a = new Lexem();
            if (!stt.isAll())
                a = stt.Get();
            return a;
        }
        //**
        //***
        //Функция K(id)
        private Lexem Kid(String str)
        {
            //Записываем действие в таблицу действий
            trTbl.StringSet(E.StringStack(), T.StringStack(), str, "K(" + str + ")", "");
            //Добавим символ в стек Е
            E.Set(str);
            //Читаем следующий символ
            return ReadCh();
        }
        //Функция K(OP)
        private String KOP()
        {
            //Извлекаем 2 элемента из стека Е
            String s1 = GetE();
            String s2 = GetE();
            String S_ret = "";
            //Заполняем строку результата
            S_ret += "M" + nk.ToString() + "," + s2 + "," + s1 + "," + T.Get();
            //Помещаем в стек новую переменную
            WriteE("M" + nk.ToString());
            nk++;
            //Возвращаем строку
            return S_ret;
        }
        //**
        private Lexem D1(String str)
        {
            //Записываем строку в таблицу
            trTbl.StringSet(E.StringStack(), T.StringStack(), str, "D1", "");
            //Помещаем в стек Т
            T.Set(str);
            //Считаем следующий символ
            return ReadCh();
        }
        //**
        private Lexem D2(String str)
        {
            //Записываем строку в таблицу
            trTbl.StringSet(E.StringStack(), T.StringStack(), str, "D2,K(" + T.GetS() + ")",KOP());
            //Помещаем элемент в стек Т
            T.Set(str);
            //Считываем следующий символ
            return ReadCh();
        }
        //**
        private Lexem D3()
        {
            //Записываем строку в таблицу
            trTbl.StringSet(E.StringStack(), T.StringStack(), ")", "D3", "");
            //Удаляем элемент из стека Т
            DeleteT();
            //Считываем следующий символ
            return ReadCh();
        }
        //**
        private void D4(String str)
        {
            trTbl.StringSet(E.StringStack(), T.StringStack(), str, "D4,K(" + T.GetS() + ")", KOP());
            //DeleteT();
            return;
        }
        //**
        private void D5(String str)
        {
            //Записываем строку в таблицу
            trTbl.StringSet(E.StringStack(), T.StringStack(), str, "D5", "");
            //Конец разбора
            End = true;
            return;
        }
        //**
        private void D6(String str)
        {
            //Записываем строку в таблицу
            trTbl.StringSet(E.StringStack(), T.StringStack(), str, "D6", E.StringStack());
            //Конец разбора
            End = true;
            return;
        }

        //Функция разбора сложного выражения
        public void Analise(Statement str)
        {
            //Лексема для работы. В нее будем помещать каждый следующий символ из потока
            Lexem lex = new Lexem();
            lex = ReadCh();
            //Пока в выражении не закончатся элементы 
            while (!End)
            {
                //Если лексема - операнд, то   
                if (lex.TableNumber == 3 || lex.TableNumber == 4)
                {
                    lex = Kid(lex.Get());
                    continue;
                }
                else
                {
                    //Если вершина стека - скобка
                    if (T.GetS() == "(")
                    {
                        //Входной символ - завершающий
                        if (lex.Get() == "#")
                        {
                            //Выводим сообщение об ошибке
                            Errors = err.FindErrors(21, 0);
                            D5(lex.Get());
                            continue;
                        }
                        //Входной символ - (+-*/
                        if (lex.Get() == "(" || lex.Get() == "+" || lex.Get() == "-" || lex.Get() == "*" || lex.Get() == "/")
                        {
                            lex = D1(lex.Get());
                            continue;
                        }
                        //Входной символ )
                        if (lex.Get() == ")")
                        {
                            lex = D3();
                            continue;
                        }
                    }
                    //Если вершина стека +-
                    if (T.GetS() == "+" || T.GetS() == "-")
                    {
                        //Входной символ - #)
                        if (lex.Get() == "#" || lex.Get() == ")")
                        {
                            D4(lex.Get());
                            continue;
                        }
                        //Входной символ - (*/
                        if (lex.Get() == "(" || lex.Get() == "*" || lex.Get() == "/")
                        {
                            lex = D1(lex.Get());
                            continue;
                        }
                        ///////////////////////////////////////////////////////////////////////
                        //Входной символ - +-
                        if (lex.Get() == "+" || lex.Get() == "-")
                        {
                            lex = D2(lex.Get());
                            continue;
                        }
                    }
                    //Если вершина стека */
                    if (T.GetS() == "*" || T.GetS() == "/")
                    {
                        //Входной символ - #)
                        if (lex.Get() == "#" || lex.Get() == ")" || lex.Get() == "+" || lex.Get() == "-")
                        {
                            D4(lex.Get());
                            continue;
                        }
                        //Входной символ - (
                        if (lex.Get() == "(")
                        {
                            lex = D1(lex.Get());
                            continue;
                        }
                        //Входной символ - */
                        if (lex.Get() == "*" || lex.Get() == "/")
                        {
                            lex = D2(lex.Get());
                            continue;
                        }
                    }
                    //если вершина стека пуста
                    if (T.isEmpty())
                    {
                        //Входной символ - */+-(
                        if (lex.Get() == "(" || lex.Get() == "*" || lex.Get() == "/" || lex.Get() == "+" || lex.Get() == "-")
                        {
                            lex = D1(lex.Get());
                            continue;
                        }
                        //Входной символ - #
                        if (lex.Get() == "#")
                        {
                            D6(lex.Get());
                            return;
                        }
                        //Входной символ - )
                        if (lex.Get() == ")")
                        {
                            //Выводим сообщение об ошибке
                            Errors = err.FindErrors(22, 0);
                            D5(lex.Get());
                            return;
                        }
                    }
                }
            }
        }
        //Проверить наличие ошибок
        public String Errorss()
        {
            return Errors;
        }
    }
}
