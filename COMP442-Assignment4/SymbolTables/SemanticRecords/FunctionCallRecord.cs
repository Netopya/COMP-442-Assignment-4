using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment4.SymbolTables.SemanticRecords
{
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
