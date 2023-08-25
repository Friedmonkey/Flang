﻿using FriedLanguage.BuiltinType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriedLanguage.Models.Parsing.Nodes
{
	internal class CaseNode : SyntaxNode
	{
		public override NodeType Type => NodeType.Label;
		public string Name { get; protected set; }
		public int Position { get; protected set; }
		public SyntaxNode Expr { get; protected set; }


		public CaseNode(SyntaxToken labelToken, string name, int pos, SyntaxNode expr) : base(labelToken.Position, labelToken.EndPosition)
		{
			this.Name = name;
			this.Position = pos;
			Expr = expr;
		}

		public override FValue Evaluate(Scope scope)
		{
			scope.SetJumpPosCreatedPos(scope.CreatedPosition);
			scope.Set(Name, new FLabel(Position));
			return FValue.Null;
		}

		public override IEnumerable<SyntaxNode> GetChildren()
		{
			return Enumerable.Empty<SyntaxNode>();
		}

		public override string ToString()
		{
			return "CaseNode:";
		}
	}
}
