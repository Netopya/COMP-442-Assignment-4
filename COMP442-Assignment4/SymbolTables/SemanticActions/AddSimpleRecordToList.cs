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
    class AddSimpleRecordToList : SemanticAction
    {
        string value;
        RecordTypes type;

        public AddSimpleRecordToList(string value, RecordTypes type)
        {
            this.value = value;
            this.type = type;
        }

        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken, MoonCodeResult moonCode)
        {
            semanticRecordTable.Push(new SemanticRecord(type, value));

            return new List<string>(); ;
        }

        public override string getProductName()
        {
            return "Add a pre-set record to the semantic stack";
        }
    }
}
