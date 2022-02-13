
grammar TestBasic;


/******************************Parser**************************************/
prog
   : line+ EOF
   ;

// a line starts with an integer
line
   : DIGIT_SEQUENCE (statement | COMMENT_BLOCK) (':' (statement | COMMENT_BLOCK))* (EOL | EOF) //comments are removed. this is what a comment line looks like
   ;

/************************master statement table****************************/
statement
    : letstmt
    ;

/******************************statements*********************************/

letstmt
   : LET? DIGIT_SEQUENCE EQ DIGIT_SEQUENCE
   ;

/******************************Lexer***************************************/
LET //assign variables
   : 'LET'
   ;

EQ //equals sign
   : '='
   ;

COMMENT_BLOCK
   : COMMENT
   ;

SKIP_
   : ( SPACES ) -> channel (HIDDEN)
   ;

DIGIT_SEQUENCE
   : DIGIT+
   ;

EOL
   : '\r'? '\n'
   ;

/******************************Lexer fragments********************************/
// fragments are not available to the parser

fragment
COMMENT //match: {comment stuff '\r\n'} and leave \r\n in the stream
    : ('\'' | 'REM') ~[\r\n]*
    ;

fragment
SPACES //match sapce and tab
 : [ \t]+
 ;

fragment
DIGIT //match zero decimal digit
   : [0-9]
   ;

