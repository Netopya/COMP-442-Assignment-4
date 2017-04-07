using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP442_Assignment4.Lexical;
using COMP442_Assignment4.SymbolTables.SemanticRecords;

namespace COMP442_Assignment4.SymbolTables.SemanticActions
{
    class CheckAssignment : SemanticAction
    {
        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken, LinkedList<string> moonCode)
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
                    return errors;
                }

                expressions.AddLast((ExpressionRecord)lastRecord);
            }

            ClassEntry type1 = expressions.First.Value.GetExpressionType();
            ClassEntry type2 = expressions.Last.Value.GetExpressionType();
            if (type1 != type2)
                errors.Add(string.Format("Cannot equate at line {0} a value of type {1} to {2}", lastToken.getLine(), type1.getName(), type2.getName()));

            return errors;
        }

        public override string getProductName()
        {
            return "Check the validity of an assignment";
        }
    }
}
