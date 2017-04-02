using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment3.SymbolTables.SemanticActions
{
    /*
        An entry in a symbol table that could be either
        a local/member variable or function parameter
    */
    class VarParamEntry : Entry
    {
        // The variable this entry represents
        Variable variable;

        public VarParamEntry(SymbolTable parent, Variable variable, EntryKinds kind) : base(parent, kind, variable.GetName())
        {
            this.variable = variable;
        }

        public override SymbolTable getChild()
        {
            return null;
        }

        public override string getType()
        {
            return variable.formatString();
        }
    }
}
