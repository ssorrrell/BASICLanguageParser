# BASIC Language Parser Library

Lexer/Parser for 8 bit BASIC languages to be used as the core of a Language Server for a Visual Studio Extension.  See BASICLanguageParser.Grammar sub solution for how to build the lexer/parser that forms the basis of this project namespace.

Unit tests, in BASICLanguageParser.UT using NUnit, are provided to validate the individual language features.  A number of code examples from Color Computer manuals and Rainbow magazine articles form the basis for Unit Tests.

At the moment (2/6/2022), work on the Color Basic Parser is under way.  Then the parser for the following versions of Basic will begin.

This a c# parser for the old 8-bit Basic used by the TRS-80 Color Computer line of computers and could be used as the beginning of other projects that take that Basic and generate output; like a cruncher, virtual machine, compiler, etc.  Antlr4 generates a very good skeleton framework of code with little insded of it.  Different actions can be coded for each of the tokens and statements.  The purpose here is simply syntax checking semantic information for a Language Server.

BASICLanguageParser.Grammar.Antlr.Build.sln is used to build the lexer/parser from grammar files

BASICLanguageParser.sln is the compiled unit testable parser.  There are post build instructions to copy the resulting files to the Language Server project.

## Languages

- Color Basic
- Extended Color Basic
- Disk Extended Color Basic
- Super Extended Color Basic

## Links

<https://blog.lextudio.com/how-to-write-your-language-server-in-c-d9302a44f694>

<https://github.com/CXuesong/LanguageServer.NET>

<https://github.com/donaldpipowitch/how-to-create-a-language-server-and-vscode-extension>