using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dmcvb1.Lexic;
using dmcvb1.Syntax;
using dmcvb1.HardStatement;
using dmcvb1.Classes;

namespace dmcvb1
{
    //Единственный класс, который можно использовать из приложения
    public class Analise
    {
        //Лексический анализ
        Lexic.Lexic lexic;
        //Синтаксический анализ
        Syntax.Syntax syntax;
        //Разбор сложных выражений
        BauerZamelson bz;

        public void Run(Text txt, ref RichTextBox rtb)
        {
            lexic = new Lexic.Lexic();
            syntax = new Syntax.Syntax();
            bz = new BauerZamelson();
            rtb.Text = "";
            int k = lexic.LexicF(txt);
            if (k != 0)
            {
                Errors err = new Errors();
                rtb.Text += err.FindErrors((byte)k, lexic.nString);
                return;
            }

           syntax.Set(lexic.ReturnTable());
           syntax.GetErrors(ref rtb);

           int[] mas = new int[10];
           int kmas = 0;
           mas = syntax.returnMas(ref kmas);

           bz.Set((byte)kmas);
           for(int i = 0; i < kmas; i++)
               bz.SetStatement(lexic.ReturnTable(), mas[i]-1);
           bz.errors(ref rtb);

            if (rtb.Text == "")
                rtb.Text = "Анализ прошел успешно\n";
        }

        public void LexicTables()
        {
            if (lexic == null)
                return;
            lexic.LexicTables();
        }

        public void BZ_tables()
        {
            if (bz == null)
                return;
            bz.FormDraw();
        }
    }
}
