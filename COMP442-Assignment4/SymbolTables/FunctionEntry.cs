using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP442_Assignment4.SymbolTables.SemanticActions;

namespace COMP442_Assignment4.SymbolTables
{
    // An entry in a symbol table representing a function
    public class FunctionEntry : Entry
    {
        SymbolTable child;

        // The type of this function points to a declared class (including int or float)
        ClassEntry type;

        public FunctionEntry(SymbolTable parent, string name, ClassEntry type) : base(parent, EntryKinds.function, name)
        {
            // Create a symbol table for the function's own scope
            child = new SymbolTable("Function Symbol Table: " + name, this);
            this.type = type;
        }

        public override SymbolTable getChild()
        {
            return child;
        }

        public override string getType()
        {
            var parameters = child.GetEntries().Where(x => x.getKind() == EntryKinds.parameter);

            // List the function's parameters in the type if there are any
            if (parameters.Count() == 0)
                return type.getName();
            else
                return string.Format("{0} : {1}", type.getName(), string.Join(", ", parameters.Select(x => x.getType())));
        }

        public void AddParameters(IEnumerable<Variable> parameters)
        {
            // Create a new entry for each parameter and
            // they will add themselves to the current "child" symbol table
            foreach (var parameter in parameters)
            {
                new VarParamEntry(child, parameter, EntryKinds.parameter);
            }
        }
    }
}
