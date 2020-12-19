using System;

namespace Метод_Гаусса
{
    class Output
    {
        //Метод для вывода СЛАУ (SLAE)
        public static void OutputSLAE(double[,] array, double[] y, int n)
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
                        }
                        else if (j < n)
                        {
                            Console.Write(" - ");
                            Console.Write($"{-1 * array[i, j]}*x{j + 1}");
                        }
                    }
                    if ((array[i, j] >= 0))
                    {
                        if (j < n && j > 0)
                            Console.Write(" + ");
                        Console.Write($"{array[i, j]}*x{j + 1}");
                    }
                }
                Console.WriteLine($" = { y[i]}");
            }
        }
        //Метод для линейного выражения вектора X(a) (если элементами матрицы являются координаты векторов , образующих базис)
        public static void OutputVector(double[] x, double n)
        {
            Console.Write("\nЛинейное выражение вектора X(a) = ");
            for (int i = 0; i < n; i++)
            {
                if (i < n && i > 0)
                {
                    if (x[i] >= 0)
                    {
                        Console.Write("+");
                    }
                }
                Console.Write($"{x[i]}*a{i + 1}");
            }
            Console.WriteLine($"\n\nВектора линейно независимы и образуют базис пространства R{n} ");
        }
    }
    class GaussMethod
    {
        //Метод Гаусса
        public static double[] GaussianSolution(double[,] array, double[] y, int n)
        {
            double maxElement, denominator, epsilon , forPermutation;
            int k, index ;
            double[] x;
            // Инициализация
            InitGaussianSolution(n, out k, out index, out epsilon, out x ,out denominator);
            while (k < n)
            {
                // Поиск строки с максимальным элементом array[i,k]
                FindingMaximumElement(array, n, out maxElement, k, out index);
                // Перестановка строк
                if (maxElement < epsilon)
                {
                    Console.WriteLine("Решение получить невозможно из-за нулевого столбца ");
                    return null;
                }
                for (int j = 0; j < n; j++)
                {
                    forPermutation = array[k, j];
                    array[k, j] = array[index, j];
                    array[index, j] = forPermutation;
                }
                forPermutation = y[k];
                y[k] = y[index];
                y[index] = forPermutation;
                // Нормализация уравнений
                denominator = EquationNormalization(array, y, n, denominator, k, epsilon);
                k++;
            }
            // Обратная подстановка
            k = ReverseSubstitution(array, y, n, x);
            return x;
        }
        // Инициализация для GaussianSolution
        private static void InitGaussianSolution(int n, out int k, out int index, out double epsilon, out double[] x ,out double denominator)
        {
            k = 0;
            index = 0;
            epsilon = 0.00001; // точность
            x = new double[n];
            denominator = 0;
        }
        // Метод для Обратной подстановка
        private static int ReverseSubstitution(double[,] array, double[] y, int n, double[] x)
        {
            int k;
            for (k = n - 1; k >= 0; k--)
            {
                x[k] = Math.Round(y[k], 3);
                for (int i = 0; i < k; i++)
                    y[i] = y[i] - array[i, k] * x[k];
            }

            return k;
        }
        // Метод для Нормализации уравнений
        private static double EquationNormalization(double[,] array, double[] y, int n, double denominator, int k, double epsilon)
        {
            for (int i = k; i < n; i++)
            {
                denominator = Math.Round(array[i, k], 5);
                if (Math.Abs(denominator) < epsilon) continue; // Пропускать для нулевого элемента
                for (int j = 0; j < n; j++)
                    array[i, j] = Math.Round(array[i, j] / denominator, 8);
                y[i] = Math.Round(y[i] / denominator, 8);
                if (i == k) continue; // Чтобы не вычитать уравнение само из себя
                for (int j = 0; j < n; j++)
                    array[i, j] = array[i, j] - array[k, j];
                y[i] = y[i] - y[k];
            }

            return denominator;
        }
        // Метод для Поиска строки с максимальным элементом array[i,k]
        private static void FindingMaximumElement(double[,] array, int n, out double maxElement, int k, out int index)
        {
            maxElement = Math.Round(Math.Abs(array[k, k]), 5);
            index = k;
            for (int i = k + 1; i < n; i++)
            {
                if (Math.Round(Math.Abs(array[i, k]), 5) > maxElement)
                {
                    maxElement = Math.Round(Math.Abs(array[i, k]), 5);
                    index = i;
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите количество уравнений ");
            int n = int.Parse(Console.ReadLine());
            double[,] array = new double[n, n];
            double[] y = new double[n];
            double[] x = new double[n];
            EnteringElementsFromConsole(n, array, y);
            Output.OutputSLAE(array, y, n);
            x = GaussMethod.GaussianSolution(array, y, n);
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"x[{i + 1}]= {x[i]}");
            }
            Output.OutputVector(x, n);
        }
        // Ввожу элементы матрицы с консоли
        private static void EnteringElementsFromConsole(int n, double[,] array, double[] y)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"a[{i + 1},{j + 1}]= ");
                    array[i, j] = Convert.ToDouble(Console.ReadLine());
                }
            }
            Console.WriteLine();
            for (int i = 0; i < n; i++)
            {
                Console.Write($"y[{i + 1}]= ");
                y[i] = int.Parse(Console.ReadLine());
            }
            Console.WriteLine();
        }
    }
}