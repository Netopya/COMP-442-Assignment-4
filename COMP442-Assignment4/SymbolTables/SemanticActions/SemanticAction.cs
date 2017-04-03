using COMP442_Assignment4.Syntactic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP442_Assignment4.Tokens;
using COMP442_Assignment4.SymbolTables.SemanticRecords;
using COMP442_Assignment4.Lexical;

namespace COMP442_Assignment4.SymbolTables.SemanticActions
{
    /*
        A semantic action can be placed on the parse stack.
        When it is encountered its ExecuteSemanticAction will be called
    */
    public abstract class SemanticAction : IProduceable
    {
        // This funcion is executed when the symbol is consumed
        public abstract List<string> ExecuteSemanticAction(Stack<SemanticRecord> semanticRecordTable, Stack<SymbolTable> symbolTable, IToken lastToken, LinkedList<string> moonCode);

        public List<Token> getFirstSet()
        {
            // Nothing leads to a semantic action
            return new List<Token> { TokenList.Epsilon };
        }

        // Follow sets are not defined for non-terminal symbol
        public List<Token> getFollowSet()
        {
            throw new NotImplementedException();
        }

        public abstract string getProductName();

        public bool isTerminal()
        {
            return false;
        }
    }
}
