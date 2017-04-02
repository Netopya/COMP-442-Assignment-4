using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP442_Assignment4.Lexical;
using COMP442_Assignment4.SymbolTables.SemanticRecords;

namespace COMP442_Assignment4.SymbolTables.SemanticActions
{
    // Add an array size definition to the stack
    class AddSizeToList : SemanticAction
    {
        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken)
        {
            semanticRecordTable.Push(new SemanticRecord(RecordTypes.Size, lastToken.getSemanticName()));

            return new List<string>();
        }

        public override string getProductName()
        {
            return "Add a size value to the semantic stack";
        }
    }
}
