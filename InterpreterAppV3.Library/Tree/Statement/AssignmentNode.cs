using InterpreterAppV3.Library.Tree.Expression;
using InterpreterAppV3.Library.Tree.Expression.Term;

namespace InterpreterAppV3.Library.Tree.Statement
{
    public class AssignmentNode : StatementNode
    {
        public AssignmentNode(List<IdentifierNode> variable, ExpressionNode expression)
        {
            Variable = variable;
            Expression = expression;
        }

        public List<IdentifierNode> Variable { get; }
        public ExpressionNode Expression { get; }
    }
}
