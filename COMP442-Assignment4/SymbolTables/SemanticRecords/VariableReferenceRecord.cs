using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment4.SymbolTables.SemanticRecords
{
    // A semantic record for a variable reference with
    // a specified number of indices
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
