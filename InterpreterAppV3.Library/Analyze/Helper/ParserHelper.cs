using InterpreterAppV3.Library.Enum;
using InterpreterAppV3.Library.Tree.Expression.Term;
using InterpreterAppV3.Library.Tree.Expression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterAppV3.Library.Analyze.Helper
{
    public static class ParserHelper
    {
        public static bool IsSymbolMatch(Symbol symbol1, Symbol symbol2)
        {
            return symbol1 == symbol2;
        }

        public static bool IsDataType(Symbol symbol)
        {
            Symbol[] data_types = new Symbol[]
            {
                Symbol.INT, Symbol.FLOAT, Symbol.CHAR, Symbol.BOOL
            };

            return data_types.Contains(symbol);
        }

        public static bool IsLiteral(Symbol symbol)
        {
            Symbol[] literal_types = new Symbol[]
            {
                Symbol.NUMBERLITERAL, Symbol.WORDLITERAL, Symbol.CHARLITERAL
            };

            return literal_types.Contains(symbol);
        }

        public static bool IsLogicalOperator(Symbol symbol)
        {
            Symbol[] logical_operators = new Symbol[]
            {
                Symbol.AND, Symbol.OR, Symbol.NOT
            };

            return logical_operators.Contains(symbol);
        }

        public static bool IsComparisonOperator(Symbol symbol)
        {
            Symbol[] comparison_operators = new Symbol[]
            {
                Symbol.GREATERTHAN, Symbol.LESSTHAN, Symbol.GREATEREQUAL, Symbol.LESSEQUAL,
                Symbol.EQUALTO, Symbol.NOTEQUAL
            };

            return comparison_operators.Contains(symbol);
        }

        public static bool IsUnaryOperator(Symbol symbol)
        {
            Symbol[] unary_operators = new Symbol[]
            {
                Symbol.PLUS, Symbol.MINUS, Symbol.NOT
            };

            return unary_operators.Contains(symbol);
        }

        public static CodeDataType GetDataType(Symbol symbol_data_type)
        {
            if (Symbol.INT == symbol_data_type)
                return CodeDataType.INT;
            if (Symbol.FLOAT == symbol_data_type)
                return CodeDataType.FLOAT;
            if (Symbol.BOOL == symbol_data_type)
                return CodeDataType.BOOL;
            if (Symbol.CHAR == symbol_data_type)
                return CodeDataType.CHAR;

            throw new Exception("Data Type not found."); 
        }

        public static CodeDataType GetDataType(ExpressionNode term)
        {
            if (term is IdentifierNode)
            {
                IdentifierNode expr = (IdentifierNode)term;
                return expr.Data_Type;
            }
            else if (term is LiteralNode)
            {
                LiteralNode expr = (LiteralNode)term;
                return expr.Data_Type;
            }
            else if (term is BinaryNode)
            {
                BinaryNode expr = (BinaryNode)term;
                
                if (IsComparisonOperator(expr.Operation.Symbol))
                    return CodeDataType.BOOL;
                return GetDataType(expr.Left_Expression);
            }
            else if (term is UnaryNode)
            {
                UnaryNode expr = (UnaryNode)term;
                return GetDataType(expr.Expression_Node);
            }
            else if (term is ParenthesisNode)
            {
                ParenthesisNode expr = (ParenthesisNode)term;
                return GetDataType(expr.Expression_Node);
            }

            throw new Exception("Unknown Expression");
        }

        public static int GetOperatorPrecedence(Symbol symbol)
        {
            Dictionary<Symbol, int> operator_precedence = new Dictionary<Symbol, int>
            {
                { Symbol.OR, 1 },
                { Symbol.AND, 2 },
                { Symbol.LESSTHAN, 4 }, { Symbol.LESSEQUAL, 4 },
                { Symbol.GREATERTHAN, 4 }, { Symbol.GREATEREQUAL, 4 },
                { Symbol.EQUALTO, 4 }, {Symbol.NOTEQUAL, 4},
                { Symbol.PLUS, 5 }, { Symbol.MINUS, 5 },
                { Symbol.PERCENT, 6 },
                { Symbol.STAR, 7 }, { Symbol.SLASH, 7 }
            };

            if (operator_precedence.ContainsKey(symbol))
                return operator_precedence[symbol];
            return 0;
        }
    }
}
