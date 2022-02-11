
grammar ColorBasic;
options
{
    language=CSharp;
}

/*
[The "BSD licence"]
Copyright (c) 2022 Stephen Sorrell
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions
are met:
1. Redistributions of source code must retain the above copyright
notice, this list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright
notice, this list of conditions and the following disclaimer in the
documentation and/or other materials provided with the distribution.
3. The name of the author may not be used to endorse or promote products
derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE AUTHOR ``AS IS'' AND ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT,
INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/// a program is a collection of lines

/*
Description: Grammar for Color Basic 1.2 for the TRS-80 Color Computer

Color Basic is a tiny basic created by Microsoft.  Version 1.2 is copyright 1983.
It is very similar to the two prior versions.  This comes primarily from the
Color Basic Unravelled series and the Color Computer Basic Manual and Quick Reference.
*/

/******************************Parser***************************************/
prog
   : line+ EOF
   ;

// a line starts with an integer
line
   : LINENUMBER (statement | COMMENT) (':' (statement | COMMENT))*
   ;


/****************************master statement table*************************************/
statement
    : returnstmt
    | restorestmt
    | printstmt
    | nextstmt
    | pokestmt
    | ifstmt1
    | ifstmt2
    | forstmt
    | inputstmt1
    | dimstmt
    | gotostmt
    | gosubstmt
    | readstmt
    | datastmt
    | printstmt
    | printtabstmt
    | printhashstmt
    | printatstmt
    | newstmt
    | stopstmt
    | endstmt
    | runstmt
    | clearstmt
    | contstmt
    | liststmt
    | lliststmt
    | setstmt
    | resetstmt
    | clsstmt
    | execstmt
    | motorstmt
    | audiostmt
    | soundstmt
    | cloadstmt
    | cloadmstmt
    | csavestmt
    | csavemstmt
    | skipfstmt
    | openstmt
    | closestmt
    | letstmt
   ;

/****************************master function table*************************************/
// func_
//    : STRINGLITERAL
//    | signed_number
//    | vardecl
//    | chrfunc
//    | lenfunc
//    | strfunc
//    | midfunc
//    | peekfunc
//    | intfunc
//    | leftfunc
//    | valfunc
//    | rightfunc
//    | sinfunc
//    | rndfunc
//    | sgnfunc
//    | absfunc
//    | inkeyfunc
//    | joystkfunc
//    | eoffunc
//    | pointfunc
//    | memfunc
//    | usrfunc
//    | ('(' expr ')')
//    ;

// exprs and such
// signed_number
//    :  ('+' | '-')? (NUMBER)
//    ;

// signExpression
//    : NOT+ ('+' | '-')? func_
//    ;

// exponentExpression
//    : signExpression ( <assoc=right> '^' signExpression)*
//    ;

// multiplyingExpression
//    : exponentExpression (('*' | '/') exponentExpression)*
//    ;

// addingExpression
//    : multiplyingExpression (('+' | '-') multiplyingExpression)*
//    ;

// relationalExpression
//    : addingExpression ((relop) addingExpression)?
//    ;

// expr
//    : func_
//    | (relationalExpression ((AND | OR) relationalExpression)*)
//    ;

char_expr
   : STRINGLITERAL
   ;

expr
   : '-' expr                          # UMINUS
   | expr addop expr                   # ADDOPGRP
   | expr mulop expr                   # MULOPGRP
   | expr ( <assoc=right> '^' expr )   # EXPONENT
   | '(' expr ')'                      # PARENGRP
   | NUMBER                            # DOUBLE
   ;

addop : '+' | '-' ;

mulop : '*' | '/'  ;

relop
   : gte
   | lte
   | neq
   | '='
   | '>'
   | '<'
   ;

neq
   : '<' '>'
   | '>' '<'
   ;

gte
    : '>' '='
    ;

lte
    : '<' '='
    ;

var_number
   : LETTER (LETTER | DIGIT)*
   ;

var_string
   : LETTER (LETTER | DIGIT)* '$'
   ;

vardecl
   : var_number ('(' exprlist ')')*
   ;

varlist
   : vardecl (',' vardecl)*
   ;

variableassignment
   : vardecl '=' expr
   ;

exprlist
   : expr (',' expr)*
   ;

datum
   : NUMBER
   | DATUM
   ;

/*******************functions**********************/
absfunc
   : ABS '(' expr ')'
   ;

ascfunc
   : ASC '(' expr ')'
   ;

sgnfunc
   : SGN '(' expr ')'
   ;

intfunc
   : INT '(' expr ')'
   ;

sinfunc
   : SIN '(' expr ')'
   ;

rndfunc
   : RND '(' expr ')'
   ;

lenfunc
   : LEN '(' expr ')'
   ;

valfunc
   : VAL '(' expr ')'
   ;

chrfunc
   : CHR '(' expr ')'
   ;

midfunc
   : MID '(' expr ',' expr ',' expr ')'
   ;

leftfunc
   : LEFT '(' expr ',' expr ')'
   ;

rightfunc
   : RIGHT '(' expr ',' expr ')'
   ; 

strfunc
   : STR '(' expr ')'
   ;

inkeyfunc
   : INKEY
   ;

joystkfunc
   : JOYSTK '(' expr ')'
   ;

eoffunc
   : EOFTOKEN '(' expr ')'
   ;

peekfunc
   : PEEK '(' expr ')'
   ;

pointfunc
   : POINT '(' expr ',' expr ')'
   ;

memfunc
   : MEM
   ; 

usrfunc
   : USR DIGIT '(' expr ')'
   ;

/*******************statements**********************/

letstmt
   : LET? variableassignment
   ;

dimstmt
   : DIM varlist
   ;

// for stmt puts the for, the statment, and the next on 3 lines.  It needs "nextstmt"
forstmt
   : FOR vardecl '=' expr TO expr (STEP expr)?
   ;

nextstmt
   : NEXT (varlist)?
   ;

ifstmt1
   : IF expr THEN? ((statement)+ | LINENUMBER)
   ;

ifstmt2
   : IF expr THEN ((statement)+ | LINENUMBER) ELSE ((statement)+ | LINENUMBER)
   ;

gotostmt
   : GO TO LINENUMBER
   ;

gosubstmt
   : GO SUB LINENUMBER
   ;

ongotostmt
   : ON expr GO TO LINENUMBER (',' LINENUMBER)*
   ;

ongosubstmt
   : ON expr GO SUB LINENUMBER (',' LINENUMBER)*
   ;

returnstmt
   : RETURN
   ;

datastmt
   : DATA datum+ (',' datum+)*
   ;

readstmt
   : READ varlist
   ;

restorestmt
   : RESTORE
   ;

newstmt
   : NEW
   ;

endstmt
   : END
   ;

stopstmt
   : STOP
   ;

runstmt
   : RUN
   ;

clearstmt
   : CLEAR
   ;

contstmt
   : CONT
   ;

liststmt
   : LIST
   ;

lliststmt
   : LLIST
   ;

// inputstmt1
//     : INPUT (func_ (INPUT_COMMA func_)*)
//     ;

inputstmt1
   : INPUT ((STRINGLITERAL ';') | ('#' DEVICE_CASSETTE ','))? varlist
   ;

printstmt
   : PRINT char_expr?
   ;

printtabstmt
   : PRINT TAB '(' expr ')' ';' expr
   ;

printhashstmt
   : PRINT '#' (DEVICE_CASSETTE | DEVICE_PRINTER) ',' expr 
   ;

printatstmt
   : PRINT '@' expr ',' expr 
   ;

setstmt
   : SET '(' expr ',' expr (',' expr)+ ')'
   ;
   
resetstmt
   : RESET '(' expr ',' expr ')'
   ;

clsstmt
   : CLS expr
   ;

execstmt
   : EXEC expr
   ;

pokestmt
   : POKE expr ',' expr
   ;

motorstmt
   : MOTOR (ON | OFF)
   ;

audiostmt
   : AUDIO (ON | OFF)
   ;

soundstmt
   : SOUND expr ',' expr
   ;   

cloadstmt
   : CLOAD expr
   ; 

cloadmstmt
   : CLOAD expr ',' expr
   ; 

csavestmt
   : CSAVE expr ',' expr
   ;

csavemstmt
   : CSAVEM expr ',' expr ',' expr ',' expr
   ; 

skipfstmt
   : SKIPF expr
   ; 

openstmt
   : OPEN ('I' | 'O' ) ',' '#' (DEVICE_KEYBOARD | DEVICE_CASSETTE | DEVICE_PRINTER) ',' expr
   ; 

closestmt
   : CLOSE '#' (DEVICE_CASSETTE)?
   ; 

/******************************Lexer***************************************/   
LET //assign variables
   : 'LET'
   ;

DIM //dim variables
   : 'DIM'
   ;

/*******************functions**********************/
ABS //absolute value
   : 'ABS'
   ;

ASC //get code of first character in string
   : 'ASC'
   ;

SGN //Convert signed number into floating point number.
   : 'SGN'
   ;

INT //Convert float to an integer
   : 'INT'
   ;

SIN //sine wave function
   : 'SIN'
   ;

RND //random number function
   : 'RND'
   ;

LEN //string length function
   : 'LEN'
   ;

VAL //convert a string to a number
   : 'VAL'
   ;

CHR //Convert string to integer
   : 'CHR$'
   ;

MID //return mid portion of string
   : 'MID$'
   ;

LEFT //return left portion of string
   : 'LEFT$'
   ;

RIGHT //return right portion of string
   : 'RIGHT$'
   ;

STR //convert n to string
   : 'STR$'
   ;

INKEY //get key from keyboard
   : 'INKEY$'
   ;

JOYSTK //get the joystick axis
   : 'JOYSTK'
   ;

EOFTOKEN //return false if there is more data on the device
   : 'EOF'
   ;

PEEK //get the contents at the memory address
   : 'PEEK'
   ;

POINT //returns info about the specified point on the screen
   : 'POINT'
   ;

MEM //returns amount of free memory
   : 'MEM'
   ;

USR //call machine language subroutine 0-9
   : 'USR'
   ;

/*******************statements**********************/

FOR //for loop
   : 'FOR'
   ;

TO //for to loop
   : 'TO'
   ;

STEP //for to step loop
   : 'STEP'
   ;

NEXT //next closing statement to loop
   : 'NEXT'
   ;

IF //if 
   : 'IF'
   ;

THEN //if then
   : 'THEN'
   ;

ELSE //if then else
   : 'ELSE'
   ;

GO //goto
   : 'GO'
   ;

SUB //gosub
   : 'SUB'
   ;

ON //on gosub
   : 'ON'
   ;

OFF //off token
    : 'OFF'
    ; 

RETURN //return from a gosub
   : 'RETURN'
   ;

DATA //define data elements
   : 'DATA'
   ;

RESTORE //restore the data pointer back
   : 'RESTORE'
   ;

READ //read from the data statement
   : 'READ'
   ;

NEW //erase basic program, clear variables space,.. 
   : 'NEW'
   ;

END //end program
   : 'END'
   ;

STOP //stop program execution
   : 'STOP'
   ;

RUN //run program
   : 'RUN'
   ;

CONT //continue program execution
   : 'CONT'
   ;

LIST //list program to the screen
   : 'LIST'
   ;

LLIST //list program to printer
   : 'LLIST'
   ;

CLEAR //erase all variables, initialize pointers,..
   : 'CLEAR'
   ;

INPUT //input
    : 'INPUT'
    ;

PRINT //print@ or print# or print or print tab
    : 'PRINT'
    ;

TAB //print tab(22);"hello" move the cursor to the tab position
    : 'TAB'
    ;

SET //set(x,y,c) c is optional set a point on the screen
    : 'SET'
    ;

RESET //reset(x,y) reset a point on the screen
    : 'RESET'
    ;    

CLS //cls n set background color of screen
    : 'CLS'
    ;   

EXEC //transfer control to a machine language address
    : 'EXEC'
    ; 

POKE //put a number in an address
    : 'POKE'
    ; 

MOTOR //turn the cassette on or off
    : 'MOTOR'
    ;   

AUDIO //connect cassette output to the screen
    : 'AUDIO'
    ; 

SOUND //play specified tone and duration
    : 'SOUND'
    ; 

CLOAD //load program from cassette
    : 'CLOAD'
    ;   

CSAVE //save program to cassette
    : 'CSAVE'
    ; 

CLOADM //load machine language program from cassette
    : 'CLOADM'
    ;   

CSAVEM //save machine language program to the cassette
    : 'CSAVEM'
    ; 

SKIPF //skip to the next program on the cassette
    : 'SKIPF'
    ; 

OPEN //open file for data transmission
    : 'OPEN'
    ; 

CLOSE //close acces to the specified device
    : 'CLOSE'
    ; 

COMMENT //match comment stuff '\n'
    : REM .*? '\r'? '\n' -> channel(2)
    ;

REM //comment
    : '\'' | 'REM'
    ;

/*******************small tokens**********************/

OR
   : 'OR'
   ;

AND
   : 'AND'
   ;

NOT
   : 'NOT'
   ;

DIGIT
   : '0'..'9'
   ;

LETTER
   : [A-Z]
   ;

DATUM //i think this should be actually much more inclusive - todo redo rule
   : [a-zA-Z0-9]
   ;

STRINGLITERAL
   : '"' ~["\r\n]* '"'
   ;

NUMBER
   : (DIGIT* '.'? DIGIT* ('E' ('+' | '-')? DIGIT+) | DIGIT* '.' DIGIT* | LINENUMBER)
   ;

LINENUMBER
   : DIGIT+
   ;

WS
   : [ \t]+ -> channel (HIDDEN)
   ;

 EOL
   : [\r\n] -> channel (HIDDEN)
   ;  

 /*******************fragments**********************/  
fragment
DEVICE_KEYBOARD
    : '0'
    ;

fragment
DEVICE_CASSETTE
    : '-1'
    ;

fragment
DEVICE_PRINTER
    : '-2'
    ;

fragment
DEVICE_RS232
    : '-3'
    ;