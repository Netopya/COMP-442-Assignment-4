﻿using COMP442_Assignment4.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment4.Lexical
{
    /*
        This interface represents a token in the language
        with a name, a lexeme, and a line number
    */
    public interface IToken
    {
        string getName();
        void setInfo(string content, int line);
        bool isError();
        Token getToken();
        int getLine();
        string getSemanticName();
    }
}
