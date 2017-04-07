using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP442_Assignment4.SymbolTables.SemanticRecords
{
    public enum RecordTypes
    {
        TypeName,
        IdName,
        Size,
        Variable,
        ConstNum,
        BasicToken,
        IndiceCount,
        VariableReference,
        FactorStart,
        IdNameReference,
        FunctionParamCount,
        FunctionCall,
        ExpressionType
    }

    // A semantic record stores information on a semantic stack
    // for use my semantic actions
    public class SemanticRecord
    {
        public RecordTypes recordType
        {
            get; set;
        }

        // Different types of information that the record can store
        string value;
        Variable variable;
        ClassEntry className;

        public SemanticRecord(RecordTypes recordType, string value)
        {
            this.recordType = recordType;
            this.value = value;
        }

        public SemanticRecord(Variable variable)
        {
            this.variable = variable;
            this.recordType = RecordTypes.Variable;
        }

        public SemanticRecord(ClassEntry className)
        {
            this.className = className;
            this.recordType = RecordTypes.TypeName;
        }

        public string getValue()
        {
            return value;
        }
      
        public Variable getVariable()
        {
            return variable;
        }

        public ClassEntry getType()
        {
            return className;
        }
    }

}
