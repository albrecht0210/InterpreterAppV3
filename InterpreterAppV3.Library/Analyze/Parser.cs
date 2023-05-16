using InterpreterAppV3.Library.Analyze.Helper;
using InterpreterAppV3.Library.Enum;
using InterpreterAppV3.Library.Tree;
using InterpreterAppV3.Library.Tree.Expression;
using InterpreterAppV3.Library.Tree.Expression.Term;
using InterpreterAppV3.Library.Tree.Statement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterAppV3.Library.Analyze
{
    public class Parser
    {
        private readonly Lexer _lexer;
        private Token _current_token;
        private Dictionary<string, CodeDataType> _variables_dict;

        public Parser(Lexer lexer)
        {
            this._lexer = lexer;
            this._current_token = this._lexer.GetToken();
            this._variables_dict = new Dictionary<string, CodeDataType>();
        }

        public ProgramNode Parse()
        {
            return Parse(Symbol.CODE);
        }
        
        public ProgramNode Parse(Symbol block)
        {
            ConsumeNewLineToken();

            ConsumeToken(Symbol.BEGIN);
            ConsumeToken(block);
            
            ConsumeNewLineToken();

            List<StatementNode> statements = ParseStatements();

            ConsumeNewLineToken();

            ConsumeToken(Symbol.END);
            ConsumeToken(block);

            ConsumeNewLineToken();

            if (ParserHelper.IsSymbolMatch(Symbol.CODE, block))
                ConsumeToken(Symbol.ENDOFFILE);

            return new ProgramNode(statements);
        }

        private List<StatementNode> ParseStatements()
        {
            List<StatementNode> statements = new List<StatementNode>();

            while (!ParserHelper.IsSymbolMatch(Symbol.END, _current_token.Symbol))
            {
                if (ParserHelper.IsDataType(_current_token.Symbol))
                {
                    statements.Add(ParseVariableDeclaration());
                }
                else if (ParserHelper.IsSymbolMatch(Symbol.IDENTIFIER, _current_token.Symbol))
                {
                    statements.Add(ParseAssignment());
                }
                else if (ParserHelper.IsSymbolMatch(Symbol.DISPLAY, _current_token.Symbol))
                {
                    statements.Add(ParseDisplay());
                }
            }

            return statements;
        }

        private DeclarationNode ParseVariableDeclaration()
        {
            CodeDataType data_type = ParserHelper.GetDataType(_current_token.Symbol);
            ConsumeToken(_current_token.Symbol);

            Dictionary<IdentifierNode, ExpressionNode> variables = new Dictionary<IdentifierNode, ExpressionNode>();

            ExtractDeclaration(data_type, variables);

            ConsumeNewLineToken();

            return new DeclarationNode(data_type, variables);
        }

        private void ExtractDeclaration(CodeDataType data_type, Dictionary<IdentifierNode, ExpressionNode> variables)
        {
            Token identifier = _current_token;
            ConsumeToken(Symbol.IDENTIFIER);

            // Semantic check on variables
            if (SemanticHelper.IsVariableExists(this._variables_dict.Keys.ToList(), identifier.Code_Fragement))
                throw new Exception($"({identifier.Line},{identifier.Column}): Variable '{identifier.Code_Fragement}' is already defined.");

            IdentifierNode var_identifier = new IdentifierNode(data_type, identifier);
            ExpressionNode var_value = null;

            if (ParserHelper.IsSymbolMatch(Symbol.EQUAL, _current_token.Symbol))
            {
                ConsumeToken(Symbol.EQUAL);
                var_value = ParseExpression();
                CodeDataType data_type_value = ParserHelper.GetDataType(var_value);
                
                // Semantic check on data types assigning
                if (!SemanticHelper.IsSameDataType(data_type, data_type_value))
                    throw new Exception($"({identifier.Line},{identifier.Column}): Type mismatch: Cannot assign {data_type_value} on {data_type}");
            }

            variables.Add(var_identifier, var_value);
            _variables_dict.Add(var_identifier.Name, data_type);

            if (ParserHelper.IsSymbolMatch(Symbol.COMMA, _current_token.Symbol))
            {
                ConsumeToken(Symbol.COMMA);
                ExtractDeclaration(data_type, variables);
            }
        }

        private AssignmentNode ParseAssignment()
        {
            List<IdentifierNode> variable_names = new List<IdentifierNode>();

            Token identifier = _current_token;
            variable_names.Add(ParseIdentifierExpression());

            ConsumeToken(Symbol.EQUAL);

            ExpressionNode expression = ParseExpression();

            while (ParserHelper.IsSymbolMatch(Symbol.EQUAL, _current_token.Symbol))
            {
                IdentifierNode i_expr = (IdentifierNode)expression;
                variable_names.Add(i_expr);

                ConsumeToken(Symbol.EQUAL);

                expression = ParseExpression();
            }

            ConsumeNewLineToken();

            return new AssignmentNode(variable_names, expression);
        }

        private StatementNode ParseDisplay()
        {
            throw new NotImplementedException();
        }
        private ExpressionNode ParseExpression()
        {
            if (ParserHelper.IsLiteral(_current_token.Symbol))
                return ParseBinaryExpression();
            else if (ParserHelper.IsSymbolMatch(Symbol.IDENTIFIER, _current_token.Symbol))
                return ParseBinaryExpression();
            else if (ParserHelper.IsSymbolMatch(Symbol.OPENPARENTHESIS, _current_token.Symbol))
                return ParseParenthesisExpression();
            else if (ParserHelper.IsUnaryOperator(_current_token.Symbol))
                return ParseUnaryExpression();
            else
                throw new Exception($"({_current_token.Line}, {_current_token.Column}): Unexpected {_current_token.Symbol} token expected expression token.");
        }

        private ExpressionNode ParseTerm()
        {
            if (ParserHelper.IsLiteral(_current_token.Symbol))
                return ParseLiteralExpression();
            else if (ParserHelper.IsSymbolMatch(Symbol.IDENTIFIER, _current_token.Symbol))
                return ParseIdentifierExpression();

            return ParseExpression();
        }

        private LiteralNode ParseLiteralExpression()
        {
            Token literal = _current_token;
            ConsumeToken(literal.Symbol);

            return new LiteralNode(literal);
        }

        private IdentifierNode ParseIdentifierExpression()
        {
            Token identifier_token = _current_token;
            ConsumeToken(Symbol.IDENTIFIER);

            if (!SemanticHelper.IsVariableExists(_variables_dict.Keys.ToList(), identifier_token.Code_Fragement))
                throw new Exception($"({identifier_token.Line},{identifier_token.Column}): Variable '{identifier_token.Code_Fragement}' is not defined.");

            CodeDataType data_type = _variables_dict[identifier_token.Code_Fragement];

            return new IdentifierNode(data_type, identifier_token);
        }

        private ExpressionNode ParseBinaryExpression()
        {
            return ParseBinaryExpression(null);
        }

        private ExpressionNode ParseBinaryExpression(ExpressionNode left_binary)
        {
            ExpressionNode left_term = left_binary;

            if (left_term == null)
                left_term = ParseTerm();

            int precedence = ParserHelper.GetOperatorPrecedence(_current_token.Symbol);

            while (precedence > 0)
            {
                Token ope = _current_token;
                ConsumeToken(ope.Symbol);

                ExpressionNode right_term = ParseTerm();

                int next_precedence = ParserHelper.GetOperatorPrecedence(_current_token.Symbol);

                if (next_precedence > precedence)
                    right_term = ParseBinaryExpression(right_term);

                CodeDataType dt1 = ParserHelper.GetDataType(left_term);
                CodeDataType dt2 = ParserHelper.GetDataType(right_term);

                if (!SemanticHelper.IsBinaryOperationViable(ope.Symbol, dt1, dt2))
                    throw new Exception($"({ope.Line},{ope.Column}): Type mismatch: Cannot use {ope.Code_Fragement} on {dt1} and {dt2}");

                left_term = new BinaryNode(left_term, ope, right_term);

                precedence = ParserHelper.GetOperatorPrecedence(_current_token.Symbol);
            }

            return left_term;
        }

        private ExpressionNode ParseParenthesisExpression()
        {
            Token open_parenthesis = _current_token;
            ConsumeToken(Symbol.OPENPARENTHESIS);

            ExpressionNode expression = ParseExpression();

            Token close_parenthesis = _current_token;
            ConsumeToken(Symbol.CLOSEPARENTHESIS);

            ParenthesisNode p_expr = new ParenthesisNode(open_parenthesis, expression, close_parenthesis);

            if (ParserHelper.GetOperatorPrecedence(_current_token.Symbol) > 0)
                return ParseBinaryExpression(p_expr);

            return p_expr;
        }

        private ExpressionNode ParseUnaryExpression()
        {
            Token unary = _current_token;
            ConsumeToken(unary.Symbol);

            ExpressionNode expression = ParseTerm();

            CodeDataType data_type = ParserHelper.GetDataType(expression);

            if (!SemanticHelper.IsUnaryOperationViable(unary.Symbol, data_type))
                throw new Exception($"({unary.Line},{unary.Column}): Type mismatch: Cannot use {unary.Code_Fragement} on {data_type}");

            UnaryNode u_expr = new UnaryNode(unary, expression);

            if (ParserHelper.GetOperatorPrecedence(_current_token.Symbol) > 0)
                return ParseBinaryExpression(u_expr);

            return u_expr;
        }

        private void ConsumeNewLineToken()
        {
            while (ParserHelper.IsSymbolMatch(Symbol.NEWLINE, _current_token.Symbol))
                ConsumeToken(Symbol.NEWLINE);
        }

        private void ConsumeToken(Symbol symbol)
        {
            if (ParserHelper.IsSymbolMatch(symbol, _current_token.Symbol))
            {
                this._current_token = this._lexer.GetToken();

                if (ParserHelper.IsSymbolMatch(_current_token.Symbol, Symbol.ERROR))
                    throw new Exception($"({_current_token.Line},{_current_token.Column}): Unexpected symbol '{_current_token.Code_Fragement}'");
            }
            else
                throw new Exception($"({_current_token.Line},{_current_token.Column}): Invalid Syntax '{_current_token.Code_Fragement}'");
        }
    }
}
