# BASICLanguageParser.Grammar

This project builds a c# lexer & parser based on ANTLR4 in the project BASICLanguageParser.Parser.  Building this project rebuilds the core of the other project.

The target language is Extended Color Basic 2 for the Color Computer 3.  This is the third version of BASIC for the line of Color Computers, each version building on the previous one.  The original is Color Basic circa 1980.  Then Extended Color Basic around 1981 for the Color Computer 1 and 2 added many more machine specific commands custom to the Color Computer line.  Extended Color Basic 2 or Super Extended Color Basic is specific to the Color Computer 3 and not backward compatible.

The 2 prior versions of Color Basic are compatible with Extended Color Basic 2.  So, the plan is to develop a grammar compatible with all 3 major flavors.  Color Basic forms the basis.  Extended Color Basic adds many commands for video and sound.  Super Extended Color Basic adds commands for the Color Computer 3.

At the moment (2/6/2022), Color Basic grammar is under development.

To use this project you compile it.  Antlr4 will build the c# files in ..\\BASICLanguageParser.  Then those are copied to the correct folder for the appropriate language in that project.  Thought Visual Studio compiles the project, VSCode is used to edit the grammar files due to the excellent Extension for Antlr4 in that editor.

The grammars here could serve as a basis to the many other 1980's Microsoft Tiny Basic.

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
