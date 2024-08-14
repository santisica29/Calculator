using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram;
class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");

        Calculator calc = new Calculator();
        while (!endApp)
        {
            Console.Clear();
            // Declare variables and set to empty.
            // Use Nullable types (with ?) to match type of System.Console.ReadLine
            string? numInput1 = "";
            string? numInput2 = "";
            double cleanNum1 = 0;
            double result = 0;

            double res = ShowMenu(ref calc);

            if (res == 0)
            {
                // Ask the user to type the first number.
                Console.Write("Type a number, and then press Enter: ");
                numInput1 = Console.ReadLine();

                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Console.Write("This is not valid input. Please enter a numeric value: ");
                    numInput1 = Console.ReadLine();
                }
            }
            else
            {
                cleanNum1 = res;
            }

            // Ask the user to type the second number.
            Console.Write("Type another number, and then press Enter: ");
            numInput2 = Console.ReadLine();

            double cleanNum2 = 0;
            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                numInput2 = Console.ReadLine();
            }

            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();

            // Validate input is not null, and matches the pattern
            if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
            {
                Console.WriteLine("Error: Unrecognized input.");
            }
            else
            {
                try
                {
                    result = calc.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else Console.WriteLine("Your result: {0:0.##}\n", result);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }
            }
            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            Console.Write(@"Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
            if (Console.ReadLine().ToLower() == "n") endApp = true;

            Console.WriteLine("\n"); // Friendly linespacing;
        }
        calc.Finish();
        return;
    }

    internal static double ShowMenu(ref Calculator calc)
    {
        double res = 0;

        Console.WriteLine("Choose 'n' to make a new calculation or 'v' to view your latest calculations\n");
        var choice = Console.ReadLine().ToLower().Trim();

        while (choice != "n" && choice != "v")
        {
            Console.WriteLine("Invalid input. Try Again.");
            choice = Console.ReadLine().ToLower().Trim();
        }

        if (choice == "v")
        {
            Console.Clear();
            Console.WriteLine("Latest Calculations:\n");

            List<string> list = calc.GetLatestsCalculations();

            if (list == null)
            {
                Console.WriteLine("List is empty");
            }
            else
            {
                foreach (var c in list)
                {
                    Console.WriteLine(c);
                }

                Console.WriteLine("Press 'd' to delete the list");
                Console.WriteLine("'n' to start a new calculation");
                Console.WriteLine("'u' to use one of the latest results to make a new operation");
                var input = Console.ReadLine().ToLower().Trim();

                while (input != "n" && input != "u" && input != "d")
                {
                    Console.WriteLine("Invalid input");
                    input = Console.ReadLine().ToLower();
                }

                if (input == "d")
                {
                    calc.DeleteLists();
                    Console.WriteLine("List Deleted");
                    Console.ReadLine();
                }

                if (input == "u")
                {
                    Console.Clear();

                    var listR = calc.GetLatestsResults();

                    Console.WriteLine("Latest results: \n");
                    foreach (var item in listR)
                    {
                        Console.Write($"{item}. y/n ");
                        var c = Console.ReadLine().ToLower();

                        if (c == "y")
                        {
                            res = item;
                            break;
                        }
                    }
                }
            }
        }

        if (res != 0) return res;
        else return 0;
    }
}

