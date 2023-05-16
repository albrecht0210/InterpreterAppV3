using InterpreterAppV3.Library.Tree.Statement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterAppV3.Library.Tree
{
    public class ProgramNode : AST
    {
        public ProgramNode(List<StatementNode> statements) 
        {
            Statements = statements;
        }

        public List<StatementNode> Statements { get; }
    }
}
