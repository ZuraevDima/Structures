using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Structures
{
    public struct AEROFLOT
    {
        private string destination;
        private int flightNumber;
        private string plainType;
        
        public string GetDestination
        {
            get { return destination; }
            private set { destination = value; }
        }
        public int GetFlightNumber
        {
            get { return flightNumber; }
            private set { flightNumber = value; }
        }
        public string GetPlainType
        {
            get { return plainType; }
            private set { plainType = value; }
        }
        public AEROFLOT(string destination, int flightNumber, string plainType)
        {
            this.destination = destination;
            this.flightNumber = flightNumber;
            this.plainType = plainType;
        }
        public void PrintData(bool isFull = false)
        {
            if (isFull)
            {
                Console.WriteLine($"Название пункта назначения рейса: {GetDestination}, Номер рейса: {flightNumber}, Тип самолёта: {plainType}");
            }
            else
            {
                Console.WriteLine($"Номер рейса: {flightNumber}, Тип самолёта: {plainType}");
            }
        }
        static public void SortbyFlightNumber(AEROFLOT[] plains)
        {
            for (int i = 0; i < plains.Length - 1; i++)
            {
                var indexMin = i;
                for (int j = i + 1; j < plains.Length; j++)
                {
                    if (plains[j].GetFlightNumber < plains[indexMin].GetFlightNumber)
                    {
                        indexMin = j;
                    }
                }
                AEROFLOT tempI = new AEROFLOT();
                tempI = plains[i];

                plains[i] = plains[indexMin];

                plains[indexMin] = tempI;

                //var temporary = plains[i].GetFlightNumber;
                //plains[i].GetFlightNumber = plains[indexMin].GetFlightNumber;
                //plains[indexMin].GetFlightNumber = temporary;
            }
        }
    }
   
    internal class Program
    {
        static public bool FindPlain(AEROFLOT[] plains)
        {
            Console.Write("\n\nВведите пункт назначения для поиска: ");
            string point = Console.ReadLine();
            bool IsExist = false;
            for (int i = 0; i < plains.Length; i++)
            {
                if (plains[i].GetDestination == point)
                {
                    plains[i].PrintData();
                    IsExist = true;
                }
            }
            return IsExist;
        }
        static void Main(string[] args)
        {
            try
            {
                //ввод данных
                AEROFLOT[] plains = new AEROFLOT[4]; 
                for (int i = 0; i < plains.Length; i++)
                {
                    Console.Write($"Введите название пункта назначения {i + 1} рейса: ");
                    string destination = Console.ReadLine();
                    bool isValidDestination = false;
                    while (!isValidDestination)
                    {
                        foreach (var c in destination)
                        {
                            if (!char.IsLetter(c) & c != '-')
                            {
                                Console.WriteLine("Пункт назначения должен состоять только из букв или знака тирэ");
                                Console.Write($"Введите название пункта назначения {i + 1} рейса: ");
                                destination = Console.ReadLine();
                            }
                            else
                            {
                                isValidDestination = true;
                            }
                        }
                    }
                    Console.Write("Введите номер рейса: ");
                    int flightNumber = Convert.ToInt32(Console.ReadLine());
                    while (flightNumber < 0)
                    {
                        Console.WriteLine("Номер рейса не может быть отрицательным");
                        Console.Write("Введите номер рейса: ");
                        flightNumber = Convert.ToInt32(Console.ReadLine());
                    }
                    Console.Write("Введите тип самолёта: ");
                    string plainType = Console.ReadLine();

                    plains[i] = new AEROFLOT(destination, flightNumber, plainType);
                    Console.WriteLine();
                }
                AEROFLOT.SortbyFlightNumber(plains);
                //вывод всех самолётов 
                foreach (var plain in plains)
                {
                    plain.PrintData(true);
                }

                bool IsExist = FindPlain(plains);
                if (!IsExist)
                {
                    while (!IsExist)
                    {
                        Console.Write("Извините, таких рейсов нет, хотите найти другой рейс? (y/n): ");
                        string answer = Console.ReadLine().ToLower();
                        if (answer == "y")
                        {
                            IsExist = FindPlain(plains);
                        }
                        else if (answer == "n")
                        {
                            Console.WriteLine("До свидания!");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("y - Да, n - Нет");
                        }
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
