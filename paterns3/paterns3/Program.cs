using System;

namespace paterns3
{
    class Program
    {
        static void Main(string[] args)
        {
            Concern c = new SedanCreator("Volkswagen");
            Car car1 = c.Create();

            c = new HatchbackCreator("Fiat");
            Car car2 = c.Create();

            Console.ReadLine();
        }
    }
    abstract class Car
    { }

    class Sedan : Car
    {
        public Sedan()
        {
            Console.WriteLine("Собран седан");
        }
    }
    class Hatchback : Car
    {
        public Hatchback()
        {
            Console.WriteLine("Собран хетчбэк");
        }
    }

    abstract class Concern
    {
        public string Name { get; set; }
        public Concern (string n)
        {
            Name = n;
        }
        abstract public Car Create();
    }

    class SedanCreator : Concern
    {
        public SedanCreator (string n) : base(n)
        {
            Console.WriteLine(n + " ");
        }
        public override Car Create()
        {
            return new Sedan();
        }
    }
    class HatchbackCreator : Concern
    {
        public HatchbackCreator(string n) : base(n)
        {
            Console.WriteLine(n + " ");
        }
        public override Car Create()
        {
            return new Hatchback();
        }
    }
}
