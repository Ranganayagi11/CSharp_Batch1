using System;


namespace C_Practice
{
    class TimePeriod
    {
        private double seconds;

     
        public double Hours
        {
            get { return seconds / 3600; } 
            set { seconds = value * 3600; } 
        }
    }

    /*class main
    {
        static void Main()
        {
            TimePeriod tp = new TimePeriod();
            tp.Hours = 2.5; 
            Console.WriteLine($"Time in Hours: {tp.Hours}"); 
        }
    }*/
}
