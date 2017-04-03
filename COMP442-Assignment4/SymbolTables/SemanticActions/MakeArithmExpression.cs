using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP442_Assignment4.Lexical;
using COMP442_Assignment4.SymbolTables.SemanticRecords;
using COMP442_Assignment4.Tokens;

namespace COMP442_Assignment4.SymbolTables.SemanticActions
{
    class MakeArithmExpression : SemanticAction
    {
        private readonly Dictionary<Token, string> opLists = new Dictionary<Token, string>
        {
            { TokenList.Plus, "add"},
            { TokenList.Minus, "sub"},
            { TokenList.Asterisk, "mul"},
            { TokenList.Slash, "div"}
        };

        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken, LinkedList<string> moonCode)
        {
            string valA = string.Empty, valB = string.Empty;
            Token op = null;

            int i = 0;

            while(i < 3)
            {
                SemanticRecord record = semanticRecordTable.Pop();

                switch(i)
                {
                    case 0:
                        valA = record.getValue();
                        break;
                    case 1:
                        op = ((BasicTokenRecord)record).getToken();
                        break;
                    case 2:
                        valB = record.getValue();
                        break;
                }

                i++;
            }

            List<string> code = new List<string> { string.Format("addi r3, r0, {0}", valA), string.Format("addi r4, r0, {0}", valB), string.Format("{0} r2, r3, r4", opLists[op]) };

            code.ForEach(x => moonCode.AddLast(x));

            return new List<string>();
        }

        public override string getProductName()
        {
            return "Generate moon code for an arithmetic expression";
        }
    }
}
