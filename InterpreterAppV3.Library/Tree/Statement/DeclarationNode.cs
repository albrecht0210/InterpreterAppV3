using InterpreterAppV3.Library.Enum;
using InterpreterAppV3.Library.Tree.Expression;
using InterpreterAppV3.Library.Tree.Expression.Term;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterAppV3.Library.Tree.Statement
{
    public class DeclarationNode : StatementNode
    {
        public DeclarationNode(CodeDataType data_type, Dictionary<IdentifierNode, ExpressionNode> variable_declaration)
        {
            Data_Type = data_type;
            Variable_Declaration = variable_declaration;
        }

        public CodeDataType Data_Type { get; }
        public Dictionary<IdentifierNode, ExpressionNode> Variable_Declaration { get; }
    }
}
