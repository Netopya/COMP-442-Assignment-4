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

        public ExpressionRecord(ClassEntry type) : base(RecordTypes.ExpressionType, "")
        {
            this.type = type;
        }

        public ClassEntry GetExpressionType()
        {
            return type;
        }
    }
}
