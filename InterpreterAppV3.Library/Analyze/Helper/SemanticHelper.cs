using InterpreterAppV3.Library.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterAppV3.Library.Analyze.Helper
{
    public static class SemanticHelper
    {
        public static bool IsVariableExists(List<string> variable_names, string name)
        {
            return variable_names.Contains(name);
        }

        public static bool IsSameDataType(CodeDataType term1_data_type, CodeDataType term2_data_type)
        {
            if (term1_data_type == CodeDataType.FLOAT && term2_data_type == CodeDataType.INT)
                return true;

            if (term1_data_type == CodeDataType.INT && term2_data_type == CodeDataType.CHAR)
                return true;

            return term1_data_type == term2_data_type;
        }
    
        public static bool IsBinaryOperationViable(Symbol symbol, CodeDataType term1_data_type, CodeDataType term2_data_type) 
        {
            if (ParserHelper.IsLogicalOperator(symbol))
                return IsLogicalOperationViable(term1_data_type, term2_data_type);

            if (term1_data_type == CodeDataType.CHAR && term2_data_type == CodeDataType.INT)
                return true;

            return IsSameDataType(term1_data_type, term2_data_type);
        }

        public static bool IsLogicalOperationViable(CodeDataType term1_data_type, CodeDataType term2_data_type)
        {
            return term1_data_type == CodeDataType.BOOL && term2_data_type == CodeDataType.BOOL;
        }

        public static bool IsUnaryOperationViable(Symbol symbol, CodeDataType term_data_type)
        {
            if (ParserHelper.IsLogicalOperator(symbol))
                return IsLogicalOperationViable(CodeDataType.BOOL, term_data_type);

            return term_data_type == CodeDataType.INT || term_data_type == CodeDataType.FLOAT || term_data_type == CodeDataType.CHAR;
        }
    }
}
