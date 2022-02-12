
grammar TestBasic;


/******************************Parser***************************************/
prog
   : line+ EOF
   ;

// a line starts with an integer
line
   : DIGIT_SEQUENCE (statement | COMMENT) (':' (statement | COMMENT))* (EOL | EOF)
   ;

/************************master statement table*******************************/
statement
    : letstmt
    ;

/******************************statements*********************************/

letstmt
   : LET? DIGIT_SEQUENCE
   ;

/******************************Lexer***************************************/
LET //assign variables
   : 'LET'
   ;

COMMENT //match comment stuff '\n'
    : REM .*? (EOL | EOF)-> skip
    ;

REM //comment
    : '\''
    | 'REM'
    ;

DIGIT_SEQUENCE
   : DIGIT+
   ;

WS
   : [ \t]+ -> channel (HIDDEN)
   ;

 EOL
   : '\r\n'
   ; 

/******************************Lexer fragments********************************/
fragment
DIGIT
   : [0-9]
   ;

