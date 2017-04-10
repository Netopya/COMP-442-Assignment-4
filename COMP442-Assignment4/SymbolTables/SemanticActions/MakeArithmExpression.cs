using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP442_Assignment4.Lexical;
using COMP442_Assignment4.SymbolTables.SemanticRecords;
using COMP442_Assignment4.Tokens;
using COMP442_Assignment4.CodeGeneration;

namespace COMP442_Assignment4.SymbolTables.SemanticActions
{
    // Generate moon code for all arithmetic and relational expressions
    class MakeArithmExpression : SemanticAction
    {
        private readonly Dictionary<Token, string> opLists = new Dictionary<Token, string>
        {
            { TokenList.Plus, "add"},
            { TokenList.Minus, "sub"},
            { TokenList.Asterisk, "mul"},
            { TokenList.Slash, "div"},
            { TokenList.DoubleEquals, "ceq" },
            { TokenList.NotEqual, "cne" },
            { TokenList.LessThan, "clt" },
            { TokenList.GreaterThan, "cgt" },
            { TokenList.LessThanOrEqual, "cle" },
            { TokenList.GreaterThanOrEqual, "cge" },
            { TokenList.And, "and" },
            { TokenList.Or, "or" }
        };

        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken, MoonCodeResult moonCode)
        {
            Token op = null;

            LinkedList<ExpressionRecord> expressions = new LinkedList<ExpressionRecord>();
            List<string> errors = new List<string>();

            // Accumulate the two last expressions on the semantic stack
            while(expressions.Count < 2)
            {
                if (!semanticRecordTable.Any())
                {
                    errors.Add(string.Format("Grammar error: Not enough expressions to validate arithmetic operation at line {0}", lastToken.getLine()));
                    return errors;
                }

                SemanticRecord record = semanticRecordTable.Pop();

                BasicTokenRecord tokenRec = record as BasicTokenRecord;
                if (tokenRec != null)
                {
                    // Save the token representing the operation
                    op = tokenRec.getToken();
                    continue;
                }
                
                if(record is ExpressionRecord)
                {
                    expressions.AddLast(record as ExpressionRecord);
                }
                else
                {
                    errors.Add(string.Format("Grammar error: record {0} at line {1} is not allowed during an arithmetic expression validation", record.recordType.ToString(), lastToken.getLine()));
                }
            }

            ExpressionRecord type1 = expressions.Last.Value;
            ExpressionRecord type2 = expressions.First.Value;

            // Ensure that both expressions are integers
            if (type1.GetExpressionType() != AddTypeToList.intClass || type2.GetExpressionType() != AddTypeToList.intClass)
                errors.Add(string.Format("Cannot perform arithmetic operation at line {0} between factors of type {1} and {2}"
                    , lastToken.getLine(), type1.GetExpressionType().getName(), type2.GetExpressionType().getName()));

            SymbolTable currentScope = symbolTable.Peek();
            string outAddress = string.Empty;

            if (currentScope.getParent() == null)
                errors.Add(string.Format("Cannot perform an arithemetic operation outside of a function"));
            else if(op == TokenList.And || op == TokenList.Or)
            {
                // Generate an address for the result of this sub-expression
                outAddress = Entry.MakeAddressForEntry(currentScope.getParent(), "arithmExpr");
                
                // Get a unique id for the jump label
                string jumpId = IDGenerator.GetNext();

                // Generate the code to perform a logic "and" or "or" instead of bitwise
                moonCode.AddGlobal(string.Format("{0} dw 0", outAddress));
                moonCode.AddLine(currentScope.getParent().getAddress(), string.Format(@"
                    lw r3, {0}(r0)
                    lw r4, {1}(r0)
                    {2} r2, r3, r4
                    bz r2, zero_{4}
                    addi r2, r0, 1
                    sw {3}(r0), r2
                    j endop_{4}
                    zero_{4} sw {3}(r0), r0
                    endop_{4}
                ", type1.GetAddress(), type2.GetAddress(), opLists[op], outAddress, jumpId));
            }
            else
            {
                // Generate an address for the result of this sub-expression
                outAddress = Entry.MakeAddressForEntry(currentScope.getParent(), "arithmExpr");

                // Generate code for a mathematical or relational expression
                moonCode.AddGlobal(string.Format("{0} dw 0", outAddress));
                moonCode.AddLine(currentScope.getParent().getAddress(),string.Format(@"
                    lw r3, {0}(r0)
                    lw r4, {1}(r0)
                    {2} r2, r3, r4
                    sw {3}(r0), r2
                ", type1.GetAddress(), type2.GetAddress(), opLists[op], outAddress));
            }
            
            // Place the resulting expression on the semantic stack
            semanticRecordTable.Push(new ExpressionRecord(AddTypeToList.intClass, outAddress));

            return errors;
        }

        public override string getProductName()
        {
            return "Generate moon code for an arithmetic expression";
        }
    }
}
