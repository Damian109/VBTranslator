using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dmcvb1.Lexic;

namespace dmcvb1.HardStatement
{
    public partial class HardForm : Form
    {
        public HardForm()
        {
            InitializeComponent();
        }

        public void Loader(TravellerTable tbl)
        {
            for (int i = 0; i < tbl.kT; i++)
            {
                listBox1.Items.Add(tbl.trvl[i].StackE);
                listBox2.Items.Add(tbl.trvl[i].StackT);
                listBox3.Items.Add(tbl.trvl[i].Home);
                listBox4.Items.Add(tbl.trvl[i].Function);
                listBox5.Items.Add(tbl.trvl[i].End);
            }  
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox5.TopIndex = listBox1.TopIndex;
            listBox2.TopIndex = listBox1.TopIndex;
            listBox3.TopIndex = listBox1.TopIndex;
            listBox4.TopIndex = listBox1.TopIndex;
            listBox5.SelectedIndex = listBox1.SelectedIndex;
            listBox2.SelectedIndex = listBox1.SelectedIndex;
            listBox3.SelectedIndex = listBox1.SelectedIndex;
            listBox4.SelectedIndex = listBox1.SelectedIndex;
        }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox5.TopIndex = listBox2.TopIndex;
            listBox1.TopIndex = listBox2.TopIndex;
            listBox3.TopIndex = listBox2.TopIndex;
            listBox4.TopIndex = listBox2.TopIndex;
            listBox5.SelectedIndex = listBox2.SelectedIndex;
            listBox1.SelectedIndex = listBox2.SelectedIndex;
            listBox3.SelectedIndex = listBox2.SelectedIndex;
            listBox4.SelectedIndex = listBox2.SelectedIndex;
        }
        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox5.TopIndex = listBox3.TopIndex;
            listBox2.TopIndex = listBox3.TopIndex;
            listBox1.TopIndex = listBox3.TopIndex;
            listBox4.TopIndex = listBox3.TopIndex;
            listBox5.SelectedIndex = listBox3.SelectedIndex;
            listBox2.SelectedIndex = listBox3.SelectedIndex;
            listBox1.SelectedIndex = listBox3.SelectedIndex;
            listBox4.SelectedIndex = listBox3.SelectedIndex;
        }
        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox5.TopIndex = listBox4.TopIndex;
            listBox2.TopIndex = listBox4.TopIndex;
            listBox3.TopIndex = listBox4.TopIndex;
            listBox1.TopIndex = listBox4.TopIndex;
            listBox5.SelectedIndex = listBox4.SelectedIndex;
            listBox2.SelectedIndex = listBox4.SelectedIndex;
            listBox1.SelectedIndex = listBox4.SelectedIndex;
            listBox3.SelectedIndex = listBox4.SelectedIndex;
        }
        private void listBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.TopIndex = listBox5.TopIndex;
            listBox2.TopIndex = listBox5.TopIndex;
            listBox3.TopIndex = listBox5.TopIndex;
            listBox4.TopIndex = listBox5.TopIndex;
            listBox1.SelectedIndex = listBox5.SelectedIndex;
            listBox2.SelectedIndex = listBox5.SelectedIndex;
            listBox3.SelectedIndex = listBox5.SelectedIndex;
            listBox4.SelectedIndex = listBox5.SelectedIndex;
        }
    }
}
