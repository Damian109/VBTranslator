using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmcvb1.HardStatement
{
    //Класс стек. Необходим для разбора методом Бауэра-Замельзона
    public class Stack
    {
        //Массив переменных для хранения значений
        private String[] Mas;
        //Переменная отвечающая за номер элемента, являющегося вершиной
        private int kMas;
        //
        public Stack()
        {
            Mas = new String[100];
            kMas = 0;
        }
        //Установка значения на вершину
        public void Set(String ch)
        {
            kMas++;
            Mas[kMas] = ch; 
        }

        //Изъятие значения с вершины
        public String Get()
        {
            if (kMas > 0)
            {
                kMas--;
                return Mas[kMas + 1];
            }
            return "";
        }

        //Просмотр значения с вершины 
        public String GetS()
        {
            if (kMas > 0)
                return Mas[kMas];
            return "";
        }

        //Обнуление стека
        public void New()
        {
            kMas = 0;
        }

        //Получение количества элементов стека
        public int Count()
        {
            return kMas;
        }

        //Проверка пустой ли стек
        public bool isEmpty()
        {
            if (kMas <= 0)
                return true;
            return false;
        }

        //Вернуть стек в текстовом виде
        public String StringStack()
        {
            String S = "";
            //перебираем все элементы до вершины
            for (int i = 1; i < kMas; i++)
                S += Mas[i] + ",";
            //В конец строки записываем вершину стека
            if (kMas < 0)
                return "";
            S += Mas[kMas];
            //возвращаем
            return S;
        }
    }
}
