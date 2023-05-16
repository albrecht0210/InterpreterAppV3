using InterpreterAppV3.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterAppV3.Library.Analyze
{
    public class Token
    {
        public Token(Symbol symbol, string code_fragement, int line, int column)
        {
            Symbol = symbol;
            Code_Fragement = code_fragement;
            Line = line;
            Column = column;
        }

        public Symbol Symbol { get; }
        public string Code_Fragement { get; }
        public int Line { get; }
        public int Column { get; }

        public override string ToString()
        {
            return $"Token(Symbol: {Symbol}, Code: '{Code_Fragement}', Line: {Line}, Column: {Column})";
        }
    }
}
