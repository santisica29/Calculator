﻿using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary;
public class Calculator
{
    int timesItWasUsed = 0;
    JsonWriter writer;
    public Calculator()
    {
        StreamWriter logFile = File.CreateText("calculatorlog.json");
        logFile.AutoFlush = true;
        writer = new JsonTextWriter(logFile);
        writer.Formatting = Formatting.Indented;
        writer.WriteStartObject();
        writer.WritePropertyName("Operations");
        writer.WriteStartArray();
    }
    public double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        writer.WritePropertyName("Operand2");
        writer.WriteValue(num2);
        writer.WritePropertyName("Operation");
        // Use a switch statement to do the math.
        switch (op)
        {
            case "a":
                result = num1 + num2;
                writer.WriteValue("Add");
                break;
            case "s":
                result = num1 - num2;
                writer.WriteValue("Substract");
                break;
            case "m":
                result = num1 * num2;
                writer.WriteValue("Multiply");
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                while (num2 == 0)
                {
                    Console.WriteLine("You can't divide by 0. Pick another number");
                    num2 = int.Parse(Console.ReadLine());
                }
                result = num1 / num2;
                writer.WriteValue("Divide");
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        timesItWasUsed++;
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        return result;
    }

    public void Finish()
    {
        CountTimesTheCalcWasUsed(timesItWasUsed);
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }

    public void CountTimesTheCalcWasUsed(int timesItWasUsed)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("Times the calc was used");
        writer.WriteValue(timesItWasUsed);
        writer.WriteEndObject();
    }
}
