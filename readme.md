# BASIC Language Parser Library

Lexer/Parser for 8 bit BASIC languages to be used as the core of a Language Server for a Visual Studio Extension.  See BASICLanguageParser.Grammar sub project for how to build the lexer/parser that forms the basis of this project namespace.

Unit tests, in BASICLanguageParser.UT using NUnit, are provided to validate the individual language features.

BASICLanguageParser.Grammar.Antlr.Build.sln is used to build the lexer/parser from grammar files

BASICLanguageParser.Grammar.sln is the compiled unit testable parser

Links

<https://blog.lextudio.com/how-to-write-your-language-server-in-c-d9302a44f694>

<https://github.com/CXuesong/LanguageServer.NET>