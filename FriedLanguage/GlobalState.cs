using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriedLanguage
{
    public static class GlobalState
    {
        private static bool isInConstructor = false;
        public static void EnterConstructor()
        { 
            isInConstructor = true;
        }
        public static void ExitConstructor()
        {
            isInConstructor = false;
        }
        public static bool IsInConstructor()
        {
            return isInConstructor;
        }
    }
}
