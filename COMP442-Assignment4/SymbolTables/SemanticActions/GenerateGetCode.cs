using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP442_Assignment4.CodeGeneration;
using COMP442_Assignment4.Lexical;
using COMP442_Assignment4.SymbolTables.SemanticRecords;

namespace COMP442_Assignment4.SymbolTables.SemanticActions
{
    class GenerateGetCode : SemanticAction
    {
        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken, MoonCodeResult moonCode)
        {
            List<string> errors = new List<string>();
            if (!semanticRecordTable.Any())
            {
                errors.Add(string.Format("Grammar error at line {0}, semantic stack empty with creating a put command", lastToken.getLine()));
                return errors;
            }

            ExpressionRecord expr = semanticRecordTable.Pop() as ExpressionRecord;

            if (expr == null)
            {
                errors.Add(string.Format("Grammar error at line {0}, can only put expressions", lastToken.getLine()));
                return errors;
            }

            if (!symbolTable.Any() || symbolTable.Peek().getParent() == null)
            {
                errors.Add(string.Format("Cannot place put command in empty scope"));
                return errors;
            }


            moonCode.AddLine(symbolTable.Peek().getParent().getAddress(), string.Format(@"
                getc r2
                sw {0}(r0), r2
            ", expr.GetAddress()));

            return errors;
        }

        public override string getProductName()
        {
            return "Generate code for the get command";
        }
    }
}
