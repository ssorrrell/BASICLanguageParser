
grammar TestBasic;


/******************************Parser**************************************/
prog
   : line+ EOF
   ;

// a line starts with an integer
line
   : DIGIT_SEQUENCE (statement | COMMENT_BLOCK) (COLON (statement | COMMENT_BLOCK))* (EOL | EOF) //comments are removed. this is what a comment line looks like
   ;

/************************master statement table****************************/
statement
    : gotostmt
    | gosubstmt
    | letstmt
    ;

/******************************statements*********************************/

gotostmt
   : 'GO' 'TO' DIGIT_SEQUENCE
   ;

gosubstmt
   : 'GO' SUB DIGIT_SEQUENCE
   ;

letstmt
   : LET? (VARIABLE_STRING_ARRAY | VARIABLE_STRING) EQ characterExpression
   | LET? (VARIABLE_NUMBER_ARRAY | VARIABLE_NUMBER) EQ expression
   ;

/*****************************expressions*********************************/
expression
   : expression (MULTIPLICATION | DIVISION) expression
   | expression (ADDITION | SUBTRACTION) expression
   | expression (<assoc=right> '^' expression)
   | (<assoc=right> (ADDITION | SUBTRACTION) expression)
   | VARIABLE_NUMBER_ARRAY
   | VARIABLE_NUMBER
   | DIGIT_SEQUENCE
   | NUMBER
   | LPAREN expression RPAREN
   ;

characterExpression
   : characterExpression ADDITION characterExpression
   | VARIABLE_STRING_ARRAY
   | VARIABLE_STRING
   | STRINGLITERAL
   ;

/******************************Lexer***************************************/
LET //assign variables
   : 'LET'
   ;

// GO //goto
//    : 'GO'
//    ;

// TO //for to loop
//    : 'TO'
//    ;

SUB //gosub
   : 'SUB'
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

COLON
   : ':'
   ;

LPAREN
   : '('
   ;

RPAREN
   : ')'
   ;

VARIABLE_STRING_ARRAY
   : VARIABLE_STRING LPAREN DIGIT_SEQUENCE (',' DIGIT_SEQUENCE)* RPAREN
   ;

VARIABLE_NUMBER_ARRAY
   : VARIABLE_NUMBER LPAREN DIGIT_SEQUENCE (',' DIGIT_SEQUENCE)* RPAREN
   ;

VARIABLE_STRING
   : VARIABLE_NUMBER '$'
   ;

VARIABLE_NUMBER
   : LETTER (LETTER | DIGIT)*
   ;

DIGIT_SEQUENCE
   : DIGIT+
   ;

NUMBER
   : ( DIGIT* '.' DIGIT* | DIGIT* '.'? DIGIT* ('E' ('+' | '-')? DIGIT+) )
   ;   

COMMENT_BLOCK
   : COMMENT
   ;

STRINGLITERAL
   : '"' ~["\r\n]* '"'
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
LETTER
   : [A-Z]
   ;
   
fragment
SPACES //match sapce and tab
 : [ \t]+
 ;

fragment
DIGIT //match zero decimal digit
   : [0-9]
   ;

