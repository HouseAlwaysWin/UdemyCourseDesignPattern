using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
namespace DesignPatternDemo.CreationalPatterns.Builder3 {

    public class Code {
        public string ClassName { get; set; }
        public Dictionary<string, string> Fields = new Dictionary<string, string> ();

        public override string ToString () {
            StringBuilder strBuilder = new StringBuilder ();
            strBuilder.Append ($"public class {ClassName}\n");
            strBuilder.Append ("{\n");
            foreach (var f in Fields) {
                strBuilder.Append ($"  public {f.Key} {f.Value};\n");
            }
            strBuilder.Append ("}\n");

            return strBuilder.ToString ();
        }
    }
    public class CodeBuilder {
        protected Code code = new Code ();
        protected CodeBuilder (Code code) { this.code = code; }
        public CodeBuilder (string className) {
            this.code.ClassName = className;
        }

        public CodeBuilder AddField (string fieldName, string fieldType) {
            code.Fields.Add (fieldType, fieldName);
            return this;
        }
        public override string ToString () {
            return this.code.ToString ();
        }

    }

    public static class CodeBuilderDemo {
        public static void Show () {

            var cb = new CodeBuilder ("Person").AddField ("Name", "string").AddField ("Age", "int");
            System.Console.WriteLine (cb);
        }
    }
}