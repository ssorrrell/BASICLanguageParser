
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
    : gotonumstmt
    | gotostmt
    | gosubnumstmt
    | gosubstmt
    | ongotonumstmt
    | ongotostmt
    | ongosubnumstmt
    | ongosubstmt
    | returnstmt
    | ifthenelsestmt
    | ifthenelsenumstmt
    | ifthenstmt
    | ifnumelsenumstmt
    | ifnumstmt
    | ifstmt
    | letstmt
    ;

/******************************statements*********************************/
ongotonumstmt //this definition requires a space around expression
   : ON expression ( GOTO_NUM | GO TO_NUM ) (',' DIGIT_SEQUENCE)*
   ;

ongotostmt //this definition requires a space around expression
   : ON expression ( GO TO | GOTO ) DIGIT_SEQUENCE (',' DIGIT_SEQUENCE)*
   ;

ongosubnumstmt //this definition requires a space around expression
   : ON expression ( GOSUB_NUM | GO SUB_NUM ) (',' DIGIT_SEQUENCE)*
   ;

ongosubstmt //this definition requires a space around expression
   : ON expression ( GO SUB | GOSUB ) DIGIT_SEQUENCE (',' DIGIT_SEQUENCE)*
   ;

gotonumstmt //seperate from gotostmt in order to extract the line number
   : ( GOTO_NUM
   | ( GO TO_NUM ))
   ;

gotostmt
   : ( ( GO TO | GOTO ) DIGIT_SEQUENCE )
   ;

gosubnumstmt //seperate from gosubstmt in order to extract the line number
   : ( GOSUB_NUM
   | ( GO SUB_NUM ) )
   ;

gosubstmt
   : ( ( GO SUB | GOSUB ) DIGIT_SEQUENCE )
   ;

returnstmt
   : RETURN
   ;

ifthenelsestmt //requires space around expression
   : IF relationalExpression THEN (statement | DIGIT_SEQUENCE) ELSE (statement | DIGIT_SEQUENCE)
   ;

ifthenelsenumstmt //requires space around expression
   : IF relationalExpression THEN (statement | DIGIT_SEQUENCE) ELSE_NUM
   ;

ifthenstmt //requires space around expression
   : IF relationalExpression THEN (statement | DIGIT_SEQUENCE)
   ;

ifnumelsenumstmt //if expr then500else500
   : IF relationalExpression (THEN_NUM_ELSE)
   ;

ifnumstmt //if expr then500
   : IF relationalExpression (THEN_NUM)
   ;

ifstmt //if expr 500
   : IF relationalExpression DIGIT_SEQUENCE
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
   | VARIABLE_NUMBER_ARRAY
   | VARIABLE_NUMBER
   | STRINGLITERAL
   | LPAREN characterExpression RPAREN
   ;

/************************relation operations****************************/

relationalExpression
   : relationalExpression logicalOperator relationalExpression
   | relationalExpression (<assoc=right> NOT relationalExpression)
   | expression relationalOperator expression
   | characterExpression relationalOperator characterExpression
   | LPAREN relationalExpression RPAREN
   ;

relationalOperator
   : GTE
   | LTE
   | NEQ
   | EQ
   | LT
   | GT
   ;

logicalOperator
   : AND
   | OR
   ;

/******************************Lexer***************************************/
GO //go
   : 'GO'
   ;

GOTO //goto
   : 'GOTO'
   ;

GOTO_NUM //goto500
   : 'GOTO' DIGIT_SEQUENCE
   ;

TO //T0
   : 'TO'
   ;

TO_NUM //TO500
   : 'TO' DIGIT_SEQUENCE
   ;

GOSUB //gosub
   : 'GOSUB'
   ;

GOSUB_NUM //gosub500
   : 'GOSUB' DIGIT_SEQUENCE
   ;

SUB //sub
   : 'SUB'
   ;

SUB_NUM //sub500
   : 'SUB' DIGIT_SEQUENCE
   ;

ON
   : 'ON'
   ;

RETURN //return
   : 'RETURN'
   ;

IF //if 
   : 'IF'
   ;

THEN //if then
   : 'THEN'
   ;

THEN_NUM //then500
   : 'THEN' DIGIT_SEQUENCE
   ;

THEN_NUM_ELSE //then500else
   : 'THEN' DIGIT_SEQUENCE 'ELSE'
   ;

ELSE //if then else
   : 'ELSE'
   ;

ELSE_NUM //else500
   : 'ELSE' DIGIT_SEQUENCE
   ;

LET //assign variables
   : 'LET'
   ;

AND
   : 'AND'
   ;

OR
   : 'OR'
   ;

NOT
   : 'NOT'
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

NEQ
   : '<' '>'
   | '>' '<'
   ;

GTE
    : '>' '='
    ;

LTE
    : '<' '='
    ;

LT
   : '<'
   ;

GT
   : '>'
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

