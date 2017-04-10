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
    // Generate Moon code for a constant integer or float
    class AddConstIntOrFloat : SemanticAction
    {
        // True is the action is for integers, false for floats
        bool intType;

        public AddConstIntOrFloat(bool intType)
        {
            this.intType = intType;
        }

        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken, MoonCodeResult moonCode)
        {
            List<string> errors = new List<string>();
            string address = string.Empty;
            if(symbolTable.Any() || symbolTable.Peek().getParent() == null)
            {
                address = Entry.MakeAddressForEntry(symbolTable.Peek().getParent(), "const");
            }
            else
            {
                errors.Add(string.Format("Cannot evaluate constant at line {0} since it is not in a scope", lastToken.getLine()));
            }

            ExpressionRecord expression = new ExpressionRecord(intType ? AddTypeToList.intClass : AddTypeToList.floatClass, address);

            moonCode.AddGlobal(string.Format("{0} dw {1}", expression.GetAddress(), lastToken.getSemanticName()));

            semanticRecordTable.Push(expression);
            return new List<string>();
        }

        public override string getProductName()
        {
            return "Add const num to the semantic stack";
        }
    }
}
