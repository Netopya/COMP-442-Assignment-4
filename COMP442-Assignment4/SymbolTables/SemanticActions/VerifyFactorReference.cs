﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP442_Assignment4.Lexical;
using COMP442_Assignment4.SymbolTables.SemanticRecords;
using COMP442_Assignment4.CodeGeneration;

namespace COMP442_Assignment4.SymbolTables.SemanticActions
{
    // Verify a factor that could be a chain of variables with a possible function call
    // Ensure identifiers are declared, arrays are indexed properly, and parameters have the correct type
    class VerifyFactorReference : SemanticAction
    {
        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken, MoonCodeResult moonCode)
        {
            Stack<SemanticRecord> callChain = new Stack<SemanticRecord>();
            List<string> errors = new List<string>();

            if (!semanticRecordTable.Any())
            {
                errors.Add(string.Format("Grammar error at line {0}: could not verify factor for emtpy stack", lastToken.getLine()));
                return errors;
            }

            SemanticRecord lastRecord = semanticRecordTable.Pop();

            // Accumulate semantic records until we reach the start of the factor
            while (lastRecord.recordType != RecordTypes.FactorStart)
            {
                callChain.Push(lastRecord);

                if (!semanticRecordTable.Any())
                {
                    errors.Add(string.Format("Grammar error at {0}. Could not find start for a factor", lastToken.getLine()));
                    break;
                }

                lastRecord = semanticRecordTable.Pop();
            }

            SemanticRecord currentLink = callChain.Pop();
            Entry linkedVariable = null;

            // The first element in the chain is a reference to a locally accessible variable or function
            // Go through symbol tables of increasing scope until we find the referenced identifier
            foreach (SymbolTable table in symbolTable)
            {
                linkedVariable = table.GetEntries().FirstOrDefault(x => (x is VarParamEntry || x is FunctionEntry) && x.getName() == currentLink.getValue());

                if (linkedVariable != null)
                    break;
            }

            // Verify the validity of this initial reference
            bool success = VerifyLink(currentLink, linkedVariable, lastToken, errors);

            // Go through the call chain and verify each link
            while (callChain.Any() && success)
            {
                currentLink = callChain.Pop();

                ClassEntry referredClass = ((VarParamEntry)linkedVariable).getVariable().getClass();

                if (referredClass.getChild() == null)
                {
                    errors.Add(string.Format("Identifier {0} cannot be reached at line {1}", currentLink.getValue(), lastToken.getLine()));
                    return errors;
                }

                // Look in the referred class for the variable or function that is being referred to
                linkedVariable = referredClass.getChild().GetEntries().FirstOrDefault(x => (x is VarParamEntry || x is FunctionEntry) && x.getName() == currentLink.getValue());

                success = VerifyLink(currentLink, linkedVariable, lastToken, errors);
            }

            ClassEntry expressionType = new ClassEntry("undefined", 0);

            // Find the linked variable and create a new expression for it
            if (linkedVariable is VarParamEntry)
                expressionType = ((VarParamEntry)linkedVariable).getVariable().getClass();
            else if(linkedVariable is FunctionEntry)
                expressionType = ((FunctionEntry)linkedVariable).GetReturnType();

            if (linkedVariable == null)
                errors.Add(string.Format("Grammar error: Could not link variable at line {0}", lastToken.getLine()));
            else
                semanticRecordTable.Push(new ExpressionRecord(expressionType, linkedVariable.getAddress()));

            return errors;
        }

        public override string getProductName()
        {
            return "Verify data members";
        }
        
        // A helper function to verify that a reference to a variable or function
        // as the correct indicies or parameters
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
                var parameters = GetParameters(linkedVariable as FunctionEntry);

                // Verify the number of paramters
                if (parameters.Count() != ((FunctionCallRecord)currentLink).GetParameterCount())
                {
                    errors.Add(string.Format("Function call {0} at line {1} does not have the correct number of parameters. Counted {2} expected {3}"
                        , currentLink.getValue(), lastToken.getLine(), ((FunctionCallRecord)currentLink).GetParameterCount(), parameters.Count()));
                    return false;
                }
                else
                {
                    var parameterComparers = parameters.Zip(((FunctionCallRecord)currentLink).GetParameters(), (f, a) => new { aparam = a, fparam = (VarParamEntry)f });

                    // Verify that the parameters are of the correct type
                    foreach(var parameterCompare in parameterComparers)
                    {
                        if (parameterCompare.aparam.GetExpressionType() != parameterCompare.fparam.getVariable().getClass())
                            errors.Add(string.Format("Cannot convert {0} to {1} in parameter at line {2}"
                                , parameterCompare.aparam.GetExpressionType().getName(), parameterCompare.fparam.getVariable().getClass().getName(), lastToken.getLine()));
                    }
                }

                return true;
            }
            else
            {
                errors.Add("Grammar error: Illegal record on semantic stack");
                return false;
            }
        }

        // Get the parameters for a function
        private IEnumerable<Entry> GetParameters(FunctionEntry enty)
        {
            return enty.getChild().GetEntries().Where(x => x.getKind() == EntryKinds.parameter);
        }
    }
}
