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

            if (linkedVariable == null)
            {
                errors.Add(string.Format("Undefined identifier {0} at line {1}", currentLink.getValue(), lastToken.getLine()));
                return errors;
            }

            if(currentLink is VariableReferenceRecord)
            {
                if (((VarParamEntry)linkedVariable).getVariable().GetDimensions().Count != ((VariableReferenceRecord)currentLink).getDimensions())
                {
                    errors.Add(string.Format("Identifier {0} at line {1} does not have the correct number of indices. Counted {2} expected {3}"
                        , currentLink.getValue(), lastToken.getLine(), ((VariableReferenceRecord)currentLink).getDimensions(), ((VarParamEntry)linkedVariable).getVariable().GetDimensions().Count));
                    return errors;
                }
            }
            else if(currentLink is FunctionCallRecord)
            {
                if (((FunctionEntry)linkedVariable).getChild().GetEntries().Where(x => x.getKind() == EntryKinds.parameter).Count() != ((FunctionCallRecord)currentLink).GetParameterCount())
                {
                    errors.Add(string.Format("Identifier {0} at line {1} does not have the correct number of parameters. Counted {2} expected {3}"
                        , currentLink.getValue(), lastToken.getLine(), ((FunctionCallRecord)currentLink).GetParameterCount(), ((FunctionEntry)linkedVariable).getChild().GetEntries().Where(x => x.getKind() == EntryKinds.parameter).Count()));
                    return errors;
                }
            }
            

            while (callChain.Any())
            {
                currentLink = callChain.Pop();

                ClassEntry referredClass = ((VarParamEntry)linkedVariable).getVariable().getClass();

                linkedVariable = referredClass.getChild().GetEntries().FirstOrDefault(x => (x is VarParamEntry || x is FunctionEntry) && x.getName() == currentLink.getValue());

                if (linkedVariable == null)
                {
                    errors.Add(string.Format("Undefined identifier {0} at line {1}", currentLink.getValue(), lastToken.getLine()));
                    return errors;
                }

                if (currentLink is VariableReferenceRecord)
                {
                    if (((VarParamEntry)linkedVariable).getVariable().GetDimensions().Count != ((VariableReferenceRecord)currentLink).getDimensions())
                    {
                        errors.Add(string.Format("Identifier {0} at line {1} does not have the correct number of indices. Counted {2} expected {3}"
                            , currentLink.getValue(), lastToken.getLine(), ((VariableReferenceRecord)currentLink).getDimensions(), ((VarParamEntry)linkedVariable).getVariable().GetDimensions().Count));
                        return errors;
                    }
                }
                else if (currentLink is FunctionCallRecord)
                {
                    if (((FunctionEntry)linkedVariable).getChild().GetEntries().Where(x => x.getKind() == EntryKinds.parameter).Count() != ((FunctionCallRecord)currentLink).GetParameterCount())
                    {
                        errors.Add(string.Format("Identifier {0} at line {1} does not have the correct number of parameters. Counted {2} expected {3}"
                            , currentLink.getValue(), lastToken.getLine(), ((FunctionCallRecord)currentLink).GetParameterCount(), ((FunctionEntry)linkedVariable).getChild().GetEntries().Where(x => x.getKind() == EntryKinds.parameter).Count()));
                        return errors;
                    }
                }
            }



            return new List<string>();
        }

        public override string getProductName()
        {
            return "Verify data members";
        }
    }
}
