﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment4.SymbolTables
{
    // A class to hold the structure of a variable (name, type, and array size description)
    public class Variable
    {
        // The type of this variable points to a declared class (including int or float)
        ClassEntry type;

        string name;

        // A list of sizes for each dimension in the array
        LinkedList<int> dimensions = new LinkedList<int>();

        public void SetName(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }

        public void SetType(ClassEntry type)
        {
            this.type = type;
        }

        public ClassEntry getClass()
        {
            return type;
        }

        // Add a dimension to the front
        public void AddDimension(int dimension)
        {
            dimensions.AddFirst(dimension);
        }

        public LinkedList<int> GetDimensions()
        {
            return dimensions;
        }

        // Create a reable string
        public string formatString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(type.getName());
            foreach(int dimension in dimensions)
            {
                sb.AppendFormat("[{0}]", dimension);
            }

            return sb.ToString();
        }

        public int GetSize()
        {
            return type.GetSize() * dimensions.Aggregate(1, (r, x) => r * x);
        }
    }
}
