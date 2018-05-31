using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dmcvb1.HardStatement
{
    public partial class HardForms : Form
    {
        //Массив выражений
        String[] stts;
        //Счетчик
        Byte cstts;
        public int Param = 0;
        //
        public HardForms()
        {
            InitializeComponent();
            stts = new String[100];
            cstts = 0;
        }

        //Добавление строки
        public void Set(String s)
        {
            stts[cstts++] = s;
            listBox1.Items.Add(s);
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Param = listBox1.SelectedIndex + 1;
            this.Close();
        }
        
    }
}
