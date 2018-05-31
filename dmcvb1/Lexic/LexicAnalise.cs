using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dmcvb1.Classes;

namespace dmcvb1.Lexic
{
    /*Класс - Лексический анализатор*/
    public partial class LexicAnalise
    {
        //Текст для работы
        Text txt = new Text();
        //Считаемый символ
        int txtc = 0;
        //Переменная, отвечающая за номер текущей проверяемой строки
        public int tmpNString = 1;
        //Таблица, для возврата
        public Table tret = new Table();
        //Временная лексема в виде строки
        private String Lex = "";
        //*
        //Замена всех букв на строчные
        //*
        private void Atoa()
        {
            //Строка с буквами заглавными     
            const String LexemA1 = "QWERTYUIOPASDFGHJKLZXCVBNM";
            //Строка с буквами строчными
            const String LexemA2 = "qwertyuiopasdfghjklzxcvbnm";
            //Новый текст - туда сохраняются символы
            Text x = new Text();
            //
            for (int i = 0; i < txt.Get().Length; i++)
            {
                //Временная переменная типа Char для хранения обрабатываемого значения
                Char tmpChar = txt.Get()[i];
                //Сравниваем выбранную переменную с нашими списками лексем
                //Если буква заглавная - меняем на строчную
                for (int j = 0; j < LexemA1.Length; j++)
                {
                    if (tmpChar == LexemA1[j])
                        tmpChar = LexemA2[j];
                }
                String g = "";
                g += tmpChar;
                x.AddText(g);
            }
            txt.ClearText();
            txt.AddText(x.Get());
        }
        //**
        //Функция - Определение типа символа
        //**
        private Byte TypeSymbol(Char ch)
        {
            const String LexemA = "qwertyuiopasdfghjklzxcvbnm";
            //Строка с разделителями
            const String LexemB = "+-*/=#,()'";
            //Строка с цифрами
            const String LexemC = "1234567890";
            //Если строчная - тип = 1
            for (int j = 0; j < LexemA.Length; j++)
                if (ch == LexemA[j])
                    return 1;
            //Если разделитель - тип = 2
            for (int j = 0; j < LexemB.Length; j++)
                if (ch == LexemB[j])
                    return 2;
            //Если литерал - тип = 3
            for (int j = 0; j < LexemC.Length; j++)
                if (ch == LexemC[j])
                    return 3;
            if (ch == ' ')
                return 4;
            return 0;
        }
        //**
        //Чтение очередного символа строки
        private Char Next()
        {
            //Если символы закончились
            //Заканчиваем чтение
            if (txt.Get().Length <= txtc)
                return '$';
            //Символ из текста
            Char ch = txt.Get()[txtc];
            //Увеличиваем счетчик 
            txtc++;
            return ch;
        }
        //Добавления символа к временной лексеме
        private void Add(Char c)
        {
            Lex += c;
            return;
        }
        //Создание лексемы
        private void Out(int t)
        {
            if (t == 1)
            {
                //Идентификатор
                Identifier I = new Identifier();
                I.Set(Lex);
                tret.SetTableString(I);
            }
            if (t == 3)
            {
                //Литерал
                Literal L = new Literal();
                L.Set(Lex);
                tret.SetTableString(L);
            }
            if (t == 2)
            {
                //Разделитель
                Delimiter D = new Delimiter();
                D.Set(Lex);
                tret.SetTableString(D);
            }
        }
        //Обнуление временной лексемы
        private void Clear()
        {
            Lex = "";
        }
        //Начало чтения. Запуск цикла до конца текста
        public int Run(Text txt_)
        {
            txt = txt_;
            Atoa();
            Char t = Next();
            //Пока не будет достигнут конец текста
            while (t != '\0')
            {
                int k = TypeSymbol(t);
                switch (k)
                {
                    //Идентификатор
                    case 1:
                        {
                            Add(t);
                            t = Next();
                            while (TypeSymbol(t) == 1 || TypeSymbol(t) == 3)
                            {
                                Add(t);
                                t = Next();
                            }
                            //Ограничение длины
                            if (Lex.Length > 8)
                                return 2;
                            Out(1);
                            Clear();
                            break;
                        }
                    //Разделитель
                    case 2:
                        {
                            //Комментарии
                            if (t == '\'')
                                while (t != '#')
                                    t = Next();
                            //Переход на новую строку
                            if (t == '#')
                                tmpNString++;
                            Add(t);
                            t = Next();
                            Out(2);
                            Clear();
                            break;
                        }
                    //Литерал
                    case 3:
                        {
                            Add(t);
                            t = Next();
                            while (TypeSymbol(t) == 3)
                            {
                                Add(t);
                                t = Next();
                            }
                            //буква идет после числа
                            if (TypeSymbol(t) == 1)
                                return 3;
                            Out(3);
                            Clear();
                            break;
                        }
                    //Пробел
                    case 4:
                        t = Next();
                        break;
                    //Неизвестный символ
                    default:
                        return 1;
                }
            }
            return 0;
        }
    }
}
    