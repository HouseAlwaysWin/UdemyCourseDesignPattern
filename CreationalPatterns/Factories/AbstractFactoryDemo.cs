using System;
using static System.Console;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

namespace DesignPatternDemo.CreationalPatterns.Factories4 {
    public interface IHotDrink {
        void Consume ();
    }

    internal class Tea : IHotDrink {
        public void Consume () {
            System.Console.WriteLine ("This tea is nice bu I'd prefer it with milk.");
        }

    }

    internal class Coffee : IHotDrink {
        public void Consume () {
            System.Console.WriteLine ("This coffee is sensational");
        }
    }

    public interface IHotDrinkFactory {
        IHotDrink Prepare (int amount);
    }

    internal class TeaFactory : IHotDrinkFactory {
        public IHotDrink Prepare (int amount) {
            System.Console.WriteLine ($"Put in a tea bag.boil water, pour {amount} ml,add lemon,enjoy");
            return new Tea ();
        }
    }

    internal class CoffeeFactory : IHotDrinkFactory {
        public IHotDrink Prepare (int amount) {
            System.Console.WriteLine ($"Grind some beans,boil water, pour {amount} ml, add cream and suger, enjoy");
            return new Coffee ();
        }
    }

    public class HotDrinkMachine {
        private List<Tuple<string, IHotDrinkFactory>> factories =
            new List<Tuple<string, IHotDrinkFactory>> ();
        public HotDrinkMachine () {
            foreach (var t in typeof (HotDrinkMachine).Assembly.GetTypes ()) {
                if (typeof (IHotDrinkFactory).IsAssignableFrom (t) && !t.IsInterface) {
                    factories.Add (Tuple.Create (
                        t.Name.Replace ("Factory", string.Empty),
                        (IHotDrinkFactory) Activator.CreateInstance (t)
                    ));
                }
            }
        }

        public IHotDrink MakeDrink () {
            WriteLine ("Available drinks:");
            for (var index = 0; index < factories.Count; index++) {
                var tuple = factories[index];
                WriteLine ($"{index}:{tuple.Item1}");
            }
            while (true) {
                string s;
                if ((s = ReadLine ()) != null &&
                    int.TryParse (s, out int i) &&
                    i > 0 &&
                    i < factories.Count) {
                    WriteLine ("Specify amount: ");
                    s = ReadLine ();
                    if (s != null &&
                        int.TryParse (s, out int amount) &&
                        amount > 0) {
                        return factories[i].Item2.Prepare (amount);
                    }
                }
                System.Console.WriteLine ("Incorrect input ,try again!!");
            }

        }

    }

    public class AbstractFactoryDemo {
        public static void Show () {
            var machine = new HotDrinkMachine ();
            var drink = machine.MakeDrink ();
            drink.Consume ();
        }
    }
}