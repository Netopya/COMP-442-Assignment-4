using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP442_Assignment4.Lexical;
using COMP442_Assignment4.SymbolTables.SemanticRecords;
using COMP442_Assignment4.CodeGeneration;

namespace COMP442_Assignment4.SymbolTables.SemanticActions
{
    // Create an entry for a variable declaration
    class MakeVariableTable : SemanticAction
    {
        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken, MoonCodeResult moonCode)
        {
            SymbolTable currentTable = symbolTable.Peek();

            // Get the last created variable
            SemanticRecord variableRecord = semanticRecordTable.Pop();

            Entry variableEntry = new VarParamEntry(currentTable, variableRecord.getVariable(), EntryKinds.variable);

            // Only declare variables for functions (not class member variables)
            //if(currentTable.getParent() is FunctionEntry)
            {
                int size = variableRecord.getVariable().GetSize();
                string address = variableEntry.getAddress();

                if (size <= 0)
                    moonCode.AddGlobal(string.Format("% {0} res 0", address));
                else if (size == 4)
                    moonCode.AddGlobal(string.Format("{0} dw 0", address));
                else
                    moonCode.AddGlobal(string.Format("{0} res {1}{2}align", address, size, Environment.NewLine));
            }
                

            return new List<string>();
        }

        public override string getProductName()
        {
            return "Add a variable to the symbolic table";
        }
    }
}
