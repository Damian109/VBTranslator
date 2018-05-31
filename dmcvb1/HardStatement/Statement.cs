using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dmcvb1.Classes;
using dmcvb1.Lexic;

namespace dmcvb1.HardStatement
{
    public class Statement
    {
        //Список лексем для разбора
        private Lexem[] lexems;
        //Номер последней лексемы в массиве
        private int mLexem;
        //Номер лексемы
        private int kLexem;
        //
        public Statement()
        {
            lexems = new Lexem[200];
            kLexem = 0;
        }

        //Установка лексем для разбора
        public void Set(Table t, int num)
        {
            mLexem = 0;
            //переводим таблицу в массив лексем
            Lexem[] lx = new Lexem[t.GetCountLexem()];
            lx = t.GetTableString();
            int n = num;
            //Выполняем разбор, пока не встретим завершающий символ
            while (lx[n].Get() != "#")
            {
                lexems[mLexem++] = lx[n++];
            }
            //Записываем завершающий символ
            lexems[mLexem] = lx[n];
        }
            
        //Изъятие одной из лексем
        public Lexem Get()
        {
            if (!isAll())
            {
                kLexem++;
                return lexems[kLexem - 1];
            }
            return null;
        }

        //Проверка на конец массива лексем
        public bool isAll()
        {
            if (kLexem > mLexem)
                return true;
            return false;
        }

        //Получить Строку-выражение
        public String GetS()
        {
            String s = "";
            for (int i = 0; i < mLexem; i++)
                s += lexems[i].Get();
            return s;
        }
    }
}
