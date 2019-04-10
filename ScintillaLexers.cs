﻿#region License
/*
MIT License

Copyright (c) 2019 Petteri Kautonen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

// (C)::https://github.com/jacobslusser/ScintillaNET
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ScintillaNET;
using VPKSoft.ScintillaLexers.CreateSpecificLexer;
using VPKSoft.ScintillaLexers.ScintillaNotepadPlus;
using static VPKSoft.ScintillaLexers.LexerEnumerations;


// (C)::https://github.com/notepad-plus-plus/notepad-plus-plus
// (C)::https://notepad-plus-plus.org
// (C)::https://github.com/jacobslusser/ScintillaNET
// (C)::https://www.scintilla.org

namespace VPKSoft.ScintillaLexers
{



    /// <summary>
    /// A class for setting a lexer for a Scintilla class instance.
    /// </summary>
    public static class ScintillaLexers
    {
        /// <summary>
        /// Gets or sets the value of a LexerColors class instance.
        /// </summary>
        public static LexerColors LexerColors { get; private set; } = new LexerColors();



        /// <summary>
        /// Creates the lexer from XML file used by the Notepad++ software.
        /// </summary>
        /// <param name="scintilla">The <see cref="Scintilla"/> which lexer style to set.</param>
        /// <param name="lexerType">Type of the lexer.</param>
        /// <param name="fileName">The file name.</param>
        /// <returns><c>true</c> if the operation was successful, <c>false</c> otherwise.</returns>
        public static bool CreateLexerFromFile(Scintilla scintilla, LexerType lexerType, string fileName, bool useGlobalOverride, bool font)
        {
            try
            {
                XDocument document = XDocument.Load(fileName);

                if (lexerType == LexerType.Cs)
                {
                    ScintillaNotepadPlusStyles.SetGlobalDefaultStyles(document, scintilla, useGlobalOverride, font);

                    ScintillaNotepadPlusStyles.LoadLexerStyleFromNotepadPlusXml(document, scintilla, lexerType); // TODO::Font?

                    scintilla.Lexer = Lexer.Cpp;

                    ScintillaKeyWords.SetKeywords(scintilla, lexerType);

                    ScintillaNotepadPlusStyles.SetFolding(document, scintilla);

                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Creates the lexer for a given Scintilla class instance with a given language type enumeration.
        /// </summary>
        /// <param name="scintilla">A Scintilla class instance to set the lexer style for.</param>
        /// <param name="lexerType">Type of the lexer / programming language.</param>
        /// <returns>True if the given lexer was found; otherwise false (a work in progress).</returns>
        public static bool CreateLexer(Scintilla scintilla, LexerType lexerType)
        {
            if (lexerType == LexerType.Cs)
            {
                return CreateLexerCs.CreateCsLexer(scintilla, LexerColors);
            }

            if (lexerType == LexerType.Xml)
            {
                return CreateLexerXml.CreateXmlLexer(scintilla, LexerColors);
            }

            if (lexerType == LexerType.Cpp)
            {
                return CreateLexerCpp.CreateCppLexer(scintilla, LexerColors);
            }

            if (lexerType == LexerType.Text || lexerType == LexerType.Unknown)
            {
                return CreateLexerNull.CreateNullLexer(scintilla);
            }

            if (lexerType == LexerType.Nsis)
            {
                return CreateLexerNsis.CreateNsisLexer(scintilla, LexerColors);
            }

            if (lexerType == LexerType.SQL)
            {
                return CreateLexerSql.CreateSqlLexer(scintilla, LexerColors);
            }

            if (lexerType == LexerType.Batch)
            {
                return CreateLexerBatch.CreateBatchLexer(scintilla, LexerColors);
            }

            if (lexerType == LexerType.Pascal)
            {
                return CreateLexerPascal.CreatePascalLexer(scintilla, LexerColors);
            }

            if (lexerType == LexerType.PHP)
            {
                return CreateLexerPhp.CreatePhpLexer(scintilla, LexerColors);
            }

            if (lexerType == LexerType.HTML)
            {
                return CreateLexerHtml.CreateHtmlLexer(scintilla, LexerColors);
            }

            if (lexerType == LexerType.WindowsPowerShell)
            {
                return CreateLexerPowerShell.CreatePowerShellLexer(scintilla, LexerColors);
            }

            if (lexerType == LexerType.INI)
            {
                return CreateLexerIni.CreateIniLexer(scintilla, LexerColors);
            }

            if (lexerType == LexerType.Python)
            {
                return CreateLexerPython.CreatePythonLexer(scintilla, LexerColors);
            }

            // a lexer wasn't found..
            return false;
        }
    }
}
