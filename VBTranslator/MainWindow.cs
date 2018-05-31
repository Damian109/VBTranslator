using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dmcvb1;
using dmcvb1.Classes;
using dmcvb1.Lexic;
using dmcvb1.HardStatement;
using dmcvb1.Syntax;

namespace VBTranslator
{
    public partial class MainWindow : Form
    {
        //Объект для хранения текущего файла
        IOFile file = new IOFile();

        Analise a;
        //
        public MainWindow()
        {
            InitializeComponent();
        }
        //Меню - новый.
        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Временная строка
            Text txt = new Text();
            //Если имя файла уже есть
            if(file.GetName() != "")
                //Открываем текущий файл
                txt.Set(file.ReadFile());
            //Проверяем был ли изменен текущий файл
            if (richTextBox1.Text + "\n" != txt.Get() && richTextBox1.Text != txt.Get())
            {
                //Если изменён
                //Спрашиваем сохранить или нет
                DialogResult result = MessageBox.Show("Сохранить?", "Создание нового файла", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                //Если требуется сохранить файл
                if (result == DialogResult.OK)
                {
                    //Сохраняем
                    txt.Set(richTextBox1.Text);
                    file.WriteFile(txt);
                }
            }
            //Выводим форму для создания файла
            NewFile nf = new NewFile();
            nf.ShowDialog();
            //Получаем от нее имя нового файла
            file.SetName("Files/" + nf.name + ".vb");
            txt.Set("");
            //Создаем его
            file.WriteFile(txt);
            //очищаем текстовое поле
            richTextBox1.Text = txt.Get();
        }
        //*
        //Меню - открыть
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Временная строка
            Text txt = new Text();
            //Если имя файла уже есть
            if (file.GetName() != "")
            {
                //Открываем текущий файл
                txt.Set(file.ReadFile());
                //Проверяем был ли изменен текущий файл
                if (richTextBox1.Text + "\n" != txt.Get() && richTextBox1.Text != txt.Get())
                {
                    //Если изменён
                    //Спрашиваем сохранить или нет
                    DialogResult result = MessageBox.Show("Сохранить?", "Открыть", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    //Если требуется сохранить файл
                    if (result == DialogResult.OK)
                    {
                        //Сохраняем
                        txt.Set(richTextBox1.Text);
                        file.WriteFile(txt);
                    }
                }
            }
            //Диалоговое окно для открытия
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "Файл vb";
            fd.Filter = "vb files (*.vb)|*.vb|All files (*.vb)|*.vb";
            //Папка по умолчанию
            fd.InitialDirectory = System.IO.Path.Combine(Application.StartupPath, @"Files");
            //Сохраняем имя файла и открываем его
            if (fd.ShowDialog() == DialogResult.OK)
            {
                file.SetName(fd.FileName);
                richTextBox1.Text = file.ReadFile();
            }
        }
        //*
        //Меню - сохранить
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Создаем текстовую переменную
            Text txt = new Text();
            //Помещаем в нее текст из текстового поля
            txt.Set(richTextBox1.Text);
            //Записываем в файл
            file.WriteFile(txt);
        }
        //*
        //Меню - сохранить как...
        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Диалоговое окно для сохранения
            SaveFileDialog sd = new SaveFileDialog();
            sd.Title = "Сохранить как...";
            sd.Filter = "vb files (*.vb)|*.vb|All files (*.vb)|*.vb";
            //Папка по умолчанию
            sd.InitialDirectory = System.IO.Path.Combine(Application.StartupPath, @"Files");
            //Сохраняем имя файла и открываем его
            if (sd.ShowDialog() == DialogResult.OK)
            {
                file.SetName(sd.FileName);
                //Создаем текстовую переменную
                Text txt = new Text();
                //Помещаем в нее текст из текстового поля
                txt.Set(richTextBox1.Text);
                //Записываем в файл
                file.WriteFile(txt);
            }
        }
        //*
        //Меню - выход
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Временная строка
            Text txt = new Text();
            //Если имя файла уже есть
            if(file.GetName() != "")
                //Открываем текущий файл
                txt.Set(file.ReadFile());
            else
                //Закрываем окно
                this.Close();
            //Проверяем был ли изменен текущий файл
            if (richTextBox1.Text + "\n" != txt.Get() && richTextBox1.Text != txt.Get())
            {
                //Если изменён
                //Спрашиваем сохранить или нет
                DialogResult result = MessageBox.Show("Сохранить?", "Выход", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                //Если требуется сохранить файл
                if (result == DialogResult.OK)
                {
                    //Сохраняем
                    txt.Set(richTextBox1.Text);
                    file.WriteFile(txt);
                    //Закрываем окно
                    this.Close();
                }
                else
                    //Закрываем окно
                    this.Close();
            }
            else
                //Закрываем окно
                this.Close();
        }
        //*
        //***
        //Меню - Выполнить анализ
        private void лексическийАнализToolStripMenuItem_Click(object sender, EventArgs e)
        {
            a = new Analise();
            Text t = new Text();
            t.Set(richTextBox1.Text);
            a.Run(t, ref richTextBox2);
        }
        //*
        //Меню - вывести лексические таблицы
        private void вывестиТаблицыToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            a.LexicTables();
        }

        private void разборСложныхВыраженийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            a.BZ_tables();
        }

    }
}
