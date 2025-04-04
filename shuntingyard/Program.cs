using System;
using System.Collections.Generic;

public class Program
{

    static List<string> ConvertToRPNWithBrackets(string input)
    {
        List<string> tokens = new List<string>();
        int temp = 0;
        for (int i = 0; i < input.Length; i++)
        {
            if (Char.IsDigit(input[i]))
            {
                temp *= 10;
                temp += int.Parse(input[i].ToString());
            }
            else if (input[i] == 42 || input[i] == 43 || input[i] == 45 || input[i] == 47 || input[i] == 40 || input[i] == 41)
            {
                if (temp != 0)
                {
                    tokens.Add(temp.ToString());
                    temp = 0;
                }
                tokens.Add(input[i].ToString());
            }
            else if (i == input.Length - 1)
                tokens.Add(temp.ToString());
        }
        string pretty = $"[ {string.Join(", ", tokens)} ]";

        Dictionary<string, int> preced = new Dictionary<string, int>
        {
            { "+", 2 }, { "-", 2 }, { "*", 4 }, { "/", 4 }
        };
        List<string> q = new List<string> { };
        Stack<string> s = new Stack<string>();
        for (int i = 0; i < tokens.Count; i++)
        {
            Console.WriteLine(tokens.Count);
            Console.WriteLine($"i = {i}");
            Console.WriteLine($"{pretty} \n");

            Console.Write("Queue: ");
            foreach (var item in q) Console.Write($"{item} ");
            Console.WriteLine();
            Console.Write("Stack: ");
            foreach (var item in s) Console.Write($"{item} ");
            Console.WriteLine("\n");

            if (int.TryParse(tokens[i], out _))
            {
                Console.WriteLine($"{tokens[i]} is a number so queueing it");
                q.Add(tokens[i]);
            }
            else if (tokens[i] == "+" || tokens[i] == "*" || tokens[i] == "-" || tokens[i] == "/")
            {
                Console.WriteLine($"{tokens[i]} is an operator");
                while (true)
                {
                    if (s.Count == 0 || s.Peek() == "(")
                    {
                        Console.WriteLine($"Finally pushing {tokens[i]} to the stack");
                        s.Push(tokens[i]);
                        break;
                    }
                    if (preced[s.Peek()] > 2)
                    {
                        Console.WriteLine($"{tokens[i]} has lower precedence than {s.Peek()} so popping and adding {s.Peek()} to the queue");
                        q.Add(s.Pop());
                        continue;
                    }
                    Console.WriteLine($"Finally pushing {tokens[i]} to the stack");
                    s.Push(tokens[i]);
                    break;
                }
            }
            else if (tokens[i] == "(")
            {
                Console.WriteLine($"pushing {tokens[i]} onto the stack");
                s.Push(tokens[i]); 
                
            }
            else if (tokens[i] == ")")
            {
                while (true)
                {
                    if (s.Count == 0)
                    {
                        break;
                    }
                    if (s.Peek() == "(")
                    {
                        s.Pop();
                        break;
                    }
                    Console.WriteLine($"{s.Peek()} is an opernand so enqueueing");
                    q.Add(s.Pop());
                }
            }
            Console.ReadLine();
            Console.Clear();
        }


        return q;
    }
    public static void Main(string[] args)
    {

        Console.WriteLine($"[ {string.Join(", ", ConvertToRPNWithBrackets("5+2/((5-1)+3))"))} ]");
        Console.WriteLine("     _\r\n  __| | ___  _ __   ___\r\n / _` |/ _ \\| '_ \\ / _ \\\r\n| (_| | (_) | | | |  __/\r\n \\__,_|\\___/|_| |_|\\___|\r\n");
    }
}