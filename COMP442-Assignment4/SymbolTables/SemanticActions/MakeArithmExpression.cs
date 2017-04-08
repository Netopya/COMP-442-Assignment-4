using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP442_Assignment4.Lexical;
using COMP442_Assignment4.SymbolTables.SemanticRecords;
using COMP442_Assignment4.Tokens;
using COMP442_Assignment4.CodeGeneration;

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

        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken, MoonCodeResult moonCode)
        {
            string valA = string.Empty, valB = string.Empty;
            Token op = null;

            LinkedList<ExpressionRecord> expressions = new LinkedList<ExpressionRecord>();
            List<string> errors = new List<string>();

            while(expressions.Count < 2)
            {
                SemanticRecord record = semanticRecordTable.Pop();

                if (!semanticRecordTable.Any())
                {
                    errors.Add(string.Format("Grammar error: Not enough expressions to validate arithmetic operation at line {0}", lastToken.getLine()));
                    return errors;
                }

                BasicTokenRecord tokenRec = record as BasicTokenRecord;
                if (tokenRec != null)
                {
                    op = tokenRec.getToken();
                    continue;
                }
                
                if(record is ExpressionRecord)
                {
                    expressions.AddLast(record as ExpressionRecord);
                }
                else
                {
                    errors.Add(string.Format("Grammar error: record {0} at line {1} is not allowed during an arithmetic expression validation", record.recordType.ToString(), lastToken.getLine()));
                }
            }

            //List<string> code = new List<string> { string.Format("addi r3, r0, {0}", valA), string.Format("addi r4, r0, {0}", valB), string.Format("{0} r2, r3, r4", opLists[op]) };

            //code.ForEach(x => moonCode.AddLast(x));

            ClassEntry type1 = expressions.First.Value.GetExpressionType();
            ClassEntry type2 = expressions.Last.Value.GetExpressionType();

            if (type1 != AddTypeToList.intClass || type2 != AddTypeToList.intClass)
                errors.Add(string.Format("Cannot perform arithmetic operation at line {0} between factors of type {1} and {2}", lastToken.getLine(), type1.getName(), type2.getName()));

            semanticRecordTable.Push(new ExpressionRecord(AddTypeToList.intClass));

            return errors;
        }

        public override string getProductName()
        {
            return "Generate moon code for an arithmetic expression";
        }
    }
}
