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
    class AddConstIntOrFloat : SemanticAction
    {
        bool intType;

        public AddConstIntOrFloat(bool intType)
        {
            this.intType = intType;
        }

        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken, MoonCodeResult moonCode)
        {
            semanticRecordTable.Push(new ExpressionRecord(intType ? AddTypeToList.intClass : AddTypeToList.floatClass));
            return new List<string>();
        }

        public override string getProductName()
        {
            return "Add const num to the semantic stack";
        }
    }
}
