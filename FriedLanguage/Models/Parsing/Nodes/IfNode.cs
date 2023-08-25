using FriedLanguage.BuiltinType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriedLanguage.Models.Parsing.Nodes
{
    internal class IfNode : SyntaxNode
    {
        public IfNode(SyntaxToken startTok) : base(startTok.Position, startTok.Position) { } // We expect the parser to properly define the endpos

        public List<(SyntaxNode cond, SyntaxNode block)> Conditions { get; private set; } = new();

        public override NodeType Type => NodeType.If;

        public override FValue Evaluate(Scope scope)
        {
            foreach ((SyntaxNode cond, SyntaxNode block) in Conditions)
            {
                var condRes = cond.Evaluate(scope);

                if (condRes.IsTruthy())
                {
                    var newScope = new Scope(scope, StartPosition);
                    var eval = block.Evaluate(newScope);
                    if (newScope.State != ScopeState.ShouldUnmatch)
                        return eval;
                }
            }

            return FValue.Null;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            foreach (var (cond, block) in Conditions)
            {
                yield return cond;
                yield return block;
            }
        }

        internal void AddCase(SyntaxNode cond, SyntaxNode block)
        {
            Conditions.Add((cond, block));
        }

        public override string ToString()
        {
            return "IfNode:";
        }
    }
}
