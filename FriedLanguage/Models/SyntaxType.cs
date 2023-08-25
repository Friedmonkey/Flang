using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriedLanguage.Models
{
    public enum SyntaxType
    {
        Semicolon,                  //  ;
        Keyword,                    //  KEYWORD
        Identifier,                 //  IDENTIFIER
        Equals,                     //  =
        EqualsEquals,               //  ==
        Arrow,                      //  =>
        AndAnd,                     //  &&
        OrOr,                       //  ||
        LessThan,                   //  <
        GreaterThan,                //  >
        GreaterThanEqu,             //  >=
        LessThanEqu,                //  <=

        Plus, Minus,                //  +    -
        PlusEqu, MinusEqu,          //  +=  -+
        PlusPlus, MinusMinus,       //  ++  --
                                    
        Mod, Mul, Div,              //  %   *   /
        ModEqu, MulEqu, DivEqu,     //  %= *=   /=
        Idx,                        //  #

        Pow,                        //  ^
        Dot,                        //  .
        LParen,                     //  (
        RParen,                     //  )
        Int,                        //  [0-9]
        Float,                      //  [0-9].?[0-9]
        CsLit,                      //  <{cs code here}>
        String,                     //  "\w*"
        LSqBracket,                 //  [
        RSqBracket,                 //  ]
        LBraces,                    //  {
        RBraces,                    //  }
        Bang,                       //  !
        Question,                   //  ?
        //NullExpr,                   //  ??
        EOF,                        //  \0
        BadToken,                   //  BAD TOKEN
        Comma,                      //  ,
        Colon,                      //  :
        BangEquals,                 //  !=
    }
}
