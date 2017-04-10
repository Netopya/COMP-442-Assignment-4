using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment4.CodeGeneration
{
    // A class that can be used to request a unique, incrementing number
    class IDGenerator
    {
        private static int counter = 0;

        public static string GetNext()
        {
            return (counter++).ToString();
        }
    }
}
