using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP442_Assignment4.Lexical;
using COMP442_Assignment4.SymbolTables.SemanticRecords;

namespace COMP442_Assignment4.SymbolTables.SemanticActions
{
    // Make an entry (and associated symbol table) for an encountered class
    class MakeClassTable : SemanticAction
    {
        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken, List<string> moonCode)
        {
            SymbolTable currentTable = symbolTable.Peek();
            List<string> errors = new List<string>();
            string className = lastToken.getSemanticName();

            // Check if the class' name already exists
            foreach (Entry entry in currentTable.GetEntries())
            {
                if (entry.getName() == className)
                {
                    errors.Add(string.Format("Identifier {0} at line {1} has already been declared", className, lastToken.getLine()));
                    break;
                }
            }

            // Create a class entry
            Entry classEntry = new ClassEntry(className, currentTable);

            symbolTable.Push(classEntry.getChild());

            return errors;
        }

        public override string getProductName()
        {
            return "Make a class symbol table";
        }
    }
}
