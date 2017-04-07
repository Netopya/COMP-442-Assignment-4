using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP442_Assignment4.Lexical;
using COMP442_Assignment4.SymbolTables.SemanticRecords;

namespace COMP442_Assignment4.SymbolTables.SemanticActions
{
    class VerifyFactorReference : SemanticAction
    {
        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken, LinkedList<string> moonCode)
        {
            SemanticRecord lastRecord = semanticRecordTable.Pop();
            Stack<SemanticRecord> callChain = new Stack<SemanticRecord>();
            List<string> errors = new List<string>();

            while(lastRecord.recordType != RecordTypes.FactorStart)
            {
                callChain.Push(lastRecord);

                lastRecord = semanticRecordTable.Pop();
            }

            SemanticRecord currentLink = callChain.Pop();
            Entry linkedVariable = null;

            foreach (SymbolTable table in symbolTable)
            {
                linkedVariable = table.GetEntries().FirstOrDefault(x => (x is VarParamEntry || x is FunctionEntry) && x.getName() == currentLink.getValue());

                if (linkedVariable != null)
                    break;
            }

            bool success = VerifyLink(currentLink, linkedVariable, lastToken, errors);

            if (!success)
                return errors;


            while (callChain.Any())
            {
                currentLink = callChain.Pop();

                ClassEntry referredClass = ((VarParamEntry)linkedVariable).getVariable().getClass();

                if (referredClass.getChild() == null)
                {
                    errors.Add(string.Format("Identifier {0} cannot be reached at line {1}", currentLink.getValue(), lastToken.getLine()));
                    return errors;
                }

                linkedVariable = referredClass.getChild().GetEntries().FirstOrDefault(x => (x is VarParamEntry || x is FunctionEntry) && x.getName() == currentLink.getValue());

                success = VerifyLink(currentLink, linkedVariable, lastToken, errors);

                if (!success)
                    return errors;
            }

            ClassEntry expressionType;

            if (linkedVariable is VarParamEntry)
                expressionType = ((VarParamEntry)linkedVariable).getVariable().getClass();
            else
                expressionType = ((FunctionEntry)linkedVariable).GetReturnType();

            semanticRecordTable.Push(new ExpressionRecord(expressionType));

            return new List<string>();
        }

        public override string getProductName()
        {
            return "Verify data members";
        }
        
        private bool VerifyLink(SemanticRecord currentLink, Entry linkedVariable, IToken lastToken, List<string> errors)
        {
            if (linkedVariable == null)
            {
                errors.Add(string.Format("Undefined identifier {0} at line {1}", currentLink.getValue(), lastToken.getLine()));
                return false;
            }

            if(linkedVariable is VarParamEntry && currentLink is FunctionCallRecord)
            {
                errors.Add(string.Format("Identifier {0} at line {1} is referring to a variable like a function", currentLink.getValue(), lastToken.getLine()));
                return false;
            }
            else if(linkedVariable is FunctionEntry && currentLink is VariableReferenceRecord)
            {
                errors.Add(string.Format("Identifier {0} at line {1} is referring to a function like a variable", currentLink.getValue(), lastToken.getLine()));
                return false;
            }

            // Verify that a variable is being accessed with the correct number of indices
            if (currentLink is VariableReferenceRecord)
            {
                if (((VarParamEntry)linkedVariable).getVariable().GetDimensions().Count != ((VariableReferenceRecord)currentLink).getDimensions())
                {
                    errors.Add(string.Format("Identifier {0} at line {1} does not have the correct number of indices. Counted {2} expected {3}"
                        , currentLink.getValue(), lastToken.getLine(), ((VariableReferenceRecord)currentLink).getDimensions(), ((VarParamEntry)linkedVariable).getVariable().GetDimensions().Count));
                    return false;
                }
                return true;
            }
            else if (currentLink is FunctionCallRecord)
            {
                if (CountNumParams(linkedVariable as FunctionEntry) != ((FunctionCallRecord)currentLink).GetParameterCount())
                {
                    errors.Add(string.Format("Identifier {0} at line {1} does not have the correct number of parameters. Counted {2} expected {3}"
                        , currentLink.getValue(), lastToken.getLine(), ((FunctionCallRecord)currentLink).GetParameterCount(), CountNumParams(linkedVariable as FunctionEntry)));
                    return false;
                }
                return true;
            }
            else
            {
                errors.Add("Grammar error: Illegal record on semantic stack");
                return false;
            }
        }

        private int CountNumParams(FunctionEntry entry)
        {
            return entry.getChild().GetEntries().Where(x => x.getKind() == EntryKinds.parameter).Count();
        }
    }
}
