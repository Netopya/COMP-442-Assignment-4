using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using COMP442_Assignment4.Lexical;
using COMP442_Assignment4.Syntactic;
using System.Linq;
using COMP442_Assignment4.SymbolTables;

namespace Assignment3_UnitTests
{
    /*
       A set of unit tests to test the symbol table generator

       For COMP 442 Assignment 3, Michael Bilinsky 26992358
    */
    [TestClass]
    public class UnitTest3
    {
        LexicalAnalyzer lexicalAnalyzer = new LexicalAnalyzer();
        SyntacticAnalyzer syntacticAnalyzer = new SyntacticAnalyzer();

        private string formatSymbolTable(SymbolTable symbolTable)
        {
            return symbolTable.printTable().Replace("\t", "").Replace(Environment.NewLine, " ").Trim();
        }

        [TestMethod]
        public void TestSampleCode()
        {
            // Analyze sample code from assignment 3
            var tokens = lexicalAnalyzer.Tokenize("class MyClass1 { int mc1v1[2][4]; float mc1v2; MyClass2 mc1v3[3]; int mc1f1(int p1, MyClass2 p2[3]) { MyClass2 fv1[3]; }; int f2(MyClass1 f2p1[3]) { int mc1v1; }; }; class MyClass2 { int mc1v1[2][4]; float fp1; MyClass2 m2[3]; }; program { int m1; float m2[3][2]; MyClass2 m3[2]; }; float f1(int fp1[2][2], float fp2) { MyClass1 fv1[3]; int fv2; }; int f2() { };");
            var results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: MyClass1, kind: classKind Class Symbol Table: MyClass1 name: mc1v1, kind: variable, type: int[2][4] name: mc1v2, kind: variable, type: float name: mc1v3, kind: variable, type: MyClass2[3] name: mc1f1, kind: function, type: int : int, MyClass2[3] Function Symbol Table: mc1f1 name: p1, kind: parameter, type: int name: p2, kind: parameter, type: MyClass2[3] name: fv1, kind: variable, type: MyClass2[3] name: f2, kind: function, type: int : MyClass1[3] Function Symbol Table: f2 name: f2p1, kind: parameter, type: MyClass1[3] name: mc1v1, kind: variable, type: int name: MyClass2, kind: classKind Class Symbol Table: MyClass2 name: mc1v1, kind: variable, type: int[2][4] name: fp1, kind: variable, type: float name: m2, kind: variable, type: MyClass2[3] name: program, kind: function Function Symbol Table: program name: m1, kind: variable, type: int name: m2, kind: variable, type: float[3][2] name: m3, kind: variable, type: MyClass2[2] name: f1, kind: function, type: float : int[2][2], float Function Symbol Table: f1 name: fp1, kind: parameter, type: int[2][2] name: fp2, kind: parameter, type: float name: fv2, kind: variable, type: int name: fv1, kind: variable, type: MyClass1[3] name: f2, kind: function, type: int Function Symbol Table: f2", formatSymbolTable(results.SymbolTable));
            Assert.AreEqual("Type name: MyClass2 does not exist at line 1 Type name: MyClass2 does not exist at line 1 Type name: MyClass2 does not exist at line 1 MyClass1's member variable or function parameter cannot refer to its own class at line 1 Identifier mc1v1 at line 1 has already been declared MyClass2's member variable or function parameter cannot refer to its own class at line 1", string.Join(" ",results.SemanticErrors));

            // Analyze sample code from assignment 2
            tokens = lexicalAnalyzer.Tokenize("class Utility { int var1[4][5][7][8][9][1][0]; float var2; int findMax(int array[100]) { int maxValue; int idx; maxValue = array[100]; for( int idx = 99; idx > 0; idx = idx - 1 ) { if(array[idx] > maxValue) then { maxValue = array[idx]; }else{}; }; return (maxValue); }; int findMin(int array[100]) { int minValue; int idx; minValue = array[100]; for( int idx = 1; idx <= 99; idx = ( idx ) + 1) { if(array[idx] < maxValue) then { maxValue = array[idx]; }else{}; }; return (minValue); }; }; program { int sample[100]; int idx; int maxValue; int minValue; Utility utility; Utility arrayUtility[2][3][6][7]; for(int t = 0; t<=100 ; t = t + 1) { get(sample[t]); sample[t] = (sample[t] * randomize()); }; maxValue = utility.findMax(sample); minValue = utility.findMin(sample); utility. var1[4][1][0][0][0][0][0] = 10; arrayUtility[1][1][1][1].var1[4][1][0][0][0][0][0] = 2; put(maxValue); put(minValue); }; float randomize() { float value; value = 100 * (2 + 3.0 / 7.0006); value = 1.05 + ((2.04 * 2.47) - 3.0) + 7.0006 ; return (value); };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: Utility, kind: classKind Class Symbol Table: Utility name: var1, kind: variable, type: int[4][5][7][8][9][1][0] name: var2, kind: variable, type: float name: findMax, kind: function, type: int : int[100] Function Symbol Table: findMax name: array, kind: parameter, type: int[100] name: maxValue, kind: variable, type: int name: idx, kind: variable, type: int name: idx, kind: variable, type: int name: findMin, kind: function, type: int : int[100] Function Symbol Table: findMin name: array, kind: parameter, type: int[100] name: minValue, kind: variable, type: int name: idx, kind: variable, type: int name: idx, kind: variable, type: int name: program, kind: function Function Symbol Table: program name: sample, kind: variable, type: int[100] name: idx, kind: variable, type: int name: maxValue, kind: variable, type: int name: minValue, kind: variable, type: int name: t, kind: variable, type: int name: arrayUtility, kind: variable, type: Utility[2][3][6][7] name: utility, kind: variable, type: Utility name: randomize, kind: function, type: float Function Symbol Table: randomize name: value, kind: variable, type: float", formatSymbolTable(results.SymbolTable));
            Assert.AreEqual("Identifier idx at line 1 has already been declared Identifier idx at line 1 has already been declared", string.Join(" ", results.SemanticErrors));
        }

        [TestMethod]
        public void TestClass()
        {
            var tokens = lexicalAnalyzer.Tokenize("class MyClass1 { }; program { };");
            var results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: MyClass1, kind: classKind Class Symbol Table: MyClass1 name: program, kind: function Function Symbol Table: program", formatSymbolTable(results.SymbolTable));
            Assert.IsFalse(results.SemanticErrors.Any());

            // Test duplicate class name
            tokens = lexicalAnalyzer.Tokenize("class MyClass1 { }; class MyClass1 { }; program { };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: MyClass1, kind: classKind Class Symbol Table: MyClass1 name: MyClass1, kind: classKind Class Symbol Table: MyClass1 name: program, kind: function Function Symbol Table: program", formatSymbolTable(results.SymbolTable));
            Assert.AreEqual(1, results.SemanticErrors.Count);
            Assert.AreEqual("Identifier MyClass1 at line 1 has already been declared", results.SemanticErrors[0]);
        }

        [TestMethod]
        public void TestFunctions()
        {
            // Test function in class
            var tokens = lexicalAnalyzer.Tokenize("class MyClass1 { int func() { }; }; program { };");
            var results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: MyClass1, kind: classKind Class Symbol Table: MyClass1 name: func, kind: function, type: int Function Symbol Table: func name: program, kind: function Function Symbol Table: program", formatSymbolTable(results.SymbolTable));
            Assert.IsFalse(results.SemanticErrors.Any());

            // Test function in func list
            tokens = lexicalAnalyzer.Tokenize("program { }; int func() { };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: program, kind: function Function Symbol Table: program name: func, kind: function, type: int Function Symbol Table: func", formatSymbolTable(results.SymbolTable));
            Assert.IsFalse(results.SemanticErrors.Any());

            // Test function with float type
            tokens = lexicalAnalyzer.Tokenize("program { }; float func() { };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: program, kind: function Function Symbol Table: program name: func, kind: function, type: float Function Symbol Table: func", formatSymbolTable(results.SymbolTable));
            Assert.IsFalse(results.SemanticErrors.Any());

            // Test function with class type
            tokens = lexicalAnalyzer.Tokenize("class MyClass1 { }; program { }; float func() { };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: MyClass1, kind: classKind Class Symbol Table: MyClass1 name: program, kind: function Function Symbol Table: program name: func, kind: function, type: float Function Symbol Table: func", formatSymbolTable(results.SymbolTable));
            Assert.IsFalse(results.SemanticErrors.Any());

            // Test function with undeclared type
            tokens = lexicalAnalyzer.Tokenize("program { }; foobar func() { };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: program, kind: function Function Symbol Table: program name: func, kind: function, type: foobar Function Symbol Table: func", formatSymbolTable(results.SymbolTable));
            Assert.AreEqual(1, results.SemanticErrors.Count);
            Assert.AreEqual("Type name: foobar does not exist at line 1", results.SemanticErrors[0]);

            // Test duplicate function name
            tokens = lexicalAnalyzer.Tokenize("program { }; int func() { }; float func() { };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: program, kind: function Function Symbol Table: program name: func, kind: function, type: int Function Symbol Table: func name: func, kind: function, type: float Function Symbol Table: func", formatSymbolTable(results.SymbolTable));
            Assert.AreEqual(1, results.SemanticErrors.Count);
            Assert.AreEqual("Identifier func at line 1 has already been declared", results.SemanticErrors[0]);

            // Test duplicate identifier
            tokens = lexicalAnalyzer.Tokenize("class MyClass1 { float func; float func() { }; }; program { };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: MyClass1, kind: classKind Class Symbol Table: MyClass1 name: func, kind: variable, type: float name: func, kind: function, type: float Function Symbol Table: func name: program, kind: function Function Symbol Table: program", formatSymbolTable(results.SymbolTable));
            Assert.AreEqual(1, results.SemanticErrors.Count);
            Assert.AreEqual("Identifier func at line 1 has already been declared", results.SemanticErrors[0]);

            // Test recursive function type
            tokens = lexicalAnalyzer.Tokenize("class MyClass1 { MyClass1 func() { }; }; program { };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: MyClass1, kind: classKind Class Symbol Table: MyClass1 name: func, kind: function, type: MyClass1 Function Symbol Table: func name: program, kind: function Function Symbol Table: program", formatSymbolTable(results.SymbolTable));
            Assert.AreEqual(1, results.SemanticErrors.Count);
            Assert.AreEqual("MyClass1's member variable or function parameter cannot refer to its own class at line 1", results.SemanticErrors[0]);

            // Test duplicate function name in different scope
            tokens = lexicalAnalyzer.Tokenize("class MyClass1 { int func() { }; }; program { }; int func() { };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: MyClass1, kind: classKind Class Symbol Table: MyClass1 name: func, kind: function, type: int Function Symbol Table: func name: program, kind: function Function Symbol Table: program name: func, kind: function, type: int Function Symbol Table: func", formatSymbolTable(results.SymbolTable));
            Assert.IsFalse(results.SemanticErrors.Any());

            // Test duplicate name with class
            tokens = lexicalAnalyzer.Tokenize("class classyFunc { }; program { }; int classyFunc() { };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: classyFunc, kind: classKind Class Symbol Table: classyFunc name: program, kind: function Function Symbol Table: program name: classyFunc, kind: function, type: int Function Symbol Table: classyFunc", formatSymbolTable(results.SymbolTable));
            Assert.AreEqual(1, results.SemanticErrors.Count);
            Assert.AreEqual("Identifier classyFunc at line 1 has already been declared", results.SemanticErrors[0]);
        }

        [TestMethod]
        public void TestVariables()
        {
            // Test member variable
            var tokens = lexicalAnalyzer.Tokenize("class MyClass1 { int myVar; }; program { };");
            var results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: MyClass1, kind: classKind Class Symbol Table: MyClass1 name: myVar, kind: variable, type: int name: program, kind: function Function Symbol Table: program", formatSymbolTable(results.SymbolTable));
            Assert.IsFalse(results.SemanticErrors.Any());

            // Test variable in class function
            tokens = lexicalAnalyzer.Tokenize("class MyClass1 { float func() { int myVar; }; }; program { };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: MyClass1, kind: classKind Class Symbol Table: MyClass1 name: func, kind: function, type: float Function Symbol Table: func name: myVar, kind: variable, type: int name: program, kind: function Function Symbol Table: program", formatSymbolTable(results.SymbolTable));
            Assert.IsFalse(results.SemanticErrors.Any());

            // Test variable in program
            tokens = lexicalAnalyzer.Tokenize("program { int myVar; };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: program, kind: function Function Symbol Table: program name: myVar, kind: variable, type: int", formatSymbolTable(results.SymbolTable));
            Assert.IsFalse(results.SemanticErrors.Any());

            // Test variable in func body function
            tokens = lexicalAnalyzer.Tokenize("program { }; float myFunc() { int myVar; };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: program, kind: function Function Symbol Table: program name: myFunc, kind: function, type: float Function Symbol Table: myFunc name: myVar, kind: variable, type: int", formatSymbolTable(results.SymbolTable));
            Assert.IsFalse(results.SemanticErrors.Any());

            // Test float variable
            tokens = lexicalAnalyzer.Tokenize("program { float myVar; };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: program, kind: function Function Symbol Table: program name: myVar, kind: variable, type: float", formatSymbolTable(results.SymbolTable));
            Assert.IsFalse(results.SemanticErrors.Any());

            // Test class variable
            tokens = lexicalAnalyzer.Tokenize("class MyClass1 { }; program { MyClass1 myVar; };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: MyClass1, kind: classKind Class Symbol Table: MyClass1 name: program, kind: function Function Symbol Table: program name: myVar, kind: variable, type: MyClass1", formatSymbolTable(results.SymbolTable));
            Assert.IsFalse(results.SemanticErrors.Any());

            // Test arrays
            tokens = lexicalAnalyzer.Tokenize("program { int myVar[1][3]; };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: program, kind: function Function Symbol Table: program name: myVar, kind: variable, type: int[1][3]", formatSymbolTable(results.SymbolTable));
            Assert.IsFalse(results.SemanticErrors.Any());

            // Test class array
            tokens = lexicalAnalyzer.Tokenize("class MyClass1 { }; program { MyClass1 myVar[1][3]; };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: MyClass1, kind: classKind Class Symbol Table: MyClass1 name: program, kind: function Function Symbol Table: program name: myVar, kind: variable, type: MyClass1[1][3]", formatSymbolTable(results.SymbolTable));
            Assert.IsFalse(results.SemanticErrors.Any());

            // Test duplicate variable names in different scopes
            tokens = lexicalAnalyzer.Tokenize("program { int myVar; }; int myFunc() { int myVar; };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: program, kind: function Function Symbol Table: program name: myVar, kind: variable, type: int name: myFunc, kind: function, type: int Function Symbol Table: myFunc name: myVar, kind: variable, type: int", formatSymbolTable(results.SymbolTable));
            Assert.IsFalse(results.SemanticErrors.Any());

            // Test duplicate names in same function
            tokens = lexicalAnalyzer.Tokenize("program { int myVar; float myVar; };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: program, kind: function Function Symbol Table: program name: myVar, kind: variable, type: int name: myVar, kind: variable, type: float", formatSymbolTable(results.SymbolTable));
            Assert.AreEqual(1, results.SemanticErrors.Count);
            Assert.AreEqual("Identifier myVar at line 1 has already been declared", results.SemanticErrors[0]);

            // Test duplicate names in same scope
            tokens = lexicalAnalyzer.Tokenize("class MyClass1 { int myVar; int myFunc() { float myVar; }; }; program { };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: MyClass1, kind: classKind Class Symbol Table: MyClass1 name: myVar, kind: variable, type: int name: myFunc, kind: function, type: int Function Symbol Table: myFunc name: myVar, kind: variable, type: float name: program, kind: function Function Symbol Table: program", formatSymbolTable(results.SymbolTable));
            Assert.AreEqual(1, results.SemanticErrors.Count);
            Assert.AreEqual("Identifier myVar at line 1 has already been declared", results.SemanticErrors[0]);

            // Test for loops
            tokens = lexicalAnalyzer.Tokenize("program { for(int i = 0; i < 10; i = i + 1); };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: program, kind: function Function Symbol Table: program name: i, kind: variable, type: int", formatSymbolTable(results.SymbolTable));
            Assert.IsFalse(results.SemanticErrors.Any());
        }

        [TestMethod]
        public void TestParameters()
        {
            // Test basic parameters
            var tokens = lexicalAnalyzer.Tokenize("program { }; int myFunc(int param1) { };");
            var results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: program, kind: function Function Symbol Table: program name: myFunc, kind: function, type: int : int Function Symbol Table: myFunc name: param1, kind: parameter, type: int", formatSymbolTable(results.SymbolTable));
            Assert.IsFalse(results.SemanticErrors.Any());

            // Test multiple parameters
            tokens = lexicalAnalyzer.Tokenize("program { }; int myFunc(int param1, float param2) { };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: program, kind: function Function Symbol Table: program name: myFunc, kind: function, type: int : int, float Function Symbol Table: myFunc name: param1, kind: parameter, type: int name: param2, kind: parameter, type: float", formatSymbolTable(results.SymbolTable));
            Assert.IsFalse(results.SemanticErrors.Any());

            // Test parameters with class type
            tokens = lexicalAnalyzer.Tokenize("class MyClass1 { }; program { }; int myFunc(MyClass1 param1, float param2) { };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: MyClass1, kind: classKind Class Symbol Table: MyClass1 name: program, kind: function Function Symbol Table: program name: myFunc, kind: function, type: int : MyClass1, float Function Symbol Table: myFunc name: param1, kind: parameter, type: MyClass1 name: param2, kind: parameter, type: float", formatSymbolTable(results.SymbolTable));
            Assert.IsFalse(results.SemanticErrors.Any());

            // Test function parameters in class
            tokens = lexicalAnalyzer.Tokenize("class MyClass1 { int myFunc(int param1, float param2) { }; }; program { };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: MyClass1, kind: classKind Class Symbol Table: MyClass1 name: myFunc, kind: function, type: int : int, float Function Symbol Table: myFunc name: param1, kind: parameter, type: int name: param2, kind: parameter, type: float name: program, kind: function Function Symbol Table: program", formatSymbolTable(results.SymbolTable));
            Assert.IsFalse(results.SemanticErrors.Any());

            // Test function parameters with function variables
            tokens = lexicalAnalyzer.Tokenize("program { }; int myFunc(int param1) { float myVar; };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: program, kind: function Function Symbol Table: program name: myFunc, kind: function, type: int : int Function Symbol Table: myFunc name: param1, kind: parameter, type: int name: myVar, kind: variable, type: float", formatSymbolTable(results.SymbolTable));
            Assert.IsFalse(results.SemanticErrors.Any());

            // Test function with array parameters
            tokens = lexicalAnalyzer.Tokenize("program { }; int myFunc(int param1[1][2]) { };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: program, kind: function Function Symbol Table: program name: myFunc, kind: function, type: int : int[1][2] Function Symbol Table: myFunc name: param1, kind: parameter, type: int[1][2]", formatSymbolTable(results.SymbolTable));
            Assert.IsFalse(results.SemanticErrors.Any());

            // Test function with multiple array parameters
            tokens = lexicalAnalyzer.Tokenize("class MyClass1 { }; program { }; int myFunc(int param1[1][2], MyClass1 param2[100]) { };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: MyClass1, kind: classKind Class Symbol Table: MyClass1 name: program, kind: function Function Symbol Table: program name: myFunc, kind: function, type: int : int[1][2], MyClass1[100] Function Symbol Table: myFunc name: param1, kind: parameter, type: int[1][2] name: param2, kind: parameter, type: MyClass1[100]", formatSymbolTable(results.SymbolTable));
            Assert.IsFalse(results.SemanticErrors.Any());

            // Test duplicate parameter names
            tokens = lexicalAnalyzer.Tokenize("program { }; int myFunc(int param1, float param1) { };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: program, kind: function Function Symbol Table: program name: myFunc, kind: function, type: int : int, float Function Symbol Table: myFunc name: param1, kind: parameter, type: int name: param1, kind: parameter, type: float", formatSymbolTable(results.SymbolTable));
            Assert.AreEqual(1, results.SemanticErrors.Count);
            Assert.AreEqual("Identifier param1 at line 1 has already been declared", results.SemanticErrors[0]);

            // Test duplicate names with inner variable
            tokens = lexicalAnalyzer.Tokenize("program { }; int myFunc(int param1) { float param1; };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: program, kind: function Function Symbol Table: program name: myFunc, kind: function, type: int : int Function Symbol Table: myFunc name: param1, kind: parameter, type: int name: param1, kind: variable, type: float", formatSymbolTable(results.SymbolTable));
            Assert.AreEqual(1, results.SemanticErrors.Count);
            Assert.AreEqual("Identifier param1 at line 1 has already been declared", results.SemanticErrors[0]);

            // Test duplicate names with member variable
            tokens = lexicalAnalyzer.Tokenize("class MyClass1 { float param1; int myFunc(int param1) {}; }; program { };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: MyClass1, kind: classKind Class Symbol Table: MyClass1 name: param1, kind: variable, type: float name: myFunc, kind: function, type: int : int Function Symbol Table: myFunc name: param1, kind: parameter, type: int name: program, kind: function Function Symbol Table: program", formatSymbolTable(results.SymbolTable));
            Assert.AreEqual(1, results.SemanticErrors.Count);
            Assert.AreEqual("Identifier param1 at line 1 has already been declared", results.SemanticErrors[0]);

            // Test recursive class dependency
            tokens = lexicalAnalyzer.Tokenize("class MyClass1 { int myFunc(MyClass1 param1) {}; }; program { };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: MyClass1, kind: classKind Class Symbol Table: MyClass1 name: myFunc, kind: function, type: int : MyClass1 Function Symbol Table: myFunc name: param1, kind: parameter, type: MyClass1 name: program, kind: function Function Symbol Table: program", formatSymbolTable(results.SymbolTable));
            Assert.AreEqual(1, results.SemanticErrors.Count);
            Assert.AreEqual("MyClass1's member variable or function parameter cannot refer to its own class at line 1", results.SemanticErrors[0]);

            // Test duplicate names in different scopes
            tokens = lexicalAnalyzer.Tokenize("program { }; int myFunc(int param1) { }; int myFunc2(int param1) { };");
            results = syntacticAnalyzer.analyzeSyntax(tokens);

            Assert.AreEqual("Global name: program, kind: function Function Symbol Table: program name: myFunc, kind: function, type: int : int Function Symbol Table: myFunc name: param1, kind: parameter, type: int name: myFunc2, kind: function, type: int : int Function Symbol Table: myFunc2 name: param1, kind: parameter, type: int", formatSymbolTable(results.SymbolTable));
            Assert.IsFalse(results.SemanticErrors.Any());
        }
    }
}