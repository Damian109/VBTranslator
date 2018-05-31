using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using dmcvb1.Classes;
using dmcvb1.Lexic;

namespace dmcvb1.HardStatement
{
    public class BauerZamelson
    {
        //Массив разборов сложных выражений на случай, если в исходном коде их присутствует больше одного
        public HardAnalise[] ha;
        //Переменная-счетчик разборов
        Byte cHardAnalise;
        //Форма, для вывода списка выражений
        HardForms hfs;
        //Формы для вывода выражений(разобранных)
        HardForm [] hf;
        //
        public BauerZamelson(Byte b)
        {
            //Массив разборов определяем по количеству, взятому из синтаксического разбора
            ha = new HardAnalise[b];
            for (int i = 0; i < b; i++)
                ha[i] = new HardAnalise();
            cHardAnalise = 0;
            hf = new HardForm[b];
            for(int i = 0; i < b;i++)
                hf[i] = new HardForm();
            hfs = new HardForms();
        }
        //
        public BauerZamelson()
        {
            cHardAnalise = 0;
            hfs = new HardForms();
        }

        //Установить количество сложных разборов
        public void Set(Byte b)
        {
            //Массив разборов определяем по количеству, взятому из синтаксического разбора
            ha = new HardAnalise[b];
            for (int i = 0; i < b; i++)
                ha[i] = new HardAnalise();
            cHardAnalise = 0;
            hf = new HardForm[b];
            for(int i = 0; i < b;i++)
                hf[i] = new HardForm();
        }

        //Подача сложных выражений с помощью таблицы стандартных символов и конкретного символа -начала разбора
        public void SetStatement(Table tbl, int cT)
        {
            ha[cHardAnalise].HardSet(tbl, cT);
            ha[cHardAnalise].HAnalise();
            hf[cHardAnalise].Loader(ha[cHardAnalise].trTbl);
            hfs.Set(ha[cHardAnalise].stt.GetS());
            cHardAnalise++;
        }

        //Вывод формы
        public void FormDraw()
        {
            hfs.ShowDialog();
            if(hfs.Param != 0)
                hf[hfs.Param-1].ShowDialog();
        }

        //Вывод ошибок
        public void errors(ref RichTextBox rtb)
        {
            String err = "";
            for (int i = 0; i < cHardAnalise; i++)
                err += ha[i].Errorss();
            if (err != "")
                rtb.Text += err;
        }

    }
}
