﻿using FriedLanguage.BuiltinType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriedLanguage.Models.Parsing.Nodes
{
    internal class ClassPropDefinitionNode : SyntaxNode
    {
        public SyntaxToken Name { get; }
        public SyntaxNode Expression { get; }
        public bool IsStatic { get; }

        public ClassPropDefinitionNode(SyntaxToken name, SyntaxNode expr, bool isStatic) : base(name.Position, expr.EndPosition)
        {
            this.Name = name;
            this.Expression = expr;
            this.IsStatic = isStatic;
        }

        public override NodeType Type => NodeType.ClassPropertyDefinition;

        public override FValue Evaluate(Scope scope)
        {
            throw new NotImplementedException("This should not be called!");
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return new TokenNode(Name);
            yield return Expression;
        }
    }
}
