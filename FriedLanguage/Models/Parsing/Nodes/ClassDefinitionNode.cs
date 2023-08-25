using FriedLanguage.BuiltinType;
using FriedLanguage.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FriedLanguage.Models.Parsing.Nodes
{
    internal class ClassDefinitionNode : SyntaxNode
    {
        private SyntaxToken className;
        private IEnumerable<SyntaxNode> body;
        private readonly bool strict;
        private readonly bool extend;

        public ClassDefinitionNode(SyntaxToken className, IEnumerable<SyntaxNode> body, bool strict, bool extend) : base(className.Position, body.GetEndingPosition(className.EndPosition))
        {
            this.className = className;
            this.body = body;
            this.strict = strict;
            this.extend = extend;
        }

        public override NodeType Type => NodeType.ClassDefinition;

        public override FValue Evaluate(Scope scope)
        {

            if (this.extend)
            {
                var clas = scope.Get(className.Text);
                if (clas is FClass fclass)
                {
                    foreach (var bodyNode in body)
                    {
                        if (bodyNode is ClassFunctionDefinitionNode cfdn)
                        {
                            var funcRaw = cfdn.Evaluate(scope);
                            if (funcRaw is not FFunction func) throw new Exception("Expected ClassFunctionDefinitionNode to return SFunction");

                            if (func.IsClassInstanceMethod)
                            {
                                if (func.ExpectedArgs.IndexOf("self") == -1) func.ExpectedArgs.Insert(0, "self");

                                fclass.InstanceBaseTable.Add((func.FunctionName, func));
                            }
                            else
                            {
                                fclass.StaticTable.Add((func.FunctionName, func));
                            }
                        }
                        else if (bodyNode is ClassPropDefinitionNode cpdn)
                        {
                            var val = cpdn.Expression.Evaluate(scope);

                            if (!cpdn.IsStatic)
                            {
                                fclass.InstanceBaseTable.Add((cpdn.Name.Text, val));
                            }
                            else
                            {
                                fclass.StaticTable.Add((cpdn.Name.Text, val));
                            }
                        }
                        else
                        {
                            throw new Exception("Unexpected node in extended class");
                        }
                    }
                }
                else 
                {
                    throw new Exception("Trying to extend class that doest exist");
                }
                scope.SetAdmin(className.Text, fclass);
                return fclass;
            }
            var @class = new FClass();
            @class.Name = className.Text;
            @class.Strict = strict;

            foreach (var bodyNode in body)
            {
                if (bodyNode is ClassFunctionDefinitionNode cfdn)
                {
                    var funcRaw = cfdn.Evaluate(scope);
                    if (funcRaw is not FFunction func) throw new Exception("Expected ClassFunctionDefinitionNode to return SFunction");

                    if (func.IsClassInstanceMethod)
                    {
                        if (func.ExpectedArgs.IndexOf("self") == -1) func.ExpectedArgs.Insert(0, "self");

                        @class.InstanceBaseTable.Add((func.FunctionName, func));
                    }
                    else
                    {
                        @class.StaticTable.Add((func.FunctionName, func));
                    }
                }
                else if (bodyNode is ClassPropDefinitionNode cpdn)
                {
                    var val = cpdn.Expression.Evaluate(scope);

                    //cpdn.copyMeta(ref val);
                    val.IsConstant = false;

                    if (!cpdn.IsStatic)
                    {
                        @class.InstanceBaseTable.Add((cpdn.Name.Text, val));
                    }
                    else
                    {
                        @class.StaticTable.Add((cpdn.Name.Text, val));
                    }
                }
                else
                {
                    throw new Exception("Unexpected node in class definition");
                }
            }

            scope.Set(className.Text, @class);
            return @class;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return new TokenNode(className);
            foreach (var n in body) yield return n;
        }
    }
}
