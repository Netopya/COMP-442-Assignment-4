using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment4.SymbolTables.SemanticRecords
{
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
