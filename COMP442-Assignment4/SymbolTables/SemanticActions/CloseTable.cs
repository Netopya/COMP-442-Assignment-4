using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP442_Assignment4.Lexical;
using COMP442_Assignment4.SymbolTables.SemanticRecords;

namespace COMP442_Assignment4.SymbolTables.SemanticActions
{
    class CloseTable : SemanticAction
    {
        // Close a table and move to the parent table in the symbol table tree
        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken, List<string> moonCode)
        {
            if(symbolTable.Any())
                symbolTable.Pop();

            return new List<string>();
        }

        public override string getProductName()
        {
            return "Close a class or function table";
        }
    }
}
