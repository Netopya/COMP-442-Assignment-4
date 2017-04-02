﻿using COMP442_Assignment4.Syntactic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment4.Tokens
{
    /*
        A producable symbol to represent a terminal symbol
    */
    public class Token : IProduceable
    {
        private string name;
        private string symbol;

        public Token(string name)
        {
            this.name = name;
        }

        public Token(string name, string symbol)
        {
            this.name = name;
            this.symbol = symbol;
        }

        // A terminal's first set is a set with itself in it
        public List<Token> getFirstSet()
        {
            return new List<Token> { this };
        }

        // A terminal's follow set it empty
        public List<Token> getFollowSet()
        {
            return new List<Token>();
        }

        public string getName()
        {
            return name;
        }

        // If the token has a particular symbol we want to display
        // return it instead of the name
        public string getProductName()
        {
            return string.IsNullOrEmpty(symbol) ? name : symbol;
        }

        public bool isTerminal()
        {
            return true;
        }
    }

    // A list of non-terminal symbols used by 
    // the grammar
    public static class TokenList
    {
        public static Token Error = new Token("Error");
        public static Token Asterisk = new Token("Asterisk", "*");
        public static Token LineComment = new Token("Line comment");
        public static Token BlockComment = new Token("Block comment");
        public static Token Slash = new Token("Slash", "/");

        public static Token OpenParanthesis = new Token("Open parenthesis", "(");
        public static Token CloseParanthesis = new Token("Close parenthesis", ")");
        public static Token OpenCurlyBracket = new Token("Open curly bracket", "{");
        public static Token CloseCurlyBracket = new Token("Close curly bracket", "}");
        public static Token OpenSquareBracket = new Token("Open square bracket", "[");
        public static Token CloseSquareBracket = new Token("Close square bracket", "]");

        public static Token GreaterThanOrEqual = new Token("Greater than or equal", ">=");
        public static Token GreaterThan = new Token("Greater than", ">");
        public static Token LessThanOrEqual = new Token("Less than or equal", "<=");
        public static Token NotEqual = new Token("Not equal", "<>");
        public static Token LessThan = new Token("Less than", "<");
        public static Token DoubleEquals = new Token("Double equals", "==");
        public static Token EqualsToken = new Token("Equals", "=");

        public static Token Period = new Token("Period", ".");
        public static Token SemiColon = new Token("Semi-colon", ";");
        public static Token Comma = new Token("Comma", ",");
        public static Token Plus = new Token("Plus", "+");
        public static Token Minus = new Token("Minus", "-");

        public static Token Float = new Token("Float", "float");
        public static Token Integer = new Token("Integer", "integer");

        public static Token Identifier = new Token("Identifier", "id");

        public static Token And = new Token("and");
        public static Token Not = new Token("not");
        public static Token Or = new Token("or");
        public static Token If = new Token("if");
        public static Token Then = new Token("then");
        public static Token Else = new Token("else");
        public static Token For = new Token("for");
        public static Token Class = new Token("class");
        public static Token IntRes = new Token("int", "int");
        public static Token FloatRes = new Token("float", "float");
        public static Token Get = new Token("get");
        public static Token Put = new Token("put");
        public static Token Return = new Token("return");
        public static Token Program = new Token("program");

        public static Token EndOfProgram = new Token("$");
        public static Token Epsilon = new Token("ε");
    }

}
