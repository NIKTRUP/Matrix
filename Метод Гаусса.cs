using System;

namespace Метод_Гаусса
{
    class Program
    {
        //Метод для вывода
        static void Conclusion(double[,] array, double[] y, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (array[i, j] < 0)
                    {
                        if (j == 0)
                        {
                            Console.Write($"{array[i, j]}*x{j + 1}");
                        }else if (j < n)
                        {
                            Console.Write(" - ");
                            Console.Write($"{-1 * array[i, j]}*x{j + 1}");
                        }
                    }
                     if ((array[i, j] >= 0))
                    {
                        if (j < n  && j > 0)
                            Console.Write(" + ");
                        Console.Write($"{array[i, j]}*x{j + 1}"); 
                    }
                }
                Console.WriteLine($" = { y[i]}");
            }
        }
        //Метод Гаусса
        public static double[] GaussMethod(double[,] array, double[] y, int n)
        {
            double max , denominator;
            int k=0, index=0;
            const double eps = 0.00001;  // точность
            double[] x = new double[n];
            while (k < n)
            {
                // Поиск строки с максимальным array[i,k]
                max = Math.Round(Math.Abs(array[k, k]),5);
                for (int i = k + 1; i < n; i++)
                {
                    if (Math.Round(Math.Abs(array[i, k]), 5) > max)
                    {
                        max = Math.Round(Math.Abs(array[i, k]),5);
                        index = i;
                    }
                }
                // Перестановка строк
                if (max < eps)
                {
                    Console.WriteLine("Решение получить невозможно из-за нулевого столбца ");
                    return null;
                }
                for (int j = 0; j < n; j++)
                {
                    denominator = array[k, j];
                    array[k, j] = array[index, j];
                    array[index, j] = denominator;
                }
                denominator = y[k];
                y[k] = y[index];
                y[index] = denominator;
                // Нормализация уравнений
                for (int i = k; i < n; i++)
                {
                    denominator = array[i, k];
                    if (Math.Abs(denominator) < eps) continue; // Пропускать для нулевого элемента
                    for (int j = 0; j < n; j++)
                        array[i, j] = Math.Round(array[i, j]/ denominator, 8);
                    y[i] = Math.Round(y[i]/ denominator, 8);
                    if (i == k) continue; // Чтобы не вычитать уравнение само из себя
                    for (int j = 0; j < n; j++)
                        array[i, j] = array[i, j] - array[k, j];
                    y[i] = y[i] - y[k];
                }
                k++;
            } 
                // обратная подстановка
                for (k = n - 1; k >= 0; k--)
                {
                    x[k] = Math.Round(y[k],3);
                    for (int i = 0; i < k; i++)
                        y[i] = y[i] - array[i,k] * x[k];
                }
            return x;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Введите количество уравнений ");
            int n = int.Parse(Console.ReadLine());
            double[,] array = new double[n , n];
            double[] y = new double[n];
            double[] x = new double[n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"a[{i+1},{j+1}]= ");
                    array[i, j] = Convert.ToDouble(Console.ReadLine());
                }
            }
            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                Console.Write($"y[{i+1}]= ");
                y[i] = int.Parse(Console.ReadLine());
            }
            Console.WriteLine();
            Conclusion(array, y, n);         
            x = GaussMethod(array, y, n);
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"x[{i+1}]= {x[i]}");
            }
        }
    }    
}
