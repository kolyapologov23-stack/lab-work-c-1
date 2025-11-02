using System;
using System.Diagnostics;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

class labOc
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Отгадай ответ");
            Console.WriteLine("2. Об авторе");
            Console.WriteLine("3. Сортировка массива");
            Console.WriteLine("4. Выход");
            Console.Write("Введите номер пункта: ");
            string MenuControl = Console.ReadLine();
            switch (MenuControl)
            {
                case "1":
                    GuessTheAnswer();
                    break;
                case "2":
                    AboutTheAuthor();
                    break;
                case "3":
                    SortingAnArray();
                    break;
                case "4":
                    if (Exit())
                    {
                        Console.WriteLine("Выход из программы...");
                        return;
                    }
                    break;
                default:
                    Console.WriteLine("Ошибка: выберите пункт меню 1, 2, 3 или 4.");
                    break;
            }
        }
    }

    static void GuessTheAnswer()
    {
        double a = NumericValueDouble("Введите значение переменной a в радианах: ");
        double b = PositiveNumberDouble("Введите значение переменной b (положительное число): ");
        double result = CalculationFunction(a, b);
        Console.WriteLine($"Результат: {result:F2}");
        AttemptsPlay(result);
    }
    static double CalculationFunction(double a,double b)
    {
        double lnB = Math.Log(b);
        return (Math.Pow(lnB, 2) / Math.Cos(a)) - 1;
    }
    static void AttemptsPlay(double resultFinal)
    {
        int attempts = 3;
        while (attempts > 0)
        {
            double userAnswer = NumericValueDouble("Введите ваш ответ: ");
            if (Math.Abs(userAnswer - resultFinal) < 0.01)
            {
                Console.WriteLine($"Правильно! Ответ: {resultFinal:F2}.");
                return;
            }
            else
            {
                attempts--;
                Console.WriteLine($"Неправильно. У вас осталось {attempts} попыток.");
            }
        }
        Console.WriteLine($"Вы проиграли. Правильный ответ: {resultFinal:F2}.");
    }
    static void AboutTheAuthor()
    {
        Console.WriteLine("ФИО студента: Пологов Николай Андреевич");
        Console.WriteLine("Группа: 6106");
    }
    static void SortingAnArray()
    {   
        int arraySize = PositiveNumberInt("Введите размер массива: ");

        int[]array = NewArrey(arraySize);
        PrintArray(array);        
        int[] bubbleSortArray = CopyArray(array);
        int[] mixingSortArray = CopyArray(array);

        BubbleSort(bubbleSortArray);
        Console.WriteLine("Массив после сортировки пузырьком:");
        PrintArray(bubbleSortArray);

        Stopwatch stopwatch = Stopwatch.StartNew();
        bubbleSortArray = BubbleSort(bubbleSortArray);
        stopwatch.Stop();
        Console.WriteLine($"Время выполнения сортировки пузырьком: {stopwatch.ElapsedTicks / (Stopwatch.Frequency / 1000000)} мкс ");
        
        MixingSort(mixingSortArray);
        Console.WriteLine("Массив после сортировки перемешиванием:");
        PrintArray(mixingSortArray);

        stopwatch.Restart();
        mixingSortArray = MixingSort(mixingSortArray);
        stopwatch.Stop();
        Console.WriteLine($"Время выполнения сортировки перемешиванием: {stopwatch.ElapsedTicks / (Stopwatch.Frequency / 1000000)} мкс ");
    }
    static int[] NewArrey(int arraySize)
    {
        int[] array1 = new int[arraySize];
        Random random = new Random();
        for (int i = 0; i < arraySize; i++)
        {
            array1[i] = random.Next(1, 1000001);
        }
        return array1;
    }
    static int[] BubbleSort(int[] arrayBabble)
    {
        int n = arrayBabble.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (arrayBabble[j] > arrayBabble[j + 1])
                {
                    int temp = arrayBabble[j];
                    arrayBabble[j] = arrayBabble[j + 1];
                    arrayBabble[j + 1] = temp;
                }
            }
        }
        return arrayBabble;
    }

    static int[] MixingSort(int[] arrayMixing)
    {
        int left = 0;
        int right = arrayMixing.Length - 1;
        while (left < right)
        {
            for (int i = left; i < right; i++)
            {
                if (arrayMixing[i] > arrayMixing[i + 1])
                {
                    int temp = arrayMixing[i];
                    arrayMixing[i] = arrayMixing[i + 1];
                    arrayMixing[i + 1] = temp;
                }
            }
            right--;

            for (int i = right; i > left; i--)
            {
                if (arrayMixing[i - 1] > arrayMixing[i])
                {
                    int temp = arrayMixing[i];
                    arrayMixing[i] = arrayMixing[i - 1];
                    arrayMixing[i - 1] = temp;
                }
            }
            left++;
        }
        return arrayMixing;
    }

    static int[] CopyArray(int[] initialArray)
    {
        int[] newArray = new int[initialArray.Length];
        for (int i = 0; i < initialArray.Length; i++)
        {
            newArray[i] = initialArray[i];
        }

        return newArray;
    }

    static void PrintArray(int[] array)
    {
        if (array.Length > 10)
        {
            Console.WriteLine("Массивы не могут быть выведены на экран, так как длина массива больше 10.");
        }
        else
        {
            foreach (var item in array)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }
    }
    static bool Exit()
    {
        while (true)
        {
            Console.Write("Вы уверены, что хотите выйти? (д/н): ");
            string confirm = Console.ReadLine();
            if (confirm.ToLower() == "д")
            {
                return true; 
            }
            else if (confirm.ToLower() == "н")
            {
                return false; 
            }
            else
            {
                Console.WriteLine("Ошибка: введите 'д' для выхода или 'н' для продолжения.");
            }
        }
    }
    static double NumericValueDouble(string examination)
    {
        double result;
        while (true)
        {
            Console.Write(examination);
            if (double.TryParse(Console.ReadLine(), out result))
            {
                return result;
            }
            else
            {
                Console.WriteLine("Ошибка: введите числовое значение.");
            }
        }
    }
    static double PositiveNumberDouble(string examination)
    {
        double result;
        while (true)
        {
            result = NumericValueDouble(examination);
            if (result > 0)
            {
                return result;
            }
            else
            {
                Console.WriteLine("Ошибка: введенное число должно быть положительным.");
            }
        }
    }
    static int PositiveNumberInt(string examination)
    {
        int result;
        while (true)
        {
            Console.Write(examination);
            if (int.TryParse(Console.ReadLine(), out result) && result > 0)
            {
                return result;
            }
            else
            {
                Console.WriteLine("Ошибка: введите положительное целое число.");
            }
        }
    }
}