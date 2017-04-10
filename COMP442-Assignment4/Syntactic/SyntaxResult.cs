using COMP442_Assignment4.CodeGeneration;
using COMP442_Assignment4.SymbolTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment4.Syntactic
{
    /*
        Class to store the results of a syntactic analysis

        For COMP 442 Assignment 2, 3 and 4 by Michael Bilinsky 26992358
    */
    public class SyntaxResult
    {
        public List<List<IProduceable>> Derivation;
        public List<string> Errors;
        public SymbolTable SymbolTable;
        public List<string> SemanticErrors;
        public MoonCodeResult MoonCode;

        public SyntaxResult(SymbolTable symbolTable)
        {
            Derivation = new List<List<IProduceable>>(); // The states of the parse stack during the parse
            Errors = new List<string>();
            SemanticErrors = new List<string>();

            this.SymbolTable = symbolTable;
        }
    }
}
