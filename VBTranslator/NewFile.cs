using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VBTranslator
{
    public partial class NewFile : Form
    {
        //Имя файла. Можем изъять позже
        public String name;
        //
        public NewFile()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Если ничего не введено
            if (textBox1.Text == "")
            {
                MessageBox.Show("Предупреждение", "Пустое поле", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }
            //Проверяем на наличие такого файла в директории
            if (System.IO.File.Exists("Files/" + textBox1.Text + ".vb"))
            {
                MessageBox.Show("Такой файл уже существует", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }
            name = textBox1.Text;
            this.Close();
        }
    }
}
