using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmcvb1.HardStatement
{
    public class Traveller
    {
        //Элементы из стека Е
        public String StackE;
        //Элементы из стека Т
        public String StackT;
        //Вход
        public String Home;
        //Действие
        public String Function;
        //Выход
        public String End;

        public Traveller()
        {
            StackE = "";
            StackT = "";
            Home = "";
            Function = "";
            End = "";
        }
    }
}
