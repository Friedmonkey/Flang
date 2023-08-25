using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlTypes;
using System.IO;
using System.Threading.Tasks;
using FriedLang;
using FriedLanguage;
using FriedLanguage.BuiltinType;
using FriedLang.NativeLibraries;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace FriedLanguageConsole
{
	public class webIO : IO
	{
		public override void Intercept()
		{
			InterReplaceMethod("print",PrintBlue);
			InterRemoveClass("File");
		}
		public static FValue PrintBlue(Scope scope, List<FValue> arguments)
		{
            Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine(arguments.First().SpagToCsString());
            Console.ResetColor();
			return arguments.First();
		}
	}
    //			//class strict printer
    //{
    //	prop text = "a";
    //	func ctor(txt)
    //	{
    //		self.text = txt;
    //	}
    //	func write()
    //	{
    //		print(self.text);
    //	}
    //}

    //var p = new printer("Hiiiii :3");
    //p.write();
    //p.write();
    //p.write();
    //p.text = "Something else :p"
    ////p.lollers = "oop xdxdxd" //can add vars that dont exist
    //p.write();
    //p.write();
    ////p.text = p.lollers;
    //p.write();
    //p.write();

    //class extend String
    //{
    //	prop static empty = "";
    //}
            //import native io;
            //import native lang;

            //class person
            //{
            //    string name = "";
            //    person(nam)
            //    {
            //        var name = "lol"; 
            //        self.name = nam;
            //    }
            //    void hello()
            //    {
            //        print("Hello my name is "+self.name);
            //    }
            //}

            //var p = new person("jhonn");
            //p.hello();
            //return p;
    ///switch case
    ///goto statement
    //dll import/interopt?
    ///add class
    ///add var (_GET["url"])
    ///add double
    ///add bool
    ///add operators as keyword = is || or && and
    //add tenery opearot ? :
    //add nullcoleancane opator ??
    ///add unmatch keyword to exit if and go to else or exit case and go to default
    //add declass keyword?
    internal class Program
    {
        static void Main(string[] args)
        {
            FLang fLang = new FLang();
			fLang.ImportNative<IO>("io");
			fLang.ImportNative<Lang>("lang");
			//fLang.ImportNative<Popups>("pop");
			//fLang.ImportNative<Async>("async");

			fLang.AddVariable("pi",Math.PI);
            //fLang.AddMethod(new FlangMethod("printNormal",IO.Global.Print,"msg"));

            //MessageBox.Show("hi","popup",MessageBoxButtons.YesNoCancel,MessageBoxIcon.None);
            string code = """
            import native io;
            //import native pop;

            class popup
            {
                string message = "";
                string title = "popup";
                string buttonType = "YesNoCancel";
                string iconType = "None";
                popup(msg)
                {
                    self.message = msg;
                }
                string show()
                {
                    string str = "Nothing";
                    <{
                        if (Enum.TryParse(context.getSelfStr("buttonType"), true, out MessageBoxButtons btype))
                        {
                            if (Enum.TryParse(context.getSelfStr("iconType"), true, out MessageBoxIcon itype))
                            {
                                var result = MessageBox.Show(context.getSelfStr("message"),context.getSelfStr("title"),btype,itype);
                                context.setStr("str",result.ToString());
                            }
                        }
                    }>
                    return str;
                }
            }
            
            var p = new popup("Do you want to date me?");
            p.type = "Ok";
            p.title = "importand";
            p.buttonType = "YesNo";
            p.iconType = "Warning";
            var result = p.show();
            if (result == "Yes")
            {
                print("yayyyy :3");
            }
            else
            {
                print(":(");
            }
""";


            if (args.Length > 0)
                code = File.ReadAllText(args[0]);


            var output = fLang.RunCode(code);
			if (output != null) 
			{
				if (output is IEnumerable<object> objs)
					Console.WriteLine("output was:" + string.Join(", ", objs));
				else
					Console.WriteLine("output was:" + output);
			}
            Console.ReadLine(); //so console doest exit
        }
    }
}
//import native io;
//import native lang;

//class strict person
//            {
//                string name; //works
//string name //doest work
//                string name = "aaa"; //works

//void cctor

//                func ctor(nam, ag)
//                {
//    self.name = nam;
//    self.age = ag;
//}
//func murder()
//{
//    self.alive = false;
//}
//            }

//            var p = new person("Jhonny", 56);
//p.murder();
//print(p.name + " Died at the age of " + p.age);
//p.name = "Dead Jhonny";
//return p;

////bool test = false;
////test = true;
////return test
