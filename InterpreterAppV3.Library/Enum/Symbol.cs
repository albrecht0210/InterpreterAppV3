using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterAppV3.Library.Enum
{
    public enum Symbol
    {
        // Keyword
        BEGIN, END, CODE, IF, ELSE, WHILE, DISPLAY, SCAN, AND, OR, NOT,

        // Data Type
        INT, FLOAT, CHAR, BOOL,

        // Identifier
        IDENTIFIER,

        // Literal
        NUMBERLITERAL, WORDLITERAL, CHARLITERAL,

        // Symbol
        COMMA, EQUAL, COLON, QUOTE, APOSTROPHE, POUND, DOLLAR, AMPERSAND,

        // Arithmetic Operators
        OPENPARENTHESIS, CLOSEPARENTHESIS, STAR, SLASH, PERCENT, PLUS, MINUS,

        // Comparison Operators
        GREATERTHAN, LESSTHAN, GREATEREQUAL, LESSEQUAL, EQUALTO, NOTEQUAL,

        // Other
        NEWLINE, ESCAPE, ERROR, ENDOFFILE
    }
}
