using InterpreterAppV3.Library.Analyze.Helper;
using InterpreterAppV3.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterAppV3.Library.Analyze
{
    public class Lexer
    {
        private readonly string _code;
        private int _text_position, _text_line, _text_column;
        private bool _has_carriage_return;

        public Lexer(string code)
        {
            this._code = code;
            this._text_position = 0;
            this._text_line = this._text_column = 1;
            this._has_carriage_return = false;
        }

        private char Peek(int offset)
        {
            int index = _text_position + offset;

            if (index >= _code.Length)
                return '\0';
            return _code[index];
        }

        private char Current => Peek(0);

        private char LookAhead => Peek(1);

        private void Next(int offset = 1)
        {
            _text_position += offset;
            _text_column += offset;
        }

        private void NewLine()
        {
            _text_line++;
            Next();
            _text_column = 1;
            
        }

        public Token GetToken()
        {
            while (Current != '\0')
            {
                if (char.IsLetter(Current))
                    return GetReserveWordToken();

                if (char.IsDigit(Current))
                    return GetNumericToken();

                switch (Current)
                {
                    // Whitespaces
                    case ' ':
                    case '\t':
                        Next();
                        break;
                    case '\r':
                        Next();
                        break;
                    case '\n':
                        Token newline = new Token(Symbol.NEWLINE, "\\n", _text_line, _text_column - 1);
                        NewLine();
                        return newline;
                    // Comment
                    case '#':
                        while (Current != '\n' && Current != '\0')
                            Next();
                        break;
                    // Identifier
                    case '_':
                        return GetReserveWordToken();
                    // Literals
                    case '\'':
                        return GetCharacterToken();
                    case '"':
                        return GetBoolNStringToken();
                    case '.':
                        return GetNumericToken();
                    case '[':
                        return GetEscapeToken();
                    // Arithmetic Operators
                    case '*':
                        return GetSymbolToken(Symbol.STAR, Current.ToString(), 1);
                    case '/':
                        return GetSymbolToken(Symbol.SLASH, Current.ToString(), 1);
                    case '%':
                        return GetSymbolToken(Symbol.PERCENT, Current.ToString(), 1);
                    case '+':
                        return GetSymbolToken(Symbol.PLUS, Current.ToString(), 1);
                    case '-':
                        return GetSymbolToken(Symbol.MINUS, Current.ToString(), 1);
                    case '(':
                        return GetSymbolToken(Symbol.OPENPARENTHESIS, Current.ToString(), 1);
                    case ')':
                        return GetSymbolToken(Symbol.CLOSEPARENTHESIS, Current.ToString(), 1);
                    // Logical Operators
                    case '<':
                        if (LookAhead == '>')
                            return GetSymbolToken(Symbol.NOTEQUAL, "<>", 2);
                        if (LookAhead == '=')
                            return GetSymbolToken(Symbol.LESSEQUAL, "<=", 2);
                        return GetSymbolToken(Symbol.LESSTHAN, Current.ToString(), 1);
                    case '>':
                        if (LookAhead == '=')
                            return GetSymbolToken(Symbol.GREATEREQUAL, ">=", 2);
                        return GetSymbolToken(Symbol.GREATERTHAN, Current.ToString(), 1);
                    case '=':
                        if (LookAhead == '=')
                            return GetSymbolToken(Symbol.EQUALTO, "==", 2);
                        return GetSymbolToken(Symbol.EQUAL, Current.ToString(), 1);
                    // String Operators
                    case '$':
                        return GetSymbolToken(Symbol.DOLLAR, Current.ToString(), 1);
                    case '&':
                        return GetSymbolToken(Symbol.AMPERSAND, Current.ToString(), 1);
                    // Other
                    case ',':
                        return GetSymbolToken(Symbol.COMMA, Current.ToString(), 1);
                    case ':':
                        return GetSymbolToken(Symbol.COLON, Current.ToString(), 1);
                    // Error
                    default:
                        return GetSymbolToken(Symbol.ERROR, Current.ToString(), 1);
                }
            }

            return GetSymbolToken(Symbol.ENDOFFILE, "EOF", 1);
        }

        private Token GetReserveWordToken()
        {
            // Record the position and column of the character
            int start = _text_position;
            int col = _text_column;

            // Call Next while current char is a letter or digit or _
            while (char.IsLetterOrDigit(Current) || Current == '_')
                Next();

            // Get the text and its length
            int text_length = _text_position - start;
            string text = _code.Substring(start, text_length);

            // Check if the string text is a reserve word
            if (!LexerHelper.IsReserveKeyword(text))
                return new Token(Symbol.IDENTIFIER, text, _text_line, col);

            return new Token(LexerHelper.GetReserveKeywordSymbol(text), text, _text_line, col);
        }

        private Token GetCharacterToken()
        {
            // Record the position and column of the character
            int start = _text_position;
            int col = _text_column;

            // Current char is ' so call Next move to next char
            Next();
            // Call Next while current char is not whitespace
            while (Current != '\'')
                Next();
            // Current char is ' so call Next move to next char
            Next();

            // Get the text and its length
            int text_length = _text_position - start;
            string text = _code.Substring(start, text_length);

            // Check if the string text match on the char pattern
            if (!LexerHelper.RegexMatcher(@"^'(?:\[[\[\]\&\$\#']\])'|'[^\[\]\&\$\#']'$", text))
                return new Token(Symbol.ERROR, text, _text_line, col);

            return new Token(Symbol.CHARLITERAL, text, _text_line, col);
        }

        private Token GetBoolNStringToken()
        {
            // Record the position and column of the character
            int start = _text_position;
            int col = _text_column;

            // Current char is " so call Next move to next char
            Next();
            // Call Next while current char is not whitespace
            while (Current != '"')
                Next();
            // Current char is " so call Next move to next char
            Next();

            // Get the text and its length
            int text_length = _text_position - start;
            string text = _code.Substring(start, text_length);

            // Check if the string text match on the string pattern
            if (!LexerHelper.RegexMatcher(@"^""[^""]*""$", text))
                return new Token(Symbol.ERROR, text, _text_line, col);

            return new Token(Symbol.WORDLITERAL, text, _text_line, col);
        }

        private Token GetNumericToken()
        {
            // Record the position and column of the character
            int start = _text_position;
            int col = _text_column;

            // Call Next while current char is not whitespace
            while (!char.IsWhiteSpace(Current))
                Next();

            // Get the text and its length
            int text_length = _text_position - start;
            string text = _code.Substring(start, text_length);

            // Check if the string text does not match on the escape pattern
            if (!LexerHelper.RegexMatcher(@"^\d*\.?\d+$", text))
                return new Token(Symbol.ERROR, text, _text_line, col);

            return new Token(Symbol.NUMBERLITERAL, text, _text_line, col);
        }

        private Token GetEscapeToken()
        {
            // Record the position and column of the character
            int start = _text_position;
            int col = _text_column;

            // Call Next while current char is not ]
            while (Current != ']')
                Next();
            // Current char is ] so call Next move to next char
            Next();

            // Get the text and its length
            int text_length = _text_position - start;
            string text = _code.Substring(start, text_length);

            // Check if the string text does not match on the escape pattern
            if (!LexerHelper.RegexMatcher(@"^\[[\]\[\&\$\#]\]$", text))
                return new Token(Symbol.ERROR, text, _text_line, col);

            return new Token(Symbol.ESCAPE, text, _text_line, col);
        }

        private Token GetSymbolToken(Symbol symbol, string str_symbol, int offset)
        {
            Next(offset);
            return new Token(symbol, str_symbol, _text_line, _text_column - offset);
        }
    }
}
