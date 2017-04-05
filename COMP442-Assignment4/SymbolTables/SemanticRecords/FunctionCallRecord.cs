using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment4.SymbolTables.SemanticRecords
{
    class FunctionCallRecord : SemanticRecord
    {
        int parameterCount;

        public FunctionCallRecord(string name, int parameterCount):base(RecordTypes.FunctionCall, name)
        {
            this.parameterCount = parameterCount;
        }

        public int GetParameterCount()
        {
            return parameterCount;
        }
    }
}
