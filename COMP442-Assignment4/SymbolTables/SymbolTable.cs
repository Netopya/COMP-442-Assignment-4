using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment4.SymbolTables
{
    /*
        Represents a symbol table in the tree of symbol
        tables for the program
    */
    public class SymbolTable
    {
        string name;

        // A possible parent entry that links to this symbol table
        Entry parent;

        // The list of entries at this scope
        List<Entry> entries = new List<Entry>();

        public SymbolTable(string name, Entry parent)
        {
            this.name = name;
            this.parent = parent;
        }

        public void AddEntry(Entry entry)
        {
            entries.Add(entry);
        }

        public List<Entry> GetEntries()
        {
            return entries;
        }

        public string printTable()
        {
            StringBuilder sb = new StringBuilder();

            printTable(0, sb);

            return sb.ToString();
        }

        public void printTable(int tabs, StringBuilder sb)
        {
            sb.Append(new String('\t', tabs));
            sb.AppendLine(name);

            foreach(var entry in entries)
            {
                entry.printTable(tabs, sb);
            }
        }

        public Entry getParent()
        {
            return parent;
        }
    }
}