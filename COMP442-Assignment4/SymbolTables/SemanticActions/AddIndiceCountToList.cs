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
    class AddIndiceCountToList : SemanticAction
    {
        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken, MoonCodeResult moonCode)
        {
            List<string> errors = new List<string>();

            if (!semanticRecordTable.Any())
                errors.Add(string.Format("Cannot verify array index at line {0}", lastToken.getLine()));
            else
            {
                ExpressionRecord record = semanticRecordTable.Pop() as ExpressionRecord;

                if (record == null)
                    errors.Add(string.Format("Array indice at line {0} is not a valid expression", lastToken.getLine()));
                else if (record.GetExpressionType() != AddTypeToList.intClass)
                    errors.Add(string.Format("Array indice at line {0} is not a valid integer", lastToken.getLine()));
                else
                    semanticRecordTable.Push(new SemanticRecord(RecordTypes.IndiceCount, string.Empty));
            }            

            return errors;
        }

        public override string getProductName()
        {
            return "Count one array indice";
        }
    }
}
