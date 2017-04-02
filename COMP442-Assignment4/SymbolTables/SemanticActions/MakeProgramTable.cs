using COMP442_Assignment4.SymbolTables.SemanticActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP442_Assignment4.SymbolTables;
using COMP442_Assignment4.SymbolTables.SemanticRecords;
using COMP442_Assignment4.Tokens;
using COMP442_Assignment4.Lexical;

namespace COMP442_Assignment4.SymbolTables.SemanticActions
{
    // Create the entry for the main program function
    public class MakeProgramTable : SemanticAction
    {
        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken)
        {
            SymbolTable currentTable = symbolTable.Peek();

            Entry funcEntry = new FunctionEntry(currentTable, lastToken.getSemanticName(), new ClassEntry(""));

            symbolTable.Push(funcEntry.getChild());

            return new List<string>();
        }

        public override string getProductName()
        {
            return "Make program entry in symbol table";
        }
    }
}
