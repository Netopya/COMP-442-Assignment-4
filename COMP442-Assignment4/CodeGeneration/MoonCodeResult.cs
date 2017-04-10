using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment4.CodeGeneration
{
    // Stores moon code into different sections so that they can be outputed in the correct order
    public class MoonCodeResult
    {
        private readonly Dictionary<string, LinkedList<string>> codeMap = new Dictionary<string, LinkedList<string>>();
        private readonly LinkedList<string> globals = new LinkedList<string>();

        // Add a single line of mooncode to a section
        public void AddLine(string area, string line)
        {
            LinkedList<string> map = CheckAddKey(area);

            map.AddLast(line);
        }

        // Add multiple lines of moon code
        public void AddLines(string area, IEnumerable<string> lines)
        {
            LinkedList<string> map = CheckAddKey(area);

            foreach (string line in lines)
                map.AddLast(line);
        }

        // Add a line to the global section
        public void AddGlobal(string line)
        {
            AddLine("global", line);
        }

        public string GenerateCode()
        {
            StringBuilder code = new StringBuilder();
            string space = new string(' ', 16);

            code.AppendLine("globals");
            foreach(var line in globals)
            {
                code.Append(space);
                code.AppendLine(line);
            }

            foreach (var kvp in codeMap)
            {
                code.AppendLine(kvp.Key);

                foreach(var line in kvp.Value)
                {
                    code.Append(space);
                    code.AppendLine(line);
                }
            }

            return code.ToString();
        }

        // A helper function to check for the existence of a
        // key and return the appropriate linkedlist
        private LinkedList<string> CheckAddKey(string key)
        {
            if(key == "global")
            {
                return globals;
            }
            else
            {
                if (!codeMap.ContainsKey(key))
                    codeMap.Add(key, new LinkedList<string>());

                return codeMap[key];
            }
        }
    }
}
