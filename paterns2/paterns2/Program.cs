using System;

namespace paterns2
{
    interface IMovable
    {
        void Move();
    }
    class Atack : IMovable
    {
        public void Move() { Console.WriteLine("Атакует"); }
    }
    class Defence : IMovable
    {
        public void Move() { Console.WriteLine("Обороняется"); }
    }

    class Checker
    {
        public void Check(Army army)
        {
            IUnitIter iter = army.CreateNum();
            while (iter.HasNext())
            {
                Unit unit = iter.Next();
                unit.GetName();
            }
        }
    }

    interface IUnitIter
    {
        bool HasNext();
        Unit Next();
    }
    interface IUnitNum
    {
        IUnitIter CreateNum();
        int Count { get; }
        Unit this[int index] { get; }
    }
    class Unit
    {
        protected string name;
        public void GetName() { Console.WriteLine(name); }
        public Unit(string name, IMovable mov)
        {
            this.name = name;
            Movable = mov;
        }
        public IMovable Movable { private get; set; }
        public void Move()
        {
            Movable.Move();
        }
    }
    class Army : IUnitNum
    {
        private Unit[] units;
        public Army(Unit[] _units)
        {
            units = _units;
        }
        public int Count
        {
            get { return units.Length; }
        }
        public Unit this[int index]
        {
            get { return units[index]; }
        }
        public IUnitIter CreateNum()
        {
            return new ArmyNum(this);
        }
    }

    class ArmyNum : IUnitIter
    {
        IUnitNum aggr;
        int index = 0;
        public ArmyNum(IUnitNum a)
        {
            aggr = a;
        }
        public bool HasNext()
        {
            return index < aggr.Count;
        }
        public Unit Next()
        {
            return aggr[index++];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Unit unit = new Unit("Взвод 1", new Defence());
            unit.Move();
            unit.Movable = new Atack();
            unit.Move();

            Unit[] units = new Unit[]
            {
                unit,
                new Unit("Взвод 2", new Atack()),
                new Unit("Взвод 3", new Defence()),
                new Unit("Взвод 4", new Atack()),
                new Unit("Взвод 5", new Defence())
            };

            Army army = new Army(units);
            Checker checker = new Checker();
            checker.Check(army);
        }
    }
}
