using System.Collections.Generic;

namespace FriedLang.NativeLibraries
{
    public partial class IO : LanguageExtention
    {
        public override List<FlangMethod> InjectMethods()
        {
            List<FlangMethod> methods = new()
            {
                new FlangMethod("print", Global.Print, "message"),

                new FlangMethod("read", Global.Read)
            };

            return methods;
        }
        public override List<FlangClass> InjectClassus()
        {
            List<FlangClass> classes = new List<FlangClass>();

            var fileClass = new FlangClass("File", extends: false,
            methods: new FlangMethod[]
            {
                    new FlangMethod("write",File.Write,ClassOptions.Static,"path","content"),
                    new FlangMethod("read",File.Read,ClassOptions.Static,"path"),
                    new FlangMethod("exists",File.Exists,ClassOptions.Static,"path"),
            });

            classes.Add(fileClass);
            return classes;
        }
    }
}
