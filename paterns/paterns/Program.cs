using System;
using System.Text;

namespace paterns
{
    interface ITransport
    {
        class CarColour
        {
            public string Colour { get; set; }
        }
        class BodyType
        {
            public string Body { get; set; }
        }
        class NumberOfDoors
        {
            public string Doors { get; set; }
        }

        abstract class Builder
        {
            public Car Car { get; set; }
            public void CreateCar()
            {
                Car = new Car();
            }
            public abstract void SetColour();
            public abstract void SetBody();
            public abstract void SetNumberOfDoors();
        }
        class Concern
        {
            public Car Create(Builder builder)
            {
                builder.CreateCar();
                builder.SetColour();
                builder.SetBody();
                builder.SetNumberOfDoors();
                return builder.Car;
            }
        }
        class SedanBuilder : Builder
        {
            public override void SetColour()
            {
                this.Car.CarColour = new CarColour { Colour = "Красный" };
            }
            public override void SetBody()
            {
                this.Car.BodyType = new BodyType { Body = "Седан" };
            }
            public override void SetNumberOfDoors()
            {
                this.Car.NumberOfDoors = new NumberOfDoors { Doors = "2" };
            }
        }
        class HatchbackBuilder : Builder
        {
            public override void SetColour()
            {
                this.Car.CarColour = new CarColour { Colour = "Черный" };
            }
            public override void SetBody()
            {
                this.Car.BodyType = new BodyType { Body = "Хэтчбэк" };
            }
            public override void SetNumberOfDoors()
            {
                this.Car.NumberOfDoors = new NumberOfDoors { Doors = "4" };
            }
        }
        class Car
        {
            public CarColour CarColour { get; set; }
            public BodyType BodyType { get; set; }
            public NumberOfDoors NumberOfDoors { get; set; }
            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                if (BodyType != null)
                    sb.Append("Тип кузова: " + BodyType.Body + "\n");
                if (CarColour != null)
                    sb.Append("Цвет: " + CarColour.Colour + "\n");
                if (NumberOfDoors != null)
                    sb.Append("Кол-во дверей: " + NumberOfDoors.Doors + "\n");
                return sb.ToString();
            }
        }
        void Drive();
    }
    class Auto : ITransport
    {
        public void Drive()
        {
            Console.WriteLine("Машина едет по дороге \n");
        }
    }
    class Driver
    {
        public void Travel(ITransport transport)
        {
            transport.Drive();
        }
    }
    interface IAnimal
    {
        class CamelBuilder : ITransport.Builder
        {
            public override void SetColour()
            {
                this.Car.CarColour = new ITransport.CarColour { Colour = "Коричневый" };
            }
            public override void SetBody()
            {
                this.Car.BodyType = new ITransport.BodyType { Body = "Двугорбый" };
            }
            public override void SetNumberOfDoors()
            {
                this.Car.NumberOfDoors = new ITransport.NumberOfDoors { Doors = "0" };
            }
        }
        void Move();
    }
    class Camel : IAnimal
    {
        public void Move()
        {
            Console.WriteLine("Верблюд идет по путыне \n");
        }
    }
    class CamelToTransportAdapter : ITransport
    {
        Camel camel;
        public CamelToTransportAdapter(Camel c)
        {
            camel = c;
        }
        public void Drive()
        {
            Console.WriteLine("Смена машины на верблюда");
            camel.Move();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ITransport.Concern concern = new ITransport.Concern();

            ITransport.Builder builder = new ITransport.SedanBuilder();
            ITransport.Car sedan = concern.Create(builder);
            Console.WriteLine(sedan.ToString());

            builder = new ITransport.HatchbackBuilder();
            ITransport.Car hatchback = concern.Create(builder);
            Console.WriteLine(hatchback.ToString());

            Driver driver = new Driver();
            Auto auto = new Auto();
            driver.Travel(auto);

            Camel camel = new Camel();
            ITransport camelTransport = new CamelToTransportAdapter(camel);

            driver.Travel(camelTransport);

            builder = new IAnimal.CamelBuilder();
            ITransport.Car cam = concern.Create(builder);
            Console.WriteLine(cam.ToString());


            Console.Read();
        }
    }
}
