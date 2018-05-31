using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dmcvb1.Classes;

namespace dmcvb1.Lexic
{
    //получить из класса можно только таблицу стандартных символов
    public class Lexic
    {
        /*Создаем лексическую таблицу*/
        private Table lexemtable = new Table();
        /*Создаем таблицу терминалов*/
        private Table terminaltable = new Table();
        /*Создаем таблицу разделителей*/
        private Table delimitertable = new Table();
        /*Создаем таблицу литералов*/
        private Table literaltable = new Table();
        /*Создаем таблицу идентификаторов*/
        private Table identificatortable = new Table();
        //**
        public int nString = 0;
        //*
        /*Создаем таблицу стандартных символов*/
        private Table lexemfinaltable = new Table();


        //Выполнить лексический анализ
        public int LexicF(Text txt)
        {
            LexicAnalise la = new LexicAnalise();
            //Преобразуем текст в единую строку с разделителями без учета форматирования
            txt.ConvertEndOfStrings();
            //Заполним лексическую таблицу по результатам лексического анализа
            //если нет ошибок
            int er = la.Run(txt);
            if (er != 0)
            {
                nString = la.tmpNString;
                return er;
            }
            lexemtable = la.tret;
            //Заполним остальные таблицы, используя результат(предыдущую таблицу)
            FillTables(lexemtable, terminaltable, delimitertable, identificatortable, literaltable, lexemfinaltable);
            return 0;
        }

        //Вывести таблицы на экран
        public void LexicTables()
        {
            //Создание формы для таблиц
            FormForTables frm = new FormForTables();
            //Заполнение таблиц
            frm.FillTables(lexemtable, terminaltable, delimitertable, literaltable, identificatortable, lexemfinaltable);
            //Показать форму
            frm.ShowDialog();
        }

        public Table ReturnTable()
        {
            return lexemfinaltable;
        }

        //Функция заполнения таблиц ключевых слов и разделителей
        private void FillTerminalsAndDelimiters(Table tTer, Table tDel)
        {
            //Создаем таблицу ключевых слов
            //Количество
            Byte NT = 10;
            //Значения
            String[] strT = { "dim", "as", "select", "case", "end", "integer", "to", "else", "double", "decimal" };
            //Переписываем в лексемы
            Lexem[] Ter = new Lexem[NT];
            for (int i = 0; i < NT; i++)
            {
                Ter[i] = new Identifier();
                Ter[i].Set(strT[i]);
            }
            //Добавляем в таблицу
            tTer.SetTable(Ter);

            //Создаем таблицу разделителей
            //Количество
            Byte ND = 9;
            //Значения
            String[] strD = { "=", "(", ")", "*", "/", "-", "+", "#", "," };
            //Переписываем в лексемы
            Lexem[] Del = new Lexem[ND];
            for (int i = 0; i < ND; i++)
            {
                Del[i] = new Delimiter();
                Del[i].Set(strD[i]);
            }
            //Добавляем в таблицу
            tDel.SetTable(Del);
        }

        //Функция для занесения элемента в одну из таблиц, а так же в таблицу стандартных символов
        private void Fill(Lexem a, Table tbl, Table final)
        {
            //Переменная состояния
            Byte B = 0;
            //Временный стандартный символ
            StandartSymbol le = new StandartSymbol();
            //Извлекаем значения из таблицы лексем
            Lexem[] temp = new Lexem[tbl.GetCountLexem()];
            temp = tbl.GetTableString();
            //Запускаем цикл
            for (int i = 0; i < temp.Length; i++)
                //Если лексема совпадает с табличной
                if (a.Get() == temp[i].Get())
                {
                    //меняем состояние
                    B = 1;
                    //Смотрим на тип лексемы
                    if (a.GetTypeString() == "Identifier")
                        le.TableNumber = 4;
                    if (a.GetTypeString() == "Literal")
                        le.TableNumber = 3;
                    //Запоминаем номер лексемы в таблице
                    le.ItemNumber = i;
                }
            //Если состояние не было изменено
            //Добавляем лексему к таблице
            //И вызываем функцию еще раз
            if (B == 0)
            {
                tbl.SetTableString(a);
                Fill(a, tbl, final);
            }
            //Если состояние все же изменилось
            //Добавляем лексему к финальной таблице
            else
            {
                le.Set(a.Get());
                final.SetTableString(le);
            }
        }

        //Заполняем таблицы
        public void FillTables(Table tlex, Table tTer, Table tDel,
            Table id, Table tLit, Table tFinal)
        {
            //Заполним таблицы терминалов и разделителей
            FillTerminalsAndDelimiters(tTer, tDel);
            //Значения основной таблицы запишем как массив лексем
            Lexem[] temp = new Lexem[tlex.GetCountLexem()];
            temp = tlex.GetTableString();
            //Запускаем цикл распределения лексем по таблицам и занесения в финальную
            for (int i = 0; i < temp.Length; i++)
            {
                //Работаем с разделителями
                if (temp[i].GetTypeString() == "Delimiter")
                {
                    //Вскрываем таблицу разделителей
                    Lexem[] temp2 = new Lexem[tDel.GetCountLexem()];
                    temp2 = tDel.GetTableString();
                    //Проверяем лексему на соответствие им
                    for (int j = 0; j < temp2.Length; j++)
                    {
                        if (temp[i].Get() == temp2[j].Get())
                        {
                            StandartSymbol le = new StandartSymbol();
                            le.TableNumber = 2;
                            le.ItemNumber = j;
                            le.Set(temp[i].Get());
                            tFinal.SetTableString(le);
                        }
                    }
                }

                if (temp[i].GetTypeString() == "Identifier")
                {
                    Byte B = 0;
                    //Вскрываем таблицу терминалов
                    Lexem[] temp2 = new Lexem[tTer.GetCountLexem()];
                    temp2 = tTer.GetTableString();
                    //Проверяем лексему на соответствие им
                    for (int j = 0; j < temp2.Length; j++)
                    {
                        if (temp[i].Get() == temp2[j].Get())
                        {
                            StandartSymbol le = new StandartSymbol();
                            le.TableNumber = 1;
                            le.ItemNumber = j;
                            le.Set(temp[i].Get());
                            tFinal.SetTableString(le);
                            B = 1;
                            break;
                        }
                    }
                    //Если в таблице терминалов соответствие не найдено, переходим к заполнению финальной таблицы
                    //обычными идентификаторами
                    if (B == 0)
                        Fill(temp[i], id, tFinal);
                }

                if (temp[i].GetTypeString() == "Literal")
                {
                    Fill(temp[i], tLit, tFinal);
                }
            }
        }
    }
}