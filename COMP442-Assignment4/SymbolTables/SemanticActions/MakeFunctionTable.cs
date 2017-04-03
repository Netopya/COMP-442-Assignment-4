using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP442_Assignment4.Lexical;
using COMP442_Assignment4.SymbolTables.SemanticRecords;

namespace COMP442_Assignment4.SymbolTables.SemanticActions
{
    // Make a function entry in a symbol table
    public class MakeFunctionTable : SemanticAction
    {
        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken, List<string> moonCode)
        {
            SymbolTable currentTable = symbolTable.Peek();

            LinkedList<Variable> foundParameters = new LinkedList<Variable>();

            bool entryCreated = false;
            string funcName = string.Empty;
            List<string> errors = new List<string>();

            // Iterate over the semantic stack and consume records relevant to this function
            while(!entryCreated)
            {
                SemanticRecord topRecord = semanticRecordTable.Pop();

                // Handle the record depending on its type
                switch(topRecord.recordType)
                {
                    case RecordTypes.Variable:
                        // Encountered paramenters
                        foundParameters.AddFirst(topRecord.getVariable());
                        break;
                    case RecordTypes.IdName:
                        // The name of this function
                        funcName = topRecord.getValue();
                        break;
                    case RecordTypes.TypeName:
                        // If we encounter a type we are done collecting and can create the entry
                        FunctionEntry funcEntry = new FunctionEntry(currentTable, funcName, topRecord.getType());
                        funcEntry.AddParameters(foundParameters);

                        // Push the function's scope to the stack of symbol tables
                        symbolTable.Push(funcEntry.getChild());

                        entryCreated = true;

                        break;
                    default:
                        // This should only fail if there is an error in the grammar.
                        errors.Add("Grammar error, parsed rule that placed unexpected character on semantic stack");
                        break;
                }
            }

            return errors;
        }

        public override string getProductName()
        {
            return "Make the table for a function";
        }
    }
}
