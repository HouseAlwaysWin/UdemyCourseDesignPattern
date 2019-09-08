namespace DesignPatternDemo.CreationalPatterns.Builder {
    public class Person {
        public string Name { get; set; }
        public string Position { get; set; }

        public class Builder : PersonJobBuilder<Builder> {

        }

        public static Builder New => new Builder ();

        public override string ToString () {
            return $"{nameof(Name)}:{Name}, {nameof(Position)}:{Position}";
        }
    }

    public abstract class PersonBuilder {
        protected Person person = new Person ();
        public Person Build () {
            return person;
        }
    }

    public class PersionInfoBuilder<SELF>:
        PersonBuilder where SELF : PersionInfoBuilder<SELF> {
            public SELF Called (string name) {
                person.Name = name;
                return (SELF) this;
            }
        }

    public class PersonJobBuilder<SELF>:
        PersionInfoBuilder<PersonJobBuilder<SELF>>
        where SELF : PersonJobBuilder<SELF> {
            public SELF WorksAsA (string position) {
                person.Position = position;
                return (SELF) this;
            }
        }
    public static class FluentBuilderDemo {
        public static void Show () {
            var martin = Person.New.Called ("martin").WorksAsA ("programmer").Build ();
        }
    }
}