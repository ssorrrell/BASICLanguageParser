# ECB2_Lib Parser Grammar

This project builds a c# lexer & parser based on ANTLR4 in the project ECB2_Lib.Parser.  Building this project rebuilds the core of the other project.

Target language is Extended Color Basic 2 for the Color Computer 3.  Intend to turn this into the basis for a Language Server.  Initially, using the jvmBasic.g4 grammar, BASIC grammar built from Apple and GW Basic.  It should be relatively easy to extend further languages, by editing the g4 file and building again.  This could provide the foundations of a language server for all 8-bit archaic BASIC languages, by adding or removing machine specific aspects.

Lexer/Parser from antlr4 following instructions in the below link.

Visual Studio 2016 Project due to the Antlr4 dependencies.

Adding new g4 File Instructions

- Add new g4 file
- Update csproj file entry.  Add additional item group.

```xml
<Antlr4 Include="jvmBasic.g4">
<Package>ECB2_Lib.Parser</Package>
<Visitor>true</Visitor>
<Error>false</Error>
<Listener>true</Listener>    
<AntOutDir>..\ECB2_Lib.Parser</AntOutDir>
</Antlr4>
```

Post Build Instructions

- Remove CSL lines to remove the compile warnings
- New/Replaced files are called:
  - \<g4 language file name\>.interp
  - \<g4 language file name\>.tokens
  - \<g4 language file name\>BaseListener.cs
  - \<g4 language file name\>BaseVisitor.cs
  - \<g4 language file name\>Lexer.cs
  - \<g4 language file name\>Lexer.interp
  - \<g4 language file name\>Lexer.tokens
  - \<g4 language file name\>Listener.cs
  - \<g4 language file name\>Parser.cs
  - \<g4 language file name\>Visitor.cs

Links

- Instructions <https://stackoverflow.com/questions/19327831/antlr4-c-sharp-application-tutorial-example>
- Grammars <https://github.com/antlr/grammars-v4/tree/master/basic>
- jvmBASIC <http://blog.khubla.com/java/jvmbasic-2-0>
- List of BASIC Dialects <https://en.wikipedia.org/wiki/List_of_BASIC_dialects>
