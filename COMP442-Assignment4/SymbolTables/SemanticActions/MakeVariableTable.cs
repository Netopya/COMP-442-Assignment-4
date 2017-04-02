using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP442_Assignment3.Lexical;
using COMP442_Assignment3.SymbolTables.SemanticRecords;

namespace COMP442_Assignment3.SymbolTables.SemanticActions
{
    // Create an entry for a variable declaration
    class MakeVariableTable : SemanticAction
    {
        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken)
        {
            SymbolTable currentTable = symbolTable.Peek();

            // Get the last created variable
            SemanticRecord variableRecord = semanticRecordTable.Pop();

            Entry variableEntry = new VarParamEntry(currentTable, variableRecord.getVariable(), EntryKinds.variable);

            return new List<string>();
        }

        public override string getProductName()
        {
            return "Add a variable to the symbolic table";
        }
    }
}
