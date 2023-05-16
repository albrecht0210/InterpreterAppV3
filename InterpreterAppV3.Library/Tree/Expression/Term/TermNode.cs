using InterpreterAppV3.Library.Analyze;
using InterpreterAppV3.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterAppV3.Library.Tree.Expression.Term
{
    public abstract class TermNode : ExpressionNode
    {
        protected TermNode(CodeDataType data_type, Token token_node)
        {
            Data_Type = data_type;
            Token_Node = token_node;
        }

        protected TermNode(Token token_node)
        {
            Token_Node = token_node;
        }

        public CodeDataType Data_Type { get; set; }
        public Token Token_Node { get; }
    }
}
