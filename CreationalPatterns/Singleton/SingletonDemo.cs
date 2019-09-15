using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autofac;
using MoreLinq;
using NUnit.Framework;

namespace DesignPatternDemo.CreationalPatterns {
    public interface IDatabase {
        int GetPopulation (string name);
    }

    public class ConfigurableRecordFinder {
        private IDatabase database;
        public ConfigurableRecordFinder (IDatabase database) {
            this.database = database ??
                throw new System.Exception ();
        }

        public int GetTotalPopulation (IEnumerable<string> names) {
            int result = 0;
            foreach (var name in names) {
                result += database.GetPopulation (name);
            }
            return result;
        }
    }

    public class DummyDatabase : IDatabase {
        public int GetPopulation (string name) {
            return new Dictionary<string, int> {
                ["alpha"] = 1,
                ["beta"] = 2,
                ["gamma"] = 3
            }[name];
        }
    }

    public class SingletonDatabase : IDatabase {
        private Dictionary<string, int> capitals;
        private static int instanceCount;
        public static int Count => instanceCount;

        private SingletonDatabase () {
            instanceCount++;
            System.Console.WriteLine ("Initinalizing database");

            capitals = File.ReadAllLines (
                    Path.Combine (
                        new FileInfo (typeof (IDatabase).Assembly.Location).DirectoryName, "capitals.txt"
                    )
                )
                .Batch (2).ToDictionary (
                    list => list.ElementAt (0).Trim (),
                    list => int.Parse (list.ElementAt (1))
                );
        }

        private static Lazy<SingletonDatabase> instance => new Lazy<SingletonDatabase> ();
        public static SingletonDatabase Instance => instance.Value;

        public int GetPopulation (string name) {
            return capitals[name];
        }
    }

    public class OrdinaryDatabase : IDatabase {
        private Dictionary<string, int> capitals;

        private OrdinaryDatabase () {
            System.Console.WriteLine ("Initinalizing database");

            capitals = File.ReadAllLines (
                    Path.Combine (
                        new FileInfo (typeof (IDatabase).Assembly.Location).DirectoryName, "capitals.txt"
                    )
                )
                .Batch (2).ToDictionary (
                    list => list.ElementAt (0).Trim (),
                    list => int.Parse (list.ElementAt (1))
                );
        }

        public int GetPopulation (string name) {
            return capitals[name];
        }
    }

    [TestFixture]
    public class SingletonTests {
        [Test]
        public void IsSingletonTest () {
            var db = SingletonDatabase.Instance;
            var db2 = SingletonDatabase.Instance;
            Assert.That (db, Is.SameAs (db2));
            Assert.That (SingletonDatabase.Count, Is.EqualTo (1));
        }

        [Test]
        public void ConfigurablePopulationTest () {
            var rf = new ConfigurableRecordFinder (new DummyDatabase ());
            var names = new [] { "alpha", "gamma" };
            int tp = rf.GetTotalPopulation (names);
            Assert.That (tp, Is.EqualTo (4));
        }

        [Test]
        public void DIPopulationTest () {
            var cb = new ContainerBuilder ();
            cb.RegisterType<OrdinaryDatabase> ().As<IDatabase> ()
                .SingleInstance ();
            cb.RegisterType<ConfigurableRecordFinder> ();
            using (var c = cb.Build ()) {
                var rf = c.Resolve<ConfigurableRecordFinder> ();
            }
        }
    }

    public class SingletonDemo {
        public static void Show () {
            var db = SingletonDatabase.Instance;
            System.Console.WriteLine (db.GetPopulation ("Tokyo"));
        }
    }
}