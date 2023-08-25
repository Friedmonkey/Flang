using FriedLanguage.BuiltinType;
using FriedLanguage.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace FriedLanguage.Models.Parsing.Nodes
{
    internal class CsLiteral : SyntaxNode
    {
        private SyntaxToken CsLit;
            
        public CsLiteral(SyntaxToken csLit)
            : base(csLit.Position, csLit.EndPosition) // either nametoken start, args start or finally block start
        {
            this.CsLit = csLit;
        }

        public override NodeType Type => NodeType.CsBlock;

        public override FValue Evaluate(Scope scope)
        {
            //var f = new FFunction(scope, start?.Text ?? "<anonymous>", args.Select((v) => v.Text).ToList(), block);
            //if (start != null)
            //{ 
            //    if (isOverride)
            //        scope.SetAdmin(start.Value.Text, f);
            //    else
            //        scope.Set(start.Value.Text, f);
            //}
            //return f;


            ///
            /// 
            ///
            Context.scope = scope;
            string code = (string)CsLit.Value;

            string fullCode = """
            public class ScriptedClass
            {
                public FriedLanguage.Models.Context context {get;set;}
                public ScriptedClass()
                {
                    context = new FriedLanguage.Models.Context();
""" + code+ """
                }
            }
            
            return (new ScriptedClass()).context;
""";


            //ScriptOptions scriptOptions = ScriptOptions.Default.AddReferences("FriedLanguage");
            var scriptOptions = ScriptOptions.Default
                .WithReferences(AppDomain.CurrentDomain.GetAssemblies())
                .AddImports("System.Windows.Forms")
                .AddImports("FriedLanguage.BuiltinType")
                .AddImports("System");

            ScriptState<object> scriptState = CSharpScript.RunAsync(fullCode, scriptOptions).Result;

            if (scriptState.ReturnValue != null && !string.IsNullOrEmpty(scriptState.ReturnValue.ToString()))
            {
                var context = (scriptState.ReturnValue as Context);
                scope.Table = Context.scope.Table;
            }

            return FValue.Null;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            return null;
            //if (start != null) yield return new TokenNode(start.Value);
            //foreach (var t in args) yield return new TokenNode(t);
            //yield return block;
        }
    }
}
