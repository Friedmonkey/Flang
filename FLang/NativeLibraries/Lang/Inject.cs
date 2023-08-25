using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriedLang.NativeLibraries
{
    public partial class Lang : LanguageExtention
    {
        public override List<FlangClass> InjectClassus()
        {
            List<FlangClass> classes = new List<FlangClass>();

            var stringClass = new FlangClass("String", extends: true,
            methods: new FlangMethod[]
            {
                new FlangMethod("count",String.Count,ClassOptions.Extend),
                new FlangMethod("toint",String.ToInt,ClassOptions.Extend,"default"),
                new FlangMethod("split",String.Split,ClassOptions.Extend,"split"),
                new FlangMethod("replace",String.Replace,ClassOptions.Extend,"replacing","replacement")
            });
            var intClass = new FlangClass("Int", extends: true,
            methods: new FlangMethod[]
            {
                new FlangMethod("tostring",Int.ToString,ClassOptions.Extend),
            });
            var listClass = new FlangClass("List", extends: true,
            methods: new FlangMethod[]
            {
                new FlangMethod("first",List.First,ClassOptions.Extend),
                new FlangMethod("last",List.Last,ClassOptions.Extend),
                new FlangMethod("add",List.Add,ClassOptions.Extend,"item"),
                new FlangMethod("remove",List.Remove,ClassOptions.Extend,"item"),
                new FlangMethod("removeAt",List.RemoveAt,ClassOptions.Extend,"index"),
                new FlangMethod("contains",List.Contains,ClassOptions.Extend,"item"),
                new FlangMethod("count",List.Count,ClassOptions.Extend),
            });
            var dictClass = new FlangClass("Dict", extends: true,
            methods: new FlangMethod[]
            {
                new FlangMethod("first",Dict.First,ClassOptions.Extend),
                new FlangMethod("last",Dict.Last,ClassOptions.Extend),
                new FlangMethod("add",Dict.Add,ClassOptions.Extend,"key","value"),
                new FlangMethod("get",Dict.Get,ClassOptions.Extend,"key"),
                new FlangMethod("remove",Dict.Remove,ClassOptions.Extend,"key"),
                new FlangMethod("removeAt",Dict.RemoveAt,ClassOptions.Extend,"index"),
                new FlangMethod("containsKey",Dict.ContainsKey,ClassOptions.Extend,"key"),
                new FlangMethod("containsValue",Dict.ContainsValue,ClassOptions.Extend,"value"),
                new FlangMethod("count",Dict.Count,ClassOptions.Extend),
            });

            classes.Add(stringClass);
            classes.Add(intClass);
            classes.Add(listClass);
            classes.Add(dictClass);
            return classes;
        }
    }
}
