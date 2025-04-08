using System.ComponentModel;

namespace C_Practice
{
    internal class Program
    {
        static void Mains(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Program program = new Program();
            program.Equalnum();
            //program.postiveornegative();
            //program.operations();
            //program.mulTable();
        }
        //1. Write a C# Sharp program to accept two integers and check whether they are equal or not. 
        //5.  Write a C# program to compute the sum of two given integers. If two values are the same, return the triple of their sum.
        public void Equalnum()
        {
            int num1, num2;
            Console.WriteLine("Enter the first number: ");
            num1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the second number: ");
            num2 = int.Parse(Console.ReadLine());
            //num1 == num2 ? Console.WriteLine("Numbers are Equal") : Console.WriteLine("Numbers not Equal");
            if (num1 == num2)
            {
                Console.WriteLine("Numbers are Equal");
                int sum=num1 + num2;
                Console.WriteLine("Triple of number sum is "+(sum*sum*sum));
            }
            else
                Console.WriteLine("Numbers are not equal");
        }
        //2. Write a C# Sharp program to check whether a given number is positive or negative.
        public void postiveornegative()
        {
            Console.Write("Input a number: ");
            int num = int.Parse(Console.ReadLine());

            if (num > 0)
                Console.WriteLine($"{num} is a positive number");
            else if (num < 0)
                Console.WriteLine($"{num} is a negative number");
            else
                Console.WriteLine("The number is zero");
        }
        //3. Write a C# Sharp program that takes two numbers as input and performs all operations (+,-,*,/) on them and displays the result of that operation. 
        public void operations()
        {
            int num1,num2;
            Console.WriteLine("Enter the first number:");
            num1=int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the operation within +,-,*/ ");
            char op = Console.ReadLine()[0];
            Console.WriteLine("Enter the Second number:");
            num2=int.Parse(Console.ReadLine());
            switch(op)
            {
                case '+':
                    Console.WriteLine($"{num1} + {num2} = {num1 + num2}"); break;
                case '-':
                    Console.WriteLine($"{num1} - {num2} = {num1 - num2}"); break;
                case '*':
                    Console.WriteLine($"{num1} * {num2} = {num1 * num2}"); break;
                case '/':
                    Console.WriteLine($"{num1} / {num2} = {num1 / num2}"); break;
                default:
                    Console.WriteLine("Invalid Operation");  break;
            }

        }
        //4. Write a C# Sharp program that prints the multiplication table of a number as input.
        public void mulTable()
        {
            int num;
            Console.WriteLine("Enter the number to print tables: ");
            num = int.Parse(Console.ReadLine());
            for (int i = 1; i <= 10; i++)
                Console.WriteLine(num + " * " + i + " = " + (num * i));
        }

    }
}