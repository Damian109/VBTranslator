using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dmcvb1.Lexic;
using dmcvb1.Classes;

namespace dmcvb1.Syntax
{
    public partial class Syntax
    {
        //
        private int actionSelect()
        {
            L = Next();
            if (L.Get() != "case")
            {
                //Ошибка
                //После Select отсутствует ключевое слово case
                err.AddText(er.FindErrors(13, Strn));
                return 1;
            }
            L = Next();
            if (L.TableNumber != 4)
            {
                //Ошибка
                //Оператор Select case задан без идентификатора
                err.AddText(er.FindErrors(14, Strn));
                return 1;
            }
            L = Next();
            if (L.Get() != "#")
            {
                //Ошибка
                //После Select case отсутствует символ конца строки
                err.AddText(er.FindErrors(15, Strn));
                return 1;
            }
            Strn++;
            L = Next();
            while (L.Get() != "end" || L.Get() != "$")
            {
                if (L.Get() == "case")
                {
                    L = Next();
                    if (L.Get() == "else")
                    {
                        if (elses() == 1)
                            return 1;
                        break;
                    }
                    else
                        if (cases() == 1)
                            return 1;
                }
                else
                {
                    if (L.Get() != "end" && L.Get() != "$")
                    {
                        //Ошибка
                        //Конструкция select case неверно завершена
                        err.AddText(er.FindErrors(16, Strn));
                        return 1;
                    }
                    break;
                }
            }
            L = Next();
            if (L.Get() != "select")
            {
                //Ошибка
                //Конструкция select case неверно завершена
                err.AddText(er.FindErrors(16, Strn));
                return 1;
            }
            L = Next();
            return 0;
        }
          
        //
        private int cases()
        {
            if (value() == 1)
                return 1;
            L = Next();
            if (caseTo() == 1)
                return 1;
            return 0;
        }

        //
        private int value()
        {
            if (L.TableNumber == 3 || L.TableNumber == 4)
                return 0;
            //Значение в конструкции case не является числом или идентификатором
            err.AddText(er.FindErrors(17, Strn));
            return 1;
        }

        //
        private int caseTo()
        {
            if (L.Get() == "#")
            {
                Strn++;
                L = Next();
                while (L.Get() != "case" || L.Get() != "end")
                {
                    if (L.Get() == "#")
                    {
                        Strn++;
                        L = Next();
                    }
                    if (L.Get() == "case" || L.Get() == "end")
                        break;
                    if (statement() == 1)
                        return 1;
                    Strn++;
                    L = Next();
                }
                return 0;
            }
            if (L.Get() == "to")
            {
                L = Next();
                if (value() == 1)
                    return 1;
                L = Next();
                if (L.Get() != "#")
                {
                    //Ошибка
                    //18 Отсутствует знак конца выражения после конструкции case
                    err.AddText(er.FindErrors(18, Strn));
                    return 1;
                }
                Strn++;
                while (L.Get() != "case" || L.Get() != "end")
                {
                    if (L.Get() == "#")
                    {
                        Strn++;
                        L = Next();
                    }
                    if (L.Get() == "case" || L.Get() == "end")
                        break;
                    if (statement() == 1)
                        return 1;
                    Strn++;
                    L = Next();
                }
                return 0;
            }
            //Ошибка 
            //Ошибка построения конструкции case
            err.AddText(er.FindErrors(19, Strn));
            return 1;
        }

        //
        private int elses()
        {
            L = Next();
            if (L.Get() == "#")
            {
                Strn++;
                L = Next();
                while (L.Get() != "end")
                {
                    if (L.Get() == "#")
                    {
                        Strn++;
                        L = Next();
                    }
                    if (L.Get() == "case")
                    {
                        //Ошибка 
                        //присутствуют конструкции case после case else
                        err.AddText(er.FindErrors(20, Strn));
                        return 1;
                    }
                    if (L.Get() == "end")
                        break;
                    if (statement() == 1)
                        return 1;
                    Strn++;
                    L = Next();
                }
                return 0;
            }
            else
            {
                //Ошибка 
                //Ошибка построения конструкции case
                err.AddText(er.FindErrors(19, Strn));
                return 1;
            }
        }      
    }
}
