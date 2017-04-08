using COMP442_Assignment4.SymbolTables.SemanticActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment4.SymbolTables
{
    // An entry in a symbol table representing a class
    public class ClassEntry : Entry
    {
        SymbolTable child;
        int defaultSize = 0;
        public ClassEntry(string name, SymbolTable parent) : base(parent, EntryKinds.classKind, name)
        {
            // Create a symbol table for the scope of this class
            child = new SymbolTable("Class Symbol Table: " + name, this);
        }

        // An unlinked (no parent) constructor if we need to create a hanging class (ex: a reference to an undefined class)
        public ClassEntry(string name, int defaultSize) : base(null, EntryKinds.classKind, name)
        {
            child = null;
            this.defaultSize = defaultSize;
        }

        public override SymbolTable getChild()
        {
            return child;
        }

        public override string getType()
        {
            return string.Empty;
        }

        public int GetSize()
        {
            if (child == null)
                return defaultSize;
            else
                return child.GetEntries().Where(x => x.getKind() == EntryKinds.variable).Sum(x => ((VarParamEntry)x).getVariable().GetSize());
        }

    }
}
