using System;


namespace C_Practice
{
   
    abstract class Furniture
    {
        public string Material { get; set; }
        public string Color { get; set; }

        public Furniture(string material, string color)
        {
            Material = material;
            Color = color;
        }

        // Abstract method to be implemented by derived classes
        public abstract void DisplayDetails();
    }

 
    class Chair : Furniture
    {
        public int NumberOfLegs { get; set; }

        public Chair(string material, string color, int numberOfLegs)
            : base(material, color)
        {
            NumberOfLegs = numberOfLegs;
        }

        public override void DisplayDetails()
        {
            Console.WriteLine($"Chair - Material: {Material}, Color: {Color}, Legs: {NumberOfLegs}");
        }
    }


    class Bookshelf : Furniture
    {
        public int NumberOfShelves { get; set; }

        public Bookshelf(string material, string color, int numberOfShelves)
            : base(material, color)
        {
            NumberOfShelves = numberOfShelves;
        }

        public override void DisplayDetails()
        {
            Console.WriteLine($"Bookshelf - Material: {Material}, Color: {Color}, Shelves: {NumberOfShelves}");
        }
    }

    class furniture
    {
        static void Main()
        {
            
            Chair chair = new Chair("Wood", "Brown", 4);
            chair.DisplayDetails();

            
            Bookshelf bookshelf = new Bookshelf("Metal", "Black", 5);
            bookshelf.DisplayDetails();
        }
    }
}
