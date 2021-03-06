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
    // Collect information regarding a function call and migrate all this information back to the semantic stack
    class MigrateFunctionCall : SemanticAction
    {
        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken, MoonCodeResult moonCode)
        {
            SemanticRecord top = semanticRecordTable.Pop();
            List<string> errors = new List<string>();
            LinkedList<ExpressionRecord> expressionParameters = new LinkedList<ExpressionRecord>();

            // Collect parameter expressions until we hit the function's name
            while (top.recordType != RecordTypes.IdNameReference)
            {
                if (top.recordType != RecordTypes.ExpressionType)
                {
                    errors.Add(string.Format("Grammar error at {0}. Migrating a function call should only count parameters, found {1}", lastToken.getLine(), top.recordType.ToString()));
                }
                else
                {
                    expressionParameters.AddFirst((ExpressionRecord)top);
                }

                top = semanticRecordTable.Pop();
            }

            // Create the new semantic record
            semanticRecordTable.Push(new FunctionCallRecord(top.getValue(), expressionParameters));

            return errors;
        }

        public override string getProductName()
        {
            return "Generate function call information";
        }
    }
}
