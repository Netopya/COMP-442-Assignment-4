using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP442_Assignment4.Lexical;
using COMP442_Assignment4.SymbolTables.SemanticRecords;

namespace COMP442_Assignment4.SymbolTables.SemanticActions
{
    class AddTokenToList : SemanticAction
    {
        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken, LinkedList<string> moonCode)
        {
            semanticRecordTable.Push(new BasicTokenRecord(lastToken.getToken()));

            return new List<string>();
        }

        public override string getProductName()
        {
            return "Add a basic token to the semantic stack";
        }
    }
}
