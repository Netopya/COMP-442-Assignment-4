using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP442_Assignment4.Lexical;
using COMP442_Assignment4.SymbolTables.SemanticRecords;

namespace COMP442_Assignment4.SymbolTables.SemanticActions
{
    /*
        When an Id is created, add it to the semantic stack 
    */
    class AddIdToList : SemanticAction
    {
        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken)
        {
            List<string> errors = new List<string>();
            string idName = lastToken.getSemanticName();

            // Check parent scopes to ensure that the id has not already been declared
            foreach (SymbolTable table in symbolTable)
            {
                foreach(Entry entry in table.GetEntries())
                {
                    if (entry.getName() == idName)
                    {
                        errors.Add(string.Format("Identifier {0} at line {1} has already been declared", idName, lastToken.getLine()));
                        break;
                    }
                }

                if (errors.Any())
                    break;
            }

            // Ensure that we don't already have the id on the semantic stack
            // waiting to be consumed
            foreach(SemanticRecord record in semanticRecordTable)
            {
                if(record.recordType == RecordTypes.Variable && record.getVariable().GetName() == idName)
                {
                    errors.Add(string.Format("Identifier {0} at line {1} has already been declared", idName, lastToken.getLine()));
                    break;
                }
            }

            // Add the Id to the semantic stack
            semanticRecordTable.Push(new SemanticRecord(RecordTypes.IdName, idName));

            return errors;
        }

        public override string getProductName()
        {
            return "Add a named type to the semantic stack";
        }
    }
}
