
grammar ColorBasic;

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
   : line + EOF
   ;

// a line starts with an integer
line
   : (linenumber (statement (COLON statement)*) )
   ;

linenumber
   : DIGITS
   ;

/****************************master statement table*************************************/
statement
   : //(CLS | LOAD | SAVE | TRACE | NOTRACE | FLASH | INVERSE | GR | NORMAL | SHLOAD | CLEAR | RUN | STOP | TEXT | HOME | HGR | HGR2)
//    | endstmt
   | returnstmt
   | restorestmt
//    | amptstmt
//    | popstmt
//    | liststmt
//    | storestmt
//    | getstmt
//    | recallstmt
   | nextstmt
//    | instmt
//    | prstmt
//    | onerrstmt
//    | hlinstmt
//    | vlinstmt
//    | colorstmt
//    | speedstmt
//    | scalestmt
//    | rotstmt
//    | hcolorstmt
//    | himemstmt
//    | lomemstmt
//    | printstmt1
//    | pokestmt
//    | plotstmt
//    | ongotostmt
//    | ongosubstmt
    | ifstmt1
    | ifstmt2
    | forstmt
//    | forstmt2
//    | inputstmt
//    | tabstmt
    | dimstmt
    | gotostmt
    | gosubstmt
//    | callstmt
    | readstmt
//    | hplotstmt
//    | vplotstmt
//    | vtabstmnt
//    | htabstmnt
//    | waitstmt
   | datastmt
//    | xdrawstmt
//    | drawstmt
//    | defstmt
    | newstmt
    | stopstmt
    | endstmt
    | runstmt
    | letstmt
//    | includestmt
   ;

/****************************master function table*************************************/
func_
   : STRINGLITERAL
   | number
//    | tabfunc
   | vardecl
   | chrfunc
//    | sqrfunc
   | lenfunc
   | strfunc
//    | ascfunc
//    | scrnfunc
   | midfunc
//    | pdlfunc
   | peekfunc
   | intfunc
//    | spcfunc
//    | frefunc
//    | posfunc
   | leftfunc
   | valfunc
   | rightfunc
//    | fnfunc
   | sinfunc
//    | cosfunc
//    | tanfunc
//    | atnfunc
   | rndfunc
   | sgnfunc
//    | expfunc
//    | logfunc
   | absfunc
   | inkeyfunc
   | joystkfunc
   | eoffunc
   | pointfunc
   | memfunc
   | usrfunc
   | (LPAREN expression RPAREN)
   ;

// expressions and such
number
   :  ('+' | '-')? (NUMBER)
   ;

signExpression
   : NOT+ (ADD | SUBTRACT)? func_
   ;

exponentExpression
   : signExpression (EXP signExpression)*
   ;

multiplyingExpression
   : exponentExpression ((MUL | DIV) exponentExpression)*
   ;

addingExpression
   : multiplyingExpression ((ADD | SUBTRACT) multiplyingExpression)*
   ;

relationalExpression
   : addingExpression ((relop) addingExpression)?
   ;

expression
   : func_
   | (relationalExpression ((AND | OR) relationalExpression)*)
   ;

relop
   : gte
   | lte
   | neq
   | EQ
   | GT
   | LT
   ;

neq
   : LT GT
   | GT LT
   ;

gte
    : GT EQ
    ;

lte
    : LT EQ
    ;

var_
   : varname varsuffix?
   ;

varname
   : LETTERS (LETTERS | DIGITS)*
   ;

varsuffix
   : DOLLAR
   ;

varlist
   : vardecl (COMMA vardecl)*
   ;

vardecl
   : var_ (LPAREN exprlist RPAREN)*
   ;

variableassignment
   : vardecl EQ exprlist
   ;

exprlist
   : expression (COMMA expression)*
   ;

datum
   : number
   | DATUM
   ;

/*******************functions**********************/
absfunc
   : ABS LPAREN expression RPAREN
   ;

ascfunc
   : ASC LPAREN expression RPAREN
   ;

sgnfunc
   : SGN LPAREN expression RPAREN
   ;

intfunc
   : INT LPAREN expression RPAREN
   ;

sinfunc
   : SIN LPAREN expression RPAREN
   ;

rndfunc
   : RND LPAREN expression RPAREN
   ;

lenfunc
   : LEN LPAREN expression RPAREN
   ;

valfunc
   : VAL LPAREN expression RPAREN
   ;

chrfunc
   : CHR LPAREN expression RPAREN
   ;

midfunc
   : MID LPAREN expression COMMA expression COMMA expression RPAREN
   ;

leftfunc
   : LEFT LPAREN expression COMMA expression RPAREN
   ;

rightfunc
   : RIGHT LPAREN expression COMMA expression RPAREN
   ; 

strfunc
   : STR LPAREN expression RPAREN
   ;

inkeyfunc
   : INKEY
   ;

joystkfunc
   : JOYSTK LPAREN expression RPAREN
   ;

eoffunc
   : EOFTOKEN LPAREN expression RPAREN
   ;

peekfunc
   : PEEK LPAREN expression RPAREN
   ;

pointfunc
   : POINT LPAREN expression COMMA expression RPAREN
   ;

memfunc
   : MEM
   ; 

usrfunc
   : USR SINGLE_DIGIT LPAREN expression RPAREN
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
   : FOR vardecl EQ expression TO expression (STEP expression)?
   ;

nextstmt
   : NEXT (vardecl (',' vardecl)*)?
   ;

ifstmt1
   : IF expression THEN? ((statement)+ | linenumber)
   ;

ifstmt2
   : IF expression THEN ((statement)+ | linenumber) ELSE ((statement)+ | linenumber)
   ;

gotostmt
   : GO TO linenumber
   ;

gosubstmt
   : GO SUB linenumber
   ;

ongotostmt
   : ON expression GO TO linenumber (COMMA linenumber)*
   ;

ongosubstmt
   : ON expression GO SUB linenumber (COMMA linenumber)*
   ;

returnstmt
   : RETURN
   ;

datastmt
   : DATA datum (COMMA datum?)*
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

PEEK
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

FOR
   : 'FOR'
   ;

TO
   : 'TO'
   ;

STEP
   : 'STEP'
   ;

NEXT
   : 'NEXT'
   ;

IF
   : 'IF'
   ;

THEN
   : 'THEN'
   ;

ELSE
   : 'ELSE'
   ;

GO
   : 'GO'
   ;

SUB
   : 'SUB'
   ;

ON
   : 'ON'
   ;

RETURN
   : 'RETURN'
   ;

DATA
   : 'DATA'
   ;

RESTORE
   : 'RESTORE'
   ;

READ
   : 'READ'
   ;

NEW
   : 'NEW'
   ;

END
   : 'END'
   ;

STOP
   : 'STOP'
   ;

RUN
   : 'RUN'
   ;

/*******************small tokens**********************/

DOLLAR
   : '$'
   ;

PERCENT
   : '%'
   ;

ADD
   : '+'
   ;

SUBTRACT
   : '-'
   ;

MUL
   : '*'
   ;

DIV
   : '/'
   ;

GT
   : '>'
   ;

LT
   : '<'
   ;

EQ
   : '='
   ;

COMMA
   : ','
   ;

SEMICOLON
   : ';'
   ;

COLON
   : ':'
   ;

EXP
   : '^'
   ;

OR
   : 'OR'
   ;

AND
   : 'AND'
   ;

NOT
   : 'NOT'
   ;

LPAREN
    : '('
    ;

RPAREN
    : ')'
    ;

DATUM //i think this is actually much more inclusive - todo redo rule
   : [a-zA-Z0-9]+
   ;

LETTERS
   : [A-Z]+
   ;

SINGLE_DIGIT
   : [0-9]
   ;

DIGITS
   : [0-9]+
   ;

NUMBER
   : [0-9]* '.'? [0-9]* (('E')('+' | '-')? [0-9]+)*
   ;

STRINGLITERAL
   : '"' ~ ["\r\n]* '"'
   ;

WS
   : [ \t] + -> channel (HIDDEN)
   ;