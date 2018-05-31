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
        private int actionAssignment()
        {
            L = Next();
            if (L.Get() != "=")
            {
                //Ошибка в конструкции оператора присваивания
                err.AddText(er.FindErrors(10, Strn));
                return 1;
            }
            L = Next();
            if (complexOrLiteral() == 1)
                return 1;
            return 0;
        }

        //
        private int complexOrLiteral()
        {
            if (L.TableNumber != 3 && L.TableNumber != 4)
            {
                //Выражение справа не является идентификатором, литералом или сложным выражением в строке 
                err.AddText(er.FindErrors(11, Strn));
                return 1;
            }
            L = Next();
            if (L.Get() == "#")
                return 0;
            else
            {
                mas[masN++] = TSSn - 1;
                while (L.Get() != "#")
                {
                    L = Next();
                    if (L.TableNumber == 1)
                        //Конструкция присваивания не должна содержать ключевых слов
                        err.AddText(er.FindErrors(12, Strn));
                }
                return 0;
            }
        }
    }
}
