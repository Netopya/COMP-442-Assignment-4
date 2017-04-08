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
    class MigrateVariableReference : SemanticAction
    {
        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken, MoonCodeResult moonCode)
        {
            int sizeCount = 0;
            
            List<string> errors = new List<string>();

            if(!semanticRecordTable.Any())
            {
                errors.Add(string.Format("Grammar error at line {0}: could not migrate variable for emtpy stack", lastToken.getLine()));
                return errors;
            }


            SemanticRecord top = semanticRecordTable.Pop();



            while (top.recordType != RecordTypes.IdNameReference)
            {
                sizeCount++;

                if (top.recordType != RecordTypes.IndiceCount)
                {
                    errors.Add(string.Format("Grammar error at {0}. Migrating a variable reference should only count indecies, found {1}", lastToken.getLine(), top.recordType.ToString()));
                    sizeCount--;
                }
                    
                if(!semanticRecordTable.Any())
                {
                    errors.Add(string.Format("Grammar error at {0}. Could not find Id name for variable", lastToken.getLine()));
                    break;
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
