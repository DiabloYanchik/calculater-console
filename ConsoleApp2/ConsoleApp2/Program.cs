using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        delegate double Operation(double x, double y);
        private static bool isInterfaceActive = true; // флаг для контроля обновления интерфейса
        static void Main(string[] args)
        {
            var operations = new Dictionary<string, Operation>()
            {
                {"+", (x, y) => x + y},
                {"-", (x, y) => x - y},
                {"*", (x, y) => x * y},
                {"/", (x, y) => y != 0 ? x / y : throw new DivideByZeroException()}
            };
            // Строка, в которой будет отображаться текущий ввод
            StringBuilder currentInput = new StringBuilder();
            double result = 0;
            bool isResultDisplayed = false;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("               КАЛЬКУЛЯТОР");
                Console.WriteLine("========================================");
                Console.WriteLine($"Писать сюда:  {currentInput}");
                Console.WriteLine("========================================");
                Console.WriteLine(" |  [ 1 ]    [ 2 ]    [ 3 ]    [ + ]  | ");  // Интерфейс калькулятора
                Console.WriteLine(" |                                    | ");
                Console.WriteLine(" |  [ 4 ]    [ 5 ]    [ 6 ]    [ - ]  | ");
                Console.WriteLine(" |                                    | ");
                Console.WriteLine(" |  [ 7 ]    [ 8 ]    [ 9 ]    [ * ]  | ");
                Console.WriteLine(" |                                    | ");
                Console.WriteLine(" |  [ 0 ]    [ . ]    [ = ]    [ / ]  | ");
                Console.WriteLine(" |                                    | ");
                Console.WriteLine("========================================");
                Console.WriteLine("Для ввода чисел и операторов используйте клавиши.");
                Console.WriteLine("Нажмите 'Enter' для вычисления результата.");
                Console.WriteLine("Для выхода нажмите на клавишу 'Escape'.");
                Console.WriteLine("========================================");

                // Обработка нажатия клавиш
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                //exit
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    break;
                }
                //=
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (currentInput.Length > 0 && !isResultDisplayed)//в строке ввода сичтаем длину и не !
                    {
                        try
                        {
                            string expression = currentInput.ToString();
                            result = EvaluateExpression(expression);
                            isResultDisplayed = true;
                            currentInput.Clear();
                            currentInput.Append(result);
                        }
                        catch (Exception ex)
                        {
                            StopInterfaceUpdates();
                            Console.WriteLine($"Ошибка: {ex.Message}");
                        }

                    }
                }
                // <--
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (currentInput.Length > 0)
                    {
                        currentInput.Remove(currentInput.Length - 1, 1); // Удаляем последний символ
                    }
                }
                //1234567890
                else if (keyInfo.Key == ConsoleKey.D0 || keyInfo.Key == ConsoleKey.D1 || keyInfo.Key == ConsoleKey.D2 ||
                         keyInfo.Key == ConsoleKey.D3 || keyInfo.Key == ConsoleKey.D4 || keyInfo.Key == ConsoleKey.D5 ||
                         keyInfo.Key == ConsoleKey.D6 || keyInfo.Key == ConsoleKey.D7 || keyInfo.Key == ConsoleKey.D8 ||
                         keyInfo.Key == ConsoleKey.D9)
                {
                    // Добавляем цифры
                    if (isResultDisplayed)
                    {
                        currentInput.Clear();
                        isResultDisplayed = false;
                    }

                    currentInput.Append(keyInfo.KeyChar);
                }
                //-+/*
                else if (keyInfo.Key == ConsoleKey.OemPlus || keyInfo.Key == ConsoleKey.OemMinus ||
                         keyInfo.Key == ConsoleKey.Multiply || keyInfo.Key == ConsoleKey.Divide)
                {
                    // Добавляем операторы
                    if (isResultDisplayed)
                    {
                        currentInput.Clear();
                        isResultDisplayed = false;
                    }
                    currentInput.Append(keyInfo.KeyChar);
                }
                //польбому отдельно
                else if (keyInfo.Key == ConsoleKey.D0)
                {
                    currentInput.Append("0");
                }
            }
        }

        // Метод для вычисления выражения с несколькими операциями
        static double EvaluateExpression(string expression)
        {
            try
            {
                // Используем встроенный DataTable для парсинга и вычисления выражений
                var table = new DataTable();
                return Convert.ToDouble(table.Compute(expression, string.Empty));//вычислять выражения, записанные в виде строк compute
            }
            catch (Exception ex)//обр.искл
            {
                
                throw new Exception("Невозможно вычислить выражение. Проверьте правильность ввода.");
            }
        }
        private static void StopInterfaceUpdates()
        {
            isInterfaceActive = false; // Остановка обновления интерфейса
           
        }
    }
}
    

