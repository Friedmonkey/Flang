﻿using FriedLanguage.BuiltinType;
using FriedLanguage.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriedLanguage.Models.Parsing.Nodes
{
    internal class DotNode : SyntaxNode
    {
        public DotNode(SyntaxNode callNode) : base(callNode.StartPosition, -1) // ending pos is overwritten
        {
            CallNode = callNode;
        }

        public SyntaxNode CallNode { get; }
        public List<SyntaxNode> NextNodes { get; internal set; } = new();
        public override int EndPosition => NextNodes.GetEndingPosition(CallNode.EndPosition);

        public override NodeType Type => NodeType.Dot;

        public override FValue Evaluate(Scope scope)
        {
            var currentValue = CallNode.Evaluate(scope);

            foreach (var node in NextNodes)
            {
                if (node is IdentifierNode rvn)
                {
                    var ident = rvn.Token;
                    currentValue = currentValue.Dot(new FString(ident.Text),rvn.Token,scope);
                    //fried change fried monkey
                }
                else if (node is AssignVariableNode avn)
                {
                    var ident = avn.Ident;
                    return currentValue.DotAssignment(new FString(ident.Text), avn.Expr.Evaluate(scope),scope);
                }
                else if (node is CallNode cn)
                {
                    if (cn.ToCallNode is IdentifierNode cnIdentNode)
                    {
                        var ident = cnIdentNode.Token;
                        var lhs = currentValue.Dot(new FString(ident.Text),ident,scope);

                        var args = cn.EvaluateArgs(scope);
                        if (lhs is FNativeFunction nfunc && nfunc.IsClassInstanceMethod)
                        {
                            var idxOfThis = nfunc.ExpectedArgs.IndexOf("this");

                            if (idxOfThis != -1)
                                args.Insert(idxOfThis, currentValue);
                        }
                        else if (lhs is FBaseFunction func && func.IsClassInstanceMethod)
                        {
                            var idxOfSelf = func.ExpectedArgs.IndexOf("self");
                            var idxOfThis = func.ExpectedArgs.IndexOf("this");

                            if (idxOfSelf != -1) 
                                args.Insert(idxOfSelf, currentValue);

                            if (idxOfThis != -1)
                                args.Insert(idxOfThis, currentValue);
                        }

                        currentValue = lhs.Call(scope, args,ident);


						if (cn.IndexerIndex != null)
						{
							var index = cn.IndexerIndex.Evaluate(scope);
							if (index == null)
								throw new Exception("Couldnt parse index");

							currentValue = currentValue.GetIndex(index);
						}

					}
                    else
                    {
                        throw new Exception("Tried to call a non identifier in dot node stack.");
                    }
                }
                else
                {
                    throw new Exception("Unexpected node in dot node stack!");
                }
            }

            return currentValue;
        }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return CallNode;

            foreach (var node in NextNodes) yield return node;
        }

        public override string ToString()
        {
            return "DotNode:";
        }

        public DotNode Clone()
        {
            var dn = new DotNode(CallNode);
            dn.NextNodes = this.NextNodes.ToList(); return dn;
        }
    }
}
