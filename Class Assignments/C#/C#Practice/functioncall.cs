using System;


namespace C_Practice
{
    internal class functioncall
    {
        
        private static int c = 0;

        
        public static void CountFunction()
        {
            c++;
            Console.WriteLine($"CountFunction has been called {c} times.");
        }
    }
/*
    internal class Programs
    {
        static void Main()
        {
          
            functioncall.CountFunction();
            functioncall.CountFunction();
            functioncall.CountFunction();
        }
    }*/
}
