using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP442_Assignment4.Lexical;
using COMP442_Assignment4.SymbolTables.SemanticRecords;

namespace COMP442_Assignment4.SymbolTables.SemanticActions
{
    class MigrateFunctionCall : SemanticAction
    {
        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken, LinkedList<string> moonCode)
        {
            int paramsCount = 0;
            SemanticRecord top = semanticRecordTable.Pop();
            List<string> errors = new List<string>();

            while (top.recordType != RecordTypes.IdNameReference)
            {
                paramsCount++;

                if (top.recordType != RecordTypes.FunctionParamCount)
                {
                    errors.Add(string.Format("Grammar error at {0}. Migrating a function call should only count parameters, found {1}", lastToken.getLine(), top.recordType.ToString()));
                    paramsCount--;
                }


                top = semanticRecordTable.Pop();
            }

            semanticRecordTable.Push(new FunctionCallRecord(top.getValue(), paramsCount));

            return errors;
        }

        public override string getProductName()
        {
            return "Generate function call information";
        }
    }
}
