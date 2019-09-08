namespace DesignPatternDemo.CreationalPatterns.Builder2 {

    public class Person {
        public string StreetAddress, Postcode, City;
        public string CompanyName, Position;

        public int AnnualIncome;

        public override string ToString () {
            return $"{nameof(StreetAddress)}:{StreetAddress}, {nameof(Postcode)}:{Postcode},{nameof(City)}:{City},{nameof(CompanyName)}:{CompanyName},{nameof(Position)}:{Position},{nameof(AnnualIncome)}:{AnnualIncome}";
        }
    }

    public class PersonBuilder {
        protected Person person = new Person ();

        public PersonJobBuilder Works => new PersonJobBuilder (person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder (person);

        public static implicit operator Person (PersonBuilder pb) {
            return pb.person;
        }
    }

    public class PersonAddressBuilder : PersonBuilder {
        public PersonAddressBuilder (Person person) {
            this.person = person;
        }

        public PersonAddressBuilder At (string streetAddress) {
            person.StreetAddress = streetAddress;
            return this;
        }

        public PersonAddressBuilder In (string city) {
            person.City = city;
            return this;
        }

        public PersonAddressBuilder WithPostCode (string postcode) {
            person.Postcode = postcode;
            return this;
        }

    }

    public class PersonJobBuilder : PersonBuilder {
        public PersonJobBuilder (Person person) {
            this.person = person;
        }

        public PersonJobBuilder At (string companyName) {
            person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA (string position) {
            person.Position = position;
            return this;
        }

        public PersonJobBuilder Earning (int amount) {
            person.AnnualIncome = amount;
            return this;
        }
    }

    public static class FacetedBuilderDemo {
        public static void Show () {
            var pb = new PersonBuilder ();
            Person person = pb
                .Lives.At ("Xitun road 5")
                .In ("Taiching").WithPostCode ("407")
                .Works.At ("")
                .AsA ("Engineer")
                .Earning (122334);
            System.Console.WriteLine (person);
        }
    }
}