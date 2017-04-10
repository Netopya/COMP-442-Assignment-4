using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment4.SymbolTables.SemanticRecords
{
    // A semantic record for an expression
    // Holds the return type for the expression and the address of the expression's result
    class ExpressionRecord : SemanticRecord
    {
        ClassEntry type;
        string address;

        public ExpressionRecord(ClassEntry type, string address) : base(RecordTypes.ExpressionType, "")
        {
            this.type = type;
            this.address = address;
        }

        public ClassEntry GetExpressionType()
        {
            return type;
        }

        public string GetAddress()
        {
            return address;
        }
    }
}
