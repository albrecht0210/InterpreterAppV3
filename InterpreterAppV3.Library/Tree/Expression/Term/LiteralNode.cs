using InterpreterAppV3.Library.Analyze;
using InterpreterAppV3.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterAppV3.Library.Tree.Expression.Term
{
    public class LiteralNode : TermNode
    {
        private dynamic _value;

        public LiteralNode(CodeDataType data_type, Token token_node) : base(data_type, token_node)
        {
            this._value = Convert();
        }

        public dynamic Value { get => _value; }

        private dynamic Convert()
        {
            if (int.TryParse(Token_Node.Code_Fragement, out int i_val))
                return i_val;

            if (float.TryParse(Token_Node.Code_Fragement, out float f_val))
                return f_val;

            if (bool.TryParse(Token_Node.Code_Fragement.Substring(1, Token_Node.Code_Fragement.Length - 2), out bool b_val))
                return b_val;

            if (char.TryParse(Token_Node.Code_Fragement.Substring(1, Token_Node.Code_Fragement.Length - 2), out char c_val))
                return c_val;

            return Token_Node.Code_Fragement;
        }
    }
}
