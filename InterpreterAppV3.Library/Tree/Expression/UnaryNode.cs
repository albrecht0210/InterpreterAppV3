using InterpreterAppV3.Library.Analyze;
using InterpreterAppV3.Library.Enum;
using InterpreterAppV3.Library.Tree.Expression.Term;

namespace InterpreterAppV3.Library.Tree.Expression
{
    public class UnaryNode : ExpressionNode
    {
        public UnaryNode(Token operation, ExpressionNode expression_node)
        {
            Operation = operation;
            Expression_Node = expression_node;
        }

        public Token Operation { get; }
        public ExpressionNode Expression_Node { get; }
    }
}
