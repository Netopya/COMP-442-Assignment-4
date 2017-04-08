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
    class CheckAssignment : SemanticAction
    {
        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken, MoonCodeResult moonCode)
        {
            LinkedList<ExpressionRecord> expressions = new LinkedList<ExpressionRecord>();
            List<string> errors = new List<string>();

            while(expressions.Count < 2)
            {
                if(!semanticRecordTable.Any())
                {
                    errors.Add(string.Format("Grammar error: Not enough expressions to validate assignment at line {0}", lastToken.getLine()));
                    return errors;
                }

                SemanticRecord lastRecord = semanticRecordTable.Pop();

                if(lastRecord.recordType != RecordTypes.ExpressionType)
                {
                    errors.Add(string.Format("Grammar error, expression validation at line {0} encountered an illegal record: \"{1}\"", lastToken.getLine(), lastRecord.recordType.ToString()));
                    continue;
                }

                expressions.AddLast((ExpressionRecord)lastRecord);
            }

            ExpressionRecord type1 = expressions.First.Value;
            ExpressionRecord type2 = expressions.Last.Value;
            if (type1.GetExpressionType() != type2.GetExpressionType())
                errors.Add(string.Format("Cannot equate at line {0} a value of type {1} to {2}", lastToken.getLine(), type1.GetExpressionType().getName(), type2.GetExpressionType().getName()));


            SymbolTable currentScope = symbolTable.Peek();
            string outAddress = string.Empty;

            if (currentScope.getParent() == null)
                errors.Add(string.Format("Cannot perform an assignment operation outside of a function"));
            else
            {
                moonCode.AddLine(currentScope.getParent().getAddress(), string.Format(@"
                    lw r2, {0}(r0)
                    sw {1}(r0), r2
                ", type1.GetAddress(), type2.GetAddress()));
            }

            return errors;
        }

        public override string getProductName()
        {
            return "Check the validity of an assignment";
        }
    }
}
