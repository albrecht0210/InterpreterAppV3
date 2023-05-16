using InterpreterAppV3.Library.Analyze;
using InterpreterAppV3.Library.Enum;

namespace InterpreterAppV3.Library.Tree.Expression
{
    public class BinaryNode : ExpressionNode
    {
        public BinaryNode(ExpressionNode left_expression, Token operation, ExpressionNode right_expression)
        {
            Left_Expression = left_expression;
            Operation = operation;
            Right_Expression = right_expression;
        }

        public ExpressionNode Left_Expression { get; }
        public Token Operation { get; }
        public ExpressionNode Right_Expression { get; }
    }
}
