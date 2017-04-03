using COMP442_Assignment4.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment4.SymbolTables.SemanticRecords
{
    class BasicTokenRecord : SemanticRecord
    {
        Token token;

        public BasicTokenRecord(Token token) : base(RecordTypes.BasicToken, token.getProductName())
        {
            this.token = token;
        }

        public Token getToken()
        {
            return token;
        }
    }
}
