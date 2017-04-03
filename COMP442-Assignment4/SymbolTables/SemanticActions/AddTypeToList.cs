using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP442_Assignment4.Lexical;
using COMP442_Assignment4.SymbolTables.SemanticRecords;

namespace COMP442_Assignment4.SymbolTables.SemanticActions
{
    // Add a type specification to the semantic stack
    // along with a reference to the type's definition
    public class AddTypeToList : SemanticAction
    {
        // Predefined class entries for int and float types
        public static ClassEntry intClass = new ClassEntry("int");
        public static ClassEntry floatClass = new ClassEntry("float");

        public override List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken, List<string> moonCode)
        {
            ClassEntry typeClass = null;
            List<string> errors = new List<string>();
            string searchType = lastToken.getSemanticName();

            // Check if the type is of int or float
            if (lastToken.getToken() == Tokens.TokenList.IntRes)
            {
                typeClass = intClass;
            }
            else if(lastToken.getToken() == Tokens.TokenList.FloatRes)
            {
                typeClass = floatClass;
            }
            // Check if we are recursively using a type defined in the immediate parent
            else if(symbolTable.Any() && symbolTable.Peek().getParent() != null && symbolTable.Peek().getParent().getName() == searchType)
            {
                errors.Add(string.Format("{0}'s member variable or function parameter cannot refer to its own class at line {1}", searchType, lastToken.getLine()));
                typeClass = symbolTable.Peek().getParent() as ClassEntry;
            }
            else
            {
                // Find the type being referenced in the parent scopes
                foreach (SymbolTable table in symbolTable)
                {
                    // Look through this table's entry list for a type
                    typeClass = table.GetEntries().FirstOrDefault(x => x.getKind() == EntryKinds.classKind && x.getName() == searchType) as ClassEntry;

                    if (typeClass != null)
                        break;
                }
            }
            

            if(typeClass != null)
                semanticRecordTable.Push(new SemanticRecord(typeClass));
            else
            {
                errors.Add(string.Format("Type name: {0} does not exist at line {1}", searchType, lastToken.getLine()));
                semanticRecordTable.Push(new SemanticRecord(new ClassEntry(searchType)));
            }

            return errors;
        }

        public override string getProductName()
        {
            return "Add a named type to the semantic stack";
        }
    }
}
