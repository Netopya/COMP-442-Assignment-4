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

        public ClassEntry(string name, SymbolTable parent) : base(parent, EntryKinds.classKind, name)
        {
            // Create a symbol table for the scope of this class
            child = new SymbolTable("Class Symbol Table: " + name, this);
        }

        // An unlinked (no parent) constructor if we need to create a hanging class (ex: a reference to an undefined class)
        public ClassEntry(string name) : base(null, EntryKinds.classKind, name)
        {
            child = null;
        }

        public override SymbolTable getChild()
        {
            return child;
        }

        public override string getType()
        {
            return string.Empty;
        }
    }
}
