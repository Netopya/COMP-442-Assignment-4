using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP442_Assignment4.Lexical;
using COMP442_Assignment4.SymbolTables.SemanticRecords;

namespace COMP442_Assignment4.SymbolTables.SemanticActions
{
    public class AddConstNum : SemanticAction
    {
        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken, LinkedList<string> moonCode)
        {
            semanticRecordTable.Push(new SemanticRecord(RecordTypes.ConstNum, lastToken.getSemanticName()));

            return new List<string>();
        }

        public override string getProductName()
        {
            return "Add a constant number to the semantic stack";
        }
    }
}
