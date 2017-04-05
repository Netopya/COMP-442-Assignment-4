using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment4.SymbolTables.SemanticRecords
{
    class VariableReferenceRecord : SemanticRecord
    {
        int dimensions;

        public VariableReferenceRecord(string name, int dimensions) : base(RecordTypes.VariableReference, name)
        {
            this.dimensions = dimensions;
        }

        public int getDimensions()
        {
            return dimensions;
        }
    }
}
