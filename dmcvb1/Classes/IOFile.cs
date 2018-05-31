using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace dmcvb1.Classes
{
    /*Класс для работы с файлами*/
    public class IOFile
    {
        /*Строка для хранения имени файла*/
        String NameOfFile;
        /*Определение конструктора*/
        public IOFile()
        {
            NameOfFile = "";
        }

        //Внесение имени
        public void SetName(String name)
        {
            NameOfFile = name;
        }

        //Получение имени
        public String GetName()
        {
            return NameOfFile;
        }

        /*Открываем файл для чтения*/
        public String ReadFile()
        {
            /*Открываем поток с указанием имени файла*/
            StreamReader tmpReader = new StreamReader(File.OpenRead(NameOfFile));
            /*Временная строка для возврата*/
            String tmpString = "";
            /*Переменная для хранения текущего считываемого символа*/
            char tmpChar;
            /*Считываем символы из потока до конца*/
            while (!tmpReader.EndOfStream)
            {
                tmpChar = (char)tmpReader.Read();
                if (tmpChar != '\r')
                    tmpString += tmpChar;
            }
            tmpReader.Close();
            return tmpString;
        }

        /*Открываем файл для записи*/
        public void WriteFile(Text text)
        {
            /*Открываем поток с указанием имени файла*/
            StreamWriter tmpWriter = new StreamWriter(File.OpenWrite(NameOfFile));
            /*Временная строка для записи*/
            String tmpString = text.Get();
            //Записываем текст в файл
            tmpWriter.WriteLine(tmpString);
            //
            tmpWriter.Close();
        }
    }
}

