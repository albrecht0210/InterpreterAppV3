using InterpreterAppV3.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InterpreterAppV3.Library.Analyze.Helper
{
    public class LexerHelper
    {
        public static bool RegexMatcher(string pattern, string text)
        {
            Regex regex = new Regex(pattern);

            return regex.IsMatch(text);
        }

        public static bool IsReserveKeyword(string text)
        {
            List<string> keywords = new List<string>
            {
                "BEGIN", "END", "CODE", "IF", "ELSE",
                "WHILE", "DISPLAY", "SCAN", "AND", "OR", "NOT",
                "INT", "FLOAT", "CHAR", "BOOL"
            };

            return keywords.Contains(text);
        }

        public static Symbol GetReserveKeywordSymbol(string text)
        {
            if (!IsReserveKeyword(text))
                throw new Exception($"{text} is not a reserve keyword");

            Dictionary<string, Symbol> keywords = new Dictionary<string, Symbol>
            {
                {"BEGIN", Symbol.BEGIN}, {"END", Symbol.END}, {"CODE", Symbol.CODE}, {"IF", Symbol.IF},
                {"ELSE", Symbol.ELSE}, {"WHILE", Symbol.WHILE}, {"DISPLAY", Symbol.DISPLAY}, {"SCAN", Symbol.SCAN},
                {"AND", Symbol.AND}, {"OR", Symbol.OR}, {"NOT", Symbol.NOT}, {"INT", Symbol.INT},
                {"FLOAT", Symbol.FLOAT}, {"CHAR", Symbol.CHAR}, {"BOOL", Symbol.BOOL}
            };

            return keywords[text];
        }
    }
}
