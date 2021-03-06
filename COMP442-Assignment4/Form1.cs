﻿using COMP442_Assignment4.Lexical;
using COMP442_Assignment4.Syntactic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COMP442_Assignment4
{
    /*
        The main form for assignment 2. Handles all input and output.

        For COMP 442 Assignment 2 by Michael Bilinsky 26992358
    */
    public partial class Form1 : Form
    {
        LexicalAnalyzer lexAnalyzer;
        SyntacticAnalyzer synAnalyzer;

        string outputLocation = Application.StartupPath;

        private const int EM_SETTABSTOPS = 0x00CB;

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr h, int msg, int wParam, int[] lParam);

        public Form1()
        {
            lexAnalyzer = new LexicalAnalyzer();
            synAnalyzer = new SyntacticAnalyzer();

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int width = 2;

            // Set tab spacing
            // Stylize tabs as shown here: http://stackoverflow.com/questions/1298406/how-to-set-the-tab-width-in-a-windows-forms-textbox-control
            SendMessage(txtCodeInput.Handle, EM_SETTABSTOPS, 1, new int[] { width * 4 });

            txtCodeInput.Text = Properties.Settings.Default.lastCode;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "Status: analyzing...";

            var code = txtCodeInput.Text;

            Properties.Settings.Default.lastCode = code;
            Properties.Settings.Default.Save();

            var tokens = lexAnalyzer.Tokenize(code);

            // Seperate the correct and error lexical output
            string lexicalTokens = string.Join(System.Environment.NewLine, tokens.Where(x => !x.isError()).Select(x => x.getName()).ToArray());
            string errorTokens = string.Join(System.Environment.NewLine, tokens.Where(x => x.isError()).Select(x => x.getName()).ToArray());

            // Display the tokens and output them to a file
            txtLexTokens.Text = lexicalTokens;
            txtLexErrors.Text = errorTokens;

            outputToFile("outputLexicalTokens.txt", lexicalTokens);
            outputToFile("outputLexicalErrors.txt", errorTokens);

            var result = synAnalyzer.analyzeSyntax(tokens);

            // Generate strings for the syntactic derivation and the errors
            string syntacticDerivation = string.Join(Environment.NewLine, result.Derivation.Select(x => string.Join(" ", x.Select(y => y.getProductName()).Reverse())));
            string syntacticErrors = string.Join(Environment.NewLine, result.Errors);

            // Display the syntactic output and output them to a file
            txtDerivation.Text = syntacticDerivation;
            txtSynErrors.Text = syntacticErrors;

            outputToFile("outputSyntacticDerivation.txt", syntacticDerivation);
            outputToFile("outputSyntacticErrors.txt", syntacticErrors);

            string symbolTableOutput = result.SymbolTable.printTable();
            string semanticErrors = string.Join(Environment.NewLine, result.SemanticErrors);

            txtSymbolTable.Text = symbolTableOutput;
            txtSymbolTableErrors.Text = semanticErrors;

            outputToFile("outputSymbolTable.txt", symbolTableOutput);
            outputToFile("outputSymbolTableErrors.txt", semanticErrors);

            string moonCodeText = result.MoonCode.GenerateCode();

            outputToFile("outputMoonCode.m", moonCodeText);

            txtMoonCode.Text = moonCodeText;

            // Switch to the moon code output tab
            if (result.Derivation.Any())
            {
                tabControl1.SelectTab(4);
            }

            // Update the status label
            label1.Text = result.Errors.Any() ? "Status: Error in Syntax" : "Status: Valid Syntax";

            
        }

        // Outputs a string to a file
        private void outputToFile(string filename, string data)
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(outputLocation + "/" + filename, false))
            {
                file.WriteLine(data);
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        // Open an explorer instance in the output directory
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer.exe", outputLocation);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        using(var reader = new StreamReader(myStream))
                        {
                            txtCodeInput.Text = reader.ReadToEnd();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}
