﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using COMP442_Assignment4.Tokens;

namespace COMP442_Assignment4.Lexical
{
    /*
        A generic token class to hold a
        token's name,  line number, and lexeme
    */
    class SimpleToken : IToken
    {
        protected Token _token;
        private string _content = string.Empty;
        protected bool _showContent = false;
        private int _line = -1;

        public SimpleToken(Token token, bool showContent)
        {
            _token = token;
            _showContent = showContent;
        }

        // Create a human readable string for this token, with
        // the lexeme if appropriate
        public string getName()
        {
            if(_showContent)
            {
                return string.Format("<{0} ({1}) Line: {2}>", _token.getName(), _content, _line);
            }
            else
            {
                return string.Format("<{0} Line: {1}>", _token.getName(), _line);
            }
        }

        public virtual void setInfo(string content, int line)
        {
            _content = content;
            _line = line;
        }

        public virtual bool isError()
        {
            return false;
        }

        public Token getToken()
        {
            return _token;
        }

        public int getLine()
        {
            return _line;
        }

        // Name to be used by the semantic analyzer
        public string getSemanticName()
        {
            return _showContent ? _content : _token.getName();
        }
    }
}
