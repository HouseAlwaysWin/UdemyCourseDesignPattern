using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DesignPatternDemo.CreationalPatterns.Prototype {

    public static class ExtensionMethods {
        public static T DeepCopy<T> (this T self) {
            using (var stream = new MemoryStream ()) {
                var formatter = new BinaryFormatter ();
                formatter.Serialize (stream, self);
                stream.Seek (0, SeekOrigin.Begin);
                object copy = formatter.Deserialize (stream);
                return (T) copy;
            }
        }
    }

    [Serializable]
    public class Person {
        public string Names;
        public Address Address;

        public override string ToString () {
            return $"{nameof(Names)}:{string.Join(" ",Names)}, {nameof(Address)}:{Address}";
        }
    }

    [Serializable]
    public class Address {

        public string StreetName;
        public int HouseNumber;

        public Address (string streetName, int houseNumber) {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public override string ToString () {
            return $"{nameof(StreetName)}:{StreetName},{nameof(HouseNumber)}:{HouseNumber}";
        }
    }

    public static class SerializationDeepCopyDemo {
        public static void Show () {
            Person p = new Person () {
                Names = "marin",
                Address = new Address ("Longman", 123)
            };

            Person p2 = p.DeepCopy ();
            p2.Names = "CJ";
            p2.Address.StreetName = "where";
            System.Console.WriteLine (p);
            System.Console.WriteLine (p2);
        }
    }
}