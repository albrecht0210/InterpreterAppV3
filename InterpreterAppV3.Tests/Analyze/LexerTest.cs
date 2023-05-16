using InterpreterAppV3.Library.Analyze;
using InterpreterAppV3.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterAppV3.Tests.Analyze
{
    public class LexerTest
    {
        [Test]
        public void GetToken_ValidTest()
        {
            string input = "BEGIN CODE\r\n\tINT i\r\n\tBOOL c = \"FALSE\"\r\nEND CODE";

            Lexer lexer = new Lexer(input);

            Token[] expected_tokens = new Token[]
            {
                new Token(Symbol.BEGIN, "BEGIN", 1, 1),
                new Token(Symbol.CODE, "CODE", 1, 7),
                new Token(Symbol.NEWLINE, "\\n", 1, 11),
                new Token(Symbol.INT, "INT", 2, 2),
                new Token(Symbol.IDENTIFIER, "i", 2, 6),
                new Token(Symbol.NEWLINE, "\\n", 2, 7),
                new Token(Symbol.BOOL, "BOOL", 3, 2),
                new Token(Symbol.IDENTIFIER, "c", 3, 7),
                new Token(Symbol.EQUAL, "=", 3, 9),
                new Token(Symbol.WORDLITERAL, "\"FALSE\"", 3, 11),
                new Token(Symbol.NEWLINE, "\\n", 3, 18),
                new Token(Symbol.END, "END", 4, 1),
                new Token(Symbol.CODE, "CODE", 4, 5),
                new Token(Symbol.ENDOFFILE, "EOF", 4, 9)
            };

            for (int i = 0; i < expected_tokens.Length; i++)
            {
                Token actual_token = lexer.GetToken();
                Console.WriteLine(actual_token);
                Assert.AreEqual(expected_tokens[i].Symbol, actual_token.Symbol);
                Assert.AreEqual(expected_tokens[i].Code_Fragement, actual_token.Code_Fragement);
                Assert.AreEqual(expected_tokens[i].Line, actual_token.Line);
                Assert.AreEqual(expected_tokens[i].Column, actual_token.Column);
            }
        }

        [Test]
        public void GetToken_InvalidTest()
        {
            string input = "BEGIN CODE\r\n\tINT x, z \r\n\tBOOL INT=\"TRUE\"\r\nEND CODE";

            Lexer lexer = new Lexer(input);

            Token[] expected_tokens = new Token[]
            {
                new Token(Symbol.BEGIN, "BEGIN", 1, 1),
                new Token(Symbol.CODE, "CODE", 1, 7),
                new Token(Symbol.NEWLINE, "\\n", 1, 11),
                new Token(Symbol.INT, "INT", 2, 2),
                new Token(Symbol.IDENTIFIER, "x", 2, 6),
                new Token(Symbol.COMMA, ",", 2, 7),
                new Token(Symbol.IDENTIFIER, "z", 2, 9),
                new Token(Symbol.NEWLINE, "\\n", 2, 11),
                new Token(Symbol.BOOL, "BOOL", 3, 2),
                new Token(Symbol.INT, "INT", 3, 7),
                new Token(Symbol.EQUAL, "=", 3, 10),
                new Token(Symbol.WORDLITERAL, "\"TRUE\"", 3, 11),
                new Token(Symbol.NEWLINE, "\\n", 3, 17),
                new Token(Symbol.END, "END", 4, 1),
                new Token(Symbol.CODE, "CODE", 4, 5),
                new Token(Symbol.ENDOFFILE, "EOF", 4, 9)
            };

            for (int i = 0; i < expected_tokens.Length; i++)
            {
                Token actual_token = lexer.GetToken();
                Console.WriteLine(actual_token);
                Assert.AreEqual(expected_tokens[i].Symbol, actual_token.Symbol);
                Assert.AreEqual(expected_tokens[i].Code_Fragement, actual_token.Code_Fragement);
                Assert.AreEqual(expected_tokens[i].Line, actual_token.Line);
                Assert.AreEqual(expected_tokens[i].Column, actual_token.Column);
            }
        }
    }
}
