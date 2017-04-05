using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP442_Assignment4.Lexical;
using COMP442_Assignment4.SymbolTables.SemanticRecords;

namespace COMP442_Assignment4.SymbolTables.SemanticActions
{
    class MigrateVariableReference : SemanticAction
    {
        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken, LinkedList<string> moonCode)
        {
            int sizeCount = 0;
            SemanticRecord top = semanticRecordTable.Pop();
            List<string> errors = new List<string>();

            while (top.recordType != RecordTypes.IdNameReference)
            {
                sizeCount++;

                if (top.recordType != RecordTypes.IndiceCount)
                {
                    errors.Add(string.Format("Grammar error at {0}. Migrating a variable reference should only count indecies, found {1}", lastToken.getLine(), top.recordType.ToString()));
                    sizeCount--;
                }
                    

                top = semanticRecordTable.Pop();
            }

            semanticRecordTable.Push(new VariableReferenceRecord(top.getValue(), sizeCount));

            return errors;
        }

        public override string getProductName()
        {
            return "Migrate a variable reference to the semantic stack";
        }
    }
}
