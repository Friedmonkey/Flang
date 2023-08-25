using FriedLanguage.BuiltinType;
using FriedLanguage.Models;
using FriedLanguage.Models.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriedLanguage.BuiltinType
{
    public class FFunction : FBaseFunction
    {
        public override FBuiltinType BuiltinName => FBuiltinType.Function;
        public string FunctionName { get; set; }
        public SyntaxNode Callback { get; set; }
        public Scope DefiningScope { get; set; }


        public FFunction(Scope definingScope, string functionName, List<string> args, SyntaxNode callback)
        {
            DefiningScope = definingScope;
            FunctionName = functionName;
            ExpectedArgs = args;
            Callback = callback;

            // If the scope is the global scope, we need to clone it as otherwise we would have a reference to the global scope
            // and any changes to the scope would be reflected in the global scope
            if (DefiningScope.ParentScope == null)
            {
                DefiningScope = DefiningScope.Clone();
            }
        }

        public override FValue Call(Scope scope, List<FValue> args,SyntaxToken token = default)
        {
            if (args.Count != ExpectedArgs.Count) throw new Exception(FunctionName + " expected " + ExpectedArgs.Count + " arguments. (" + string.Join(", ", ExpectedArgs) + ")");

            Scope funcScope = new(DefiningScope, DefiningScope.CreatedPosition);

            for (int i = 0; i < ExpectedArgs.Count; i++)
            {
                funcScope.Set(ExpectedArgs[i], args[i]);
            }

            Callback.Evaluate(funcScope);
            scope.SetState(ScopeState.None);

            return funcScope.ReturnValue;
        }

        public override bool IsTruthy()
        {
            return true;
        }
    }
}
