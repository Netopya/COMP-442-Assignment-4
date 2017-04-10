using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment4.SymbolTables.SemanticRecords
{
    // A semantic record to store a function call along with expressions
    // that would be evaluated and passed as parameters
    class FunctionCallRecord : SemanticRecord
    {
        LinkedList<ExpressionRecord> parameterExpressions;

        public FunctionCallRecord(string name, LinkedList<ExpressionRecord> parameterExpressions):base(RecordTypes.FunctionCall, name)
        {
            this.parameterExpressions = parameterExpressions;
        }

        public int GetParameterCount()
        {
            return parameterExpressions.Count;
        }

        public LinkedList<ExpressionRecord> GetParameters()
        {
            return parameterExpressions;
        }
    }
}
