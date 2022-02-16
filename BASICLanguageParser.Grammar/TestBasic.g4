
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

expression
   : expression (MULTIPLICATION | DIVISION) expression
   | expression (ADDITION | SUBTRACTION) expression
   | expression (<assoc=right> '^' expression)
   | (ADDITION | SUBTRACTION) expression
   | VARIABLE_NUMBER_ARRAY
   | VARIABLE_NUMBER
   | NUMBER
   | '(' expression ')'
   ;

characterExpression
   : characterExpression ADDITION characterExpression
   | VARIABLE_STRING_ARRAY
   | VARIABLE_STRING
   | STRINGLITERAL
   ;

/******************************statements*********************************/

letstmt
   : LET? (VARIABLE_NUMBER_ARRAY | VARIABLE_NUMBER) EQ NUMBER
   ;

/******************************Lexer***************************************/
LET //assign variables
   : 'LET'
   ;

EQ //equals sign
   : '='
   ;

ADDITION
   : '+'
   ;

SUBTRACTION
   : '-'
   ;

MULTIPLICATION
   : '*'
   ;
   
DIVISION
   : '/'
   ;

VARIABLE_NUMBER
   : LETTER (LETTER | DIGIT)*
   ;

VARIABLE_STRING
   : VARIABLE_NUMBER '$'
   ;

VARIABLE_NUMBER_ARRAY
   : VARIABLE_NUMBER '(' DIGIT_SEQUENCE (',' DIGIT_SEQUENCE)* ')'
   ;

VARIABLE_STRING_ARRAY
   : VARIABLE_STRING '(' DIGIT_SEQUENCE (',' DIGIT_SEQUENCE)* ')'
   ;

LETTER
   : [A-Z]
   ;

STRINGLITERAL
   : '"' ~["\r\n]* '"'
   ;

DIGIT_SEQUENCE
   : DIGIT+
   ;

NUMBER
   : (DIGIT+ | DIGIT* '.' DIGIT* | DIGIT* '.'? DIGIT* ('E' ('+' | '-')? DIGIT+))
   ;   

COMMENT_BLOCK
   : COMMENT
   ;

SKIP_
   : ( SPACES ) -> channel (HIDDEN)
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

