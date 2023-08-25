using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriedLanguage.Models
{
#warning keywords are here
    public static class SyntaxFacts
    {
        public static void ClassifyIdentifierAsKeyword(ref SyntaxToken token)
        {
            if (token.Text.ToString() 
			#region existing
                                       is "import" or "native" or "export" 

                                       or "return" or "continue" or "break" 

                                       or "if" or "elseif" or "else" 

                                       or "for" or "while" 
                                       or "repeat" or "times" 

                                       or "func" or "var" or "class" or "prop" 
                                       or "op" 

                                       or "static" or "const" 
                                       or "new" 
									   //"self" is a unofficial keyword
			#endregion

			#region  mine
									   //"this" its not an officiel keyword but its used for extending

									   or "memory" 

									   //or "int"
									   //or "float"
									   //or "double"
									   //or "long"
									   //or "string"
									   //or "bool"
									   //or "object"
									   //or "list"
									   //or "dictionary"

                                       or "extend"
                                       
                                       or "try" or "catch" 
                                       or "throw"

                                       or "label" or "goto"
                                       
                                       or "switch" or "case"
                                       or "default"

									   or "strict" or "override"

                                       or "foreach" or "in"

                                       or "keyword" or "disable" or "enable"

                                       or "unmatch"

            #endregion
                                       )
			{
                token.Type = SyntaxType.Keyword;
            }
        }
        public static void ClassifyIdentifierAsLogical(ref SyntaxToken token)
        {
            var str = token.Text.ToString();
            switch (str)
            {
                case "becomes":
                    token.Type = SyntaxType.Equals;
                    break;
                case "is":
                    token.Type = SyntaxType.EqualsEquals;
                    break;
                case "or":
                    token.Type = SyntaxType.OrOr;
                    break;
                case "and":
                    token.Type = SyntaxType.AndAnd;
                    break;
                case "not":
                    token.Type = SyntaxType.Bang;
                    break;
                case "then":
                    token.Type = SyntaxType.LBraces;
                    break;
                case "end":
                    token.Type = SyntaxType.RBraces;
                    break;
                default:
                    break;
            }
        }
    }
}
