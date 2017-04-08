using COMP442_Assignment4.CodeGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment4.SymbolTables
{
    // The different kinds of symbol table entries
    public enum EntryKinds
    {
        function,
        classKind,
        parameter,
        variable
    };

    // An entry in the symbol table
    public abstract class Entry
    {
        bool declared;

        // The symbol table holding this entry
        SymbolTable parent;

        EntryKinds kind;
        string name;
        string address;

        public Entry(SymbolTable parent, EntryKinds kind, string name)
        {
            this.parent = parent;

            // Establish a two-way link in the tree
            if(parent != null)
                parent.AddEntry(this);

            this.kind = kind;
            this.name = name;

            LinkedList<string> names = new LinkedList<string>();
            Entry current = this;

            while(current != null)
            {
                names.AddFirst(current.getName());

                if (current.getParent() == null)
                    break;

                current = current.getParent().getParent();
            }

            names.AddFirst(IDGenerator.GetNext());

            address = string.Join("_", names);
        }

        // Get a symbol table for the inner scope of this entry
        public abstract SymbolTable getChild();

        public abstract string getType();

        // Create a readable string
        public void printTable(int tabs, StringBuilder sb)
        {
            sb.Append(new String('\t', tabs));

            sb.AppendFormat("name: {0}, kind: {1}", name, kind.ToString());

            string type = getType();

            if(type != string.Empty)
            {
                sb.AppendFormat(", type: {0}", type);
            }

            sb.AppendLine();

            if(getChild() != null)
                getChild().printTable(tabs + 1, sb);
        }

        public EntryKinds getKind()
        {
            return kind;
        }

        public string getName()
        {
            return name;
        }

        protected SymbolTable getParent()
        {
            return parent;
        }

        public string getAddress()
        {
            return address;
        }
    }
}
