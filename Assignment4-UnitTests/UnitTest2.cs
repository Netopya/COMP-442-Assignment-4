﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using COMP442_Assignment3.Lexical;
using COMP442_Assignment3.Syntactic;
using System.Linq;

namespace Assignment2_UnitTests
{
    /// <summary>
    /// Summary description for UnitTest2
    /// </summary>
    [TestClass]
    public class UnitTest2
    {
        LexicalAnalyzer lexicalAnalyzer = new LexicalAnalyzer();
        SyntacticAnalyzer syntacticAnalyzer = new SyntacticAnalyzer();

        [TestMethod]
        public void TestDemoCode()
        {
            var tokens = lexicalAnalyzer.Tokenize("class Utility { int var1[4][5][7][8][9][1][0]; float var2; int findMax(int array[100]) { int maxValue; int idx; maxValue = array[100]; for( int idx = 99; idx > 0; idx = idx - 1 ) { if(array[idx] > maxValue) then { maxValue = array[idx]; }else{}; }; return (maxValue); }; int findMin(int array[100]) { int minValue; int idx; minValue = array[100]; for( int idx = 1; idx <= 99; idx = ( idx ) + 1) { if(array[idx] < maxValue) then { maxValue = array[idx]; }else{}; }; return (minValue); }; }; program { int sample[100]; int idx; int maxValue; int minValue; Utility utility; Utility arrayUtility[2][3][6][7]; for(int t = 0; t<=100 ; t = t + 1) { get(sample[t]); sample[t] = (sample[t] * randomize()); }; maxValue = utility.findMax(sample); minValue = utility.findMin(sample); utility. var1[4][1][0][0][0][0][0] = 10; arrayUtility[1][1][1][1].var1[4][1][0][0][0][0][0] = 2; put(maxValue); put(minValue); }; float randomize() { float value; value = 100 * (2 + 3.0 / 7.0006); value = 1.05 + ((2.04 * 2.47) - 3.0) + 7.0006 ; return (value); }; ");
            var result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());
        }

        // Test code at the level of the prog token
        [TestMethod]
        public void TestProgLevelCode()
        {
            // 1 class, 1 program
            var tokens = lexicalAnalyzer.Tokenize("class foo { }; program {  };");
            var result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // 1 program
            tokens = lexicalAnalyzer.Tokenize("program {  };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // 2 classes, 1 program, 2 functions
            tokens = lexicalAnalyzer.Tokenize("class foo { }; class foo { }; program {  }; float foo() {}; float random() {};");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // 2 classes, no program, 2 functions
            tokens = lexicalAnalyzer.Tokenize("class foo { }; class foo { }; float foo() {}; float random() {};");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());
        }

        [TestMethod]
        public void TestFunctionBody()
        {
            // illegal func decleration in program
            var tokens = lexicalAnalyzer.Tokenize("program { int findMin(int array[100]){}; };");
            var result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // func declaration in class
            tokens = lexicalAnalyzer.Tokenize("class foo { int findMin(int array[100]){}; }; program {  };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // func declaration in functionlist
            tokens = lexicalAnalyzer.Tokenize("program {  };  int findMin(int array[100]){};");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // different types of function types
            tokens = lexicalAnalyzer.Tokenize("class foo { int findMin(int array[100]){}; float findMin(int array[100]){}; sometype findMin(int array[100]){};}; program {  };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // missing type
            tokens = lexicalAnalyzer.Tokenize("class foo { findMin(int array[100]){}; }; program {  };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());
        }

        // Test various types of statements
        [TestMethod]
        public void TestStatements()
        {
            // Basic if statement
            var tokens = lexicalAnalyzer.Tokenize("program { if (foo==3) then else; };");
            var result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // If with blocks 
            tokens = lexicalAnalyzer.Tokenize("program { if (foo==3) then { foo = foo + 1; } else { foo = foo == 5; }; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // If with single line blocks 
            tokens = lexicalAnalyzer.Tokenize("program { if (foo==3) then foo = foo + 1; else foo = foo == 5; ; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // If wrong placement of statement block 
            tokens = lexicalAnalyzer.Tokenize("program { if (foo==3) { foo = foo + 1; } then else { foo = foo == 5; }; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // If with assignment instead of expr
            tokens = lexicalAnalyzer.Tokenize("program { if (foo=3) then { foo = foo + 1; } else { foo = foo == 5; }; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // Basic for statement
            tokens = lexicalAnalyzer.Tokenize("program { for (int foo = 0; 45 > foo; foo = foo + 1) ; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Basic for statement with identifier type
            tokens = lexicalAnalyzer.Tokenize("program { for (idname foo = 0; 45 > foo; foo = foo + 1) ; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Basic for statement with block
            tokens = lexicalAnalyzer.Tokenize("program { for (int foo = 0; 45 > foo; foo = foo + 1) { foo = 3; } ; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Illegal for statement no semicolons
            tokens = lexicalAnalyzer.Tokenize("program { for (int foo = 0 45 > foo foo = foo + 1) { foo = 3; } ; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // Basic get statement
            tokens = lexicalAnalyzer.Tokenize("program { get(foo); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Get statement with idnests
            tokens = lexicalAnalyzer.Tokenize("program { get(foo[5][6].bar[1][100]); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Incorrect get statement with relation expression
            tokens = lexicalAnalyzer.Tokenize("program { get(foo[5][6].bar[1][100] > monkey); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // Basic put statement
            tokens = lexicalAnalyzer.Tokenize("program { put(100); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Put statement with idnests
            tokens = lexicalAnalyzer.Tokenize("program { put(foo[5][6].bar[1][100] > monkey); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Put statement with mathematic expression
            tokens = lexicalAnalyzer.Tokenize("program { put(1 + 1); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Incorrect put statement with variable
            tokens = lexicalAnalyzer.Tokenize("program { put(foo someid); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // Basic return statement
            tokens = lexicalAnalyzer.Tokenize("program { return(50); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Incorrect return statement with variable
            tokens = lexicalAnalyzer.Tokenize("program { return(foo someid); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // Test assignment statement
            tokens = lexicalAnalyzer.Tokenize("program { foo = 300; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test assignment statement with expression
            tokens = lexicalAnalyzer.Tokenize("program { foo = 300 + 3; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test illegal assignment statement with relop
            tokens = lexicalAnalyzer.Tokenize("program { foo == 300; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // Test illegal assignment statement with type
            tokens = lexicalAnalyzer.Tokenize("program { int foo = 300; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // Test illegal statement with missing semi-colon
            tokens = lexicalAnalyzer.Tokenize("program { foo = 300 };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // Test illegal assignment of a statement
            tokens = lexicalAnalyzer.Tokenize("program { foo = { bar = 300 } };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());
        }

        // Test statements in different locations
        [TestMethod]
        public void TestStatementLocation()
        {
            // Basic if in funcdef
            var tokens = lexicalAnalyzer.Tokenize("program { }; int function() {if (foo==3) then else;};");
            var result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // If statement in class decleration
            tokens = lexicalAnalyzer.Tokenize("class className { int function() {if (foo==3) then else;}; }; program { };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // If statement in func decleration
            tokens = lexicalAnalyzer.Tokenize("class className { }; program { }; functype funcname() { if (foo==3) then else; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Illegal if statement outside of any body
            tokens = lexicalAnalyzer.Tokenize("class className { }; program { }; if (foo==3) then else; functype funcname() { };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());
        }

        // Test variables and id nests
        [TestMethod]
        public void TestVariables()
        {
            // Basic variable declaration
            var tokens = lexicalAnalyzer.Tokenize("program { int variablename; };");
            var result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Basic variable declaration with id type
            tokens = lexicalAnalyzer.Tokenize("program { idname variablename; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Illegal variable declaration without semi colon
            tokens = lexicalAnalyzer.Tokenize("program { idname variablename };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // Basic variable declaration with array size
            tokens = lexicalAnalyzer.Tokenize("program { idname variablename[3][4]; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Illegal variable declaration without int array size
            tokens = lexicalAnalyzer.Tokenize("program { idname variablename[3 + 1][4 - foo]; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // Basic variable assignment
            tokens = lexicalAnalyzer.Tokenize("program { variablename = 20; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Variable assignment with basic indices
            tokens = lexicalAnalyzer.Tokenize("program { variablename[3][2] = 20; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Variable assignment with arithmetic for indices
            tokens = lexicalAnalyzer.Tokenize("program { variablename[3 + 1][foo * 2] = 20; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Illegal variable assignment with non-arithmetic assignments for indices
            tokens = lexicalAnalyzer.Tokenize("program { variablename[4 > 1][foo <> 2] = 20; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // Basic variable assignment with id nest
            tokens = lexicalAnalyzer.Tokenize("program { variablename.someothername = 20; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Variable assignment id nest and indices
            tokens = lexicalAnalyzer.Tokenize("program { variablename[3][4].someothername[1] = 20; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Illegal variable assignment with empty indice
            tokens = lexicalAnalyzer.Tokenize("program { variablename[] = 20; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());
        }

        [TestMethod]
        public void TestExpressions()
        {
            // Basic arithmetic expression that resolves to <term> then <factor>
            var tokens = lexicalAnalyzer.Tokenize("program { variablename = 20; };");
            var result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test addition and subtraction
            tokens = lexicalAnalyzer.Tokenize("program { variablename = 20 + 30 - 10; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test multiplication and division
            tokens = lexicalAnalyzer.Tokenize("program { variablename = 20 / 30 * 10; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test addition, subtraction, multiplication and division
            tokens = lexicalAnalyzer.Tokenize("program { variablename = 20 / 40 + 1 - 30 * 10; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // test illegal multiple multops
            tokens = lexicalAnalyzer.Tokenize("program { variablename = 20 / * 40; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // test illegal multops after addop
            tokens = lexicalAnalyzer.Tokenize("program { variablename = 20 + / 40; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // Test relation expressions
            tokens = lexicalAnalyzer.Tokenize("program { variablename = 20 == 30; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test illegal multiple relation expressions
            tokens = lexicalAnalyzer.Tokenize("program { variablename = 20 == 30 <> 10; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());
        }

        [TestMethod]
        public void TestFactors()
        {
            // Test basic factor num to int
            var tokens = lexicalAnalyzer.Tokenize("program { variablename = 20; };");
            var result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test basic factor num to float
            tokens = lexicalAnalyzer.Tokenize("program { variablename = 20.1; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test basic function call
            tokens = lexicalAnalyzer.Tokenize("program { variablename = funcName(); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test basic function call with id nest
            tokens = lexicalAnalyzer.Tokenize("program { variablename = somevariable.funcName(); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test function call with id nest and indices
            tokens = lexicalAnalyzer.Tokenize("program { variablename = somevariable[3][1].funcName(); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test illegal function call with id nest and indices
            tokens = lexicalAnalyzer.Tokenize("program { variablename = somevariable.funcName[1][2](); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // Test function call with parameters
            tokens = lexicalAnalyzer.Tokenize("program { variablename = somevariable.funcName(1, 200); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test arithmetic expression with brackets
            tokens = lexicalAnalyzer.Tokenize("program { variablename = (2); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test arithmetic expression with brackets and operations
            tokens = lexicalAnalyzer.Tokenize("program { variablename =  1 - (2 + 2) / 2; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test arithmetic expression with brackets, relexpr, and operations
            tokens = lexicalAnalyzer.Tokenize("program { variablename =  1 <> (2 + 2) / 2; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test illegal parenthesization with relationship expression
            tokens = lexicalAnalyzer.Tokenize("program { variablename =  1 - (2 < 2); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // Test not factor
            tokens = lexicalAnalyzer.Tokenize("program { variablename =  not 2; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test factor not another factor
            tokens = lexicalAnalyzer.Tokenize("program { variablename =  not somevariable; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test factor not in a relexpr
            tokens = lexicalAnalyzer.Tokenize("program { variablename =  not 1 > 2; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test basic sign
            tokens = lexicalAnalyzer.Tokenize("program { variablename = -2; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test mutiple signs
            tokens = lexicalAnalyzer.Tokenize("program { variablename = - - + - +2; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test sign to factor
            tokens = lexicalAnalyzer.Tokenize("program { variablename = - (2 + 1); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Illegal Test sign to relexp
            tokens = lexicalAnalyzer.Tokenize("program { variablename = - (2 > 1); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());
        }

        // Test various parameters for function declarations
        [TestMethod]
        public void TestFParams()
        {
            // Test basic function in func body
            var tokens = lexicalAnalyzer.Tokenize("program { }; idname funname() {};");
            var result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test function in class
            tokens = lexicalAnalyzer.Tokenize("class classname { idname funname() {}; }; program { };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test function with int and float
            tokens = lexicalAnalyzer.Tokenize("program { }; int funname() {}; float funname() {};");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test illegal function in program
            tokens = lexicalAnalyzer.Tokenize("program {  idname funname() {}; }; ");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // Test function with parameters
            tokens = lexicalAnalyzer.Tokenize("program { }; idname funname(idname param1) {};");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test function with arraysize
            tokens = lexicalAnalyzer.Tokenize("program { }; idname funname(idname param1[1]) {};");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test function with multiple parameters
            tokens = lexicalAnalyzer.Tokenize("program { }; idname funname(idname param1, float param1, int param3, idname2 param4) {};");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test illegal idnest
            tokens = lexicalAnalyzer.Tokenize("program { }; idname funname(idname param1.foo) {};");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // Test illegal missing comma
            tokens = lexicalAnalyzer.Tokenize("program { }; idname funname(idname param1 int param2) {};");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // Test illegal expression
            tokens = lexicalAnalyzer.Tokenize("program { }; idname funname(3 + 2) {};");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());
        }

        // Test various permutations of function calls
        [TestMethod]
        public void TestAParams()
        {
            // Test basic function call in program
            var tokens = lexicalAnalyzer.Tokenize("program { idname = funcname(); };");
            var result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test basic function call in class
            tokens = lexicalAnalyzer.Tokenize("class classname { int func() { idname = funcname(); }; }; program {  };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test illegal hanging function call
            tokens = lexicalAnalyzer.Tokenize("program { funcname(); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // Test illegal function call outside of program
            tokens = lexicalAnalyzer.Tokenize("program { }; funcname();");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // Test illegal function call  in a class body
            tokens = lexicalAnalyzer.Tokenize("class classname { funcname(); }; program { }; ");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // Test function call with parameter
            tokens = lexicalAnalyzer.Tokenize("program { idname = funcname(param1); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test function call with multiple parameters
            tokens = lexicalAnalyzer.Tokenize("program { idname = funcname(param1, param2, param3); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test function call with multiple different expressions
            tokens = lexicalAnalyzer.Tokenize("program { idname = funcname(param1, 1 + 3, 1 / (2 + 1), somefunc(), var[1][2].var[1]); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test illegal function call with type
            tokens = lexicalAnalyzer.Tokenize("program { idname = funcname(int number); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());

            // Test illegal function call with missing comma
            tokens = lexicalAnalyzer.Tokenize("program { idname = funcname(param1 param2); };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());
        }

        // Test relationship operations
        [TestMethod]
        public void TestRelationOperators()
        {
            
            List<IToken> tokens;
            SyntaxResult result;

            // Test relation operations
            List<string> reops = new List<string> { "==", "<>", "<", ">", "<=", ">="};

            foreach(string op in reops)
            {
                tokens = lexicalAnalyzer.Tokenize("program { idname = 1 " + op + " 2; };");
                result = syntacticAnalyzer.analyzeSyntax(tokens);
                Assert.IsFalse(result.Errors.Any());
            }

            // Test illegal relation operations
            reops = new List<string> { "=<", "=>", "<<", ">>", "===" };

            foreach (string op in reops)
            {
                tokens = lexicalAnalyzer.Tokenize("program { idname = 1 " + op + " 2; };");
                result = syntacticAnalyzer.analyzeSyntax(tokens);
                Assert.IsTrue(result.Errors.Any());
            }
        }

        [TestMethod]
        public void TestNum()
        {
            // Test basic int
            var tokens = lexicalAnalyzer.Tokenize("program { idname = 12; };");
            var result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test basic float
            tokens = lexicalAnalyzer.Tokenize("program { idname = 12.23; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test illegal float
            tokens = lexicalAnalyzer.Tokenize("program { idname = 12.230; };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsTrue(result.Errors.Any());
        }

        [TestMethod]
        public void TestComments()
        {
            // Test block comment
            var tokens = lexicalAnalyzer.Tokenize("program { /* A block comment */ };");
            var result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());

            // Test line comment
            tokens = lexicalAnalyzer.Tokenize("program { // A line comment" + Environment.NewLine + " };");
            result = syntacticAnalyzer.analyzeSyntax(tokens);
            Assert.IsFalse(result.Errors.Any());
        }
    }
}