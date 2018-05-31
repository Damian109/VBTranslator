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
        private int definition()
        {
            L = Next();
            if (listIdentificator() == 1)
                return 1;
            L = Next();
            if (type() == 1)
                return 1;
            else
            {
                L = Next();
                return 0;
            }
        }

        //
        private int listIdentificator()
        {
            while(L.Get() != "as")
            {
                if(L.TableNumber == 4)
                {
                    L = Next();
                    if(L.Get() != "," && L.Get() != "as")
                    {
                        //8
                        err.AddText(er.FindErrors(8, Strn));
                        return 1;
                    }
                }
            }
            return 0;
        }

        //
        private int type()
        {
            if (L.Get() == "integer" || L.Get() == "double" || L.Get() == "decimal")
                return 0;
            else
            {
                //Ошибка
                //Задан неизвестный тип переменной
                err.AddText(er.FindErrors(9, Strn));
                return 1;
            }
        }
    }
}
