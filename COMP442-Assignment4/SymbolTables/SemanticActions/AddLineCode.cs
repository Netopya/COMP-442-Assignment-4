using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP442_Assignment4.CodeGeneration;
using COMP442_Assignment4.Lexical;
using COMP442_Assignment4.SymbolTables.SemanticRecords;

namespace COMP442_Assignment4.SymbolTables.SemanticActions
{
    class AddLineCode : SemanticAction
    {
        string line;

        public AddLineCode(string line)
        {
            this.line = line;
        }


        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken, MoonCodeResult moonCode)
        {
            List<string> errors = new List<string>();

            if (symbolTable.Any() && symbolTable.Peek().getParent() != null)
                moonCode.AddLine(symbolTable.Peek().getParent().getAddress(), line);
            else
                errors.Add(string.Format("Grammar error: Could not generate the requested code \"{0}\" at line {1}", line, lastToken.getLine()));

            return new List<string>();
        }

        public override string getProductName()
        {
            return "Add a specified line to the moon code generator";
        }
    }
}
