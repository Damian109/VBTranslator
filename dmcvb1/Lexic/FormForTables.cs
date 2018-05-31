using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dmcvb1.Classes;

namespace dmcvb1.Lexic
{
    //Форма для вывода таблиц лексического анализа
    //Содержит 6 таблиц:
    //Лексемы
    //Терминалы
    //Разделители
    //Литералы
    //Идентификаторы
    //Таблицу стандартных символов
    public partial class FormForTables : Form
    {
        public FormForTables()
        {
            InitializeComponent();
        }
        //Функция, предназначенная для заполнения таблиц данными. Принимает 6 аргументов, как классы таблицы
        public void FillTables(Table t1, Table t2, Table t3, Table t4, Table t5, Table t6)
        {
            label1.Text = "Таблица лексем";
            label2.Text = "Таблица терминалов";
            label3.Text = "Таблица разделителей";
            label4.Text = "Таблица литералов";
            label5.Text = "Таблица идентификаторов";
            label6.Text = "Таблица стандартных символов";

            //
            //Заполним списки на экране значениями полученных лексем
            t1.FillList(ref listBox1, 1, 2);
            t2.FillList(ref listBox2, 0, 1);
            t3.FillList(ref listBox3, 0, 1);
            t4.FillList(ref listBox4, 0, 1);
            t5.FillList(ref listBox5, 0, 1);
            t6.FillList(ref listBox6, 3, 4); 
        }
    }
}
