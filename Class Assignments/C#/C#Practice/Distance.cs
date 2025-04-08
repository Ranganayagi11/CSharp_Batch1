using System;


namespace C_Practice
{
    class Distance
    {
        public int dist;

        public static Distance operator +(Distance a, Distance b)

        {

            Distance temp = new Distance();
            temp.dist = a.dist + b.dist;
            return temp;
        }
        
    }
}
