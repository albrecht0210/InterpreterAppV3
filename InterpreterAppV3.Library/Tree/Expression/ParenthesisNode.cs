using InterpreterAppV3.Library.Analyze;
using InterpreterAppV3.Library.Enum;

namespace InterpreterAppV3.Library.Tree.Expression
{
    public class ParenthesisNode : ExpressionNode
    {
        public ParenthesisNode(Token open_pr, ExpressionNode expression, Token close_pr)
        {
            Open_Pr = open_pr;
            Expression = expression;
            Close_Pr = close_pr;
        }

        public Token Open_Pr { get; }
        public ExpressionNode Expression { get; }
        public Token Close_Pr { get; }
    }
}
