using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP442_Assignment4.Lexical;
using COMP442_Assignment4.SymbolTables.SemanticRecords;

namespace COMP442_Assignment4.SymbolTables.SemanticActions
{
    // Create an entry for a variable declaration
    class MakeVariableTable : SemanticAction
    {
        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken, LinkedList<string> moonCode)
        {
            SymbolTable currentTable = symbolTable.Peek();

            // Get the last created variable
            SemanticRecord variableRecord = semanticRecordTable.Pop();

            Entry variableEntry = new VarParamEntry(currentTable, variableRecord.getVariable(), EntryKinds.variable);

            moonCode.AddFirst(string.Format("{0} dw 0", variableEntry.getAddress()));

            return new List<string>();
        }

        public override string getProductName()
        {
            return "Add a variable to the symbolic table";
        }
    }
}
