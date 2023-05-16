using InterpreterAppV3.Library.Analyze;
using InterpreterAppV3.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterAppV3.Library.Tree.Expression.Term
{
    public class IdentifierNode : TermNode
    {
        private string _name;

        public IdentifierNode(CodeDataType data_type, Token token_node, LiteralNode literal) : base(data_type, token_node)
        {
            this._name = token_node.Code_Fragement;
            Literal = literal;
        }

        public string Name { get => _name; set => _name = value; }
        public LiteralNode Literal { get; }
    }
}
