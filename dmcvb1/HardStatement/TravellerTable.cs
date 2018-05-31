using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmcvb1.HardStatement
{
    //Таблица строк разбора
    public class TravellerTable
    {
        //Массив строк
        public Traveller[] trvl;
        //номер последнего элемента
        public int kT;

        //
        public TravellerTable()
        {
            //создаем массив строк как таблицу
            trvl = new Traveller[400];
            for (int i = 0; i < 400; i++)
                trvl[i] = new Traveller();
            kT = 0;
        }

        //Запись в таблицу строки 
        public void StringSet(String s1, String s2, String s3, String s4, String s5)
        {
            trvl[kT].StackE = s1;
            trvl[kT].StackT = s2;
            trvl[kT].Home = s3;
            trvl[kT].Function = s4;
            trvl[kT].End = s5;
            kT++;
        }
    }
}
