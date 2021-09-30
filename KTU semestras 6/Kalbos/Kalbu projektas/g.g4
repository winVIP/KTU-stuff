grammar g;
/*
 * Parser Rules
 */
prograam             : line+ EOF ;
line                : (assign | whileLoop | ifas | printToC | forBody | intervalC | method | methodCall | printToF) | NEWLINE;
//declare             : TYPE WORD ('=' (WORD | INT | FLOAT) | ) ENDL;
operation           : (WORD | INT | FLOAT) OPERATORS (WORD | INT | FLOAT);
condition           : (WORD | INT | FLOAT) LOPS (WORD | INT | FLOAT);
intervalC           : (WORD | INT | FLOAT) LOPS (WORD | INT | FLOAT) LOPS (WORD | INT | FLOAT);
assign              : (WORD) '=' (WORD | INT | operation | FLOAT | TEXT | condition | intervalC | BOOL | methodCall) ENDL;
whileLoop           : 'while(' (condition | intervalC | ) ')' NEWLINE '{' NEWLINE (line+ | ) '}';
ifas                : 'if(' (condition | intervalC) ')' NEWLINE '{' NEWLINE (line+ | )'}';
printToC            : 'print('(TEXT+ | WORD | FLOAT | methodCall)')' ENDL;
forCondition        : assign (condition | intervalC) ENDL assign;
forBody             : 'for(' (forCondition | forCondition forCondition) ')' NEWLINE '{' NEWLINE (line+ | ) '}';
method              : 'def' WORD ENDL NEWLINE '{' NEWLINE (line+ | ) '}';
methodCall          : WORD '()' ENDL;
printToF            : 'printf(' WORD',' WORD ')' ENDL;
// for                 : 'for(' declare ENDL condition ENDL assign ')' NEWLINE '{' '}' ;

/*
 * Lexer Rules
 */
fragment LOWERCASE  : [a-z] ;
fragment UPPERCASE  : [A-Z] ;

BOOL                : ('true' | 'false');
INT                 : [0-9]+ ;
WORD                : (LOWERCASE | UPPERCASE | '_') ((LOWERCASE | UPPERCASE | INT | '_')+ | );
PARAMNAME           : (LOWERCASE | UPPERCASE | '_') (LOWERCASE | UPPERCASE | INT | '_')+;
FLOAT               : [0-9]+ '.' [0-9]+;
OPERATORS           : ('+' | '*' | '-' | '/');
LOPS                : ('==' | '<' | '>' | '<=' | '>=' | '!=');
ANDOR               : ('&&' | '||');
WHITESPACE          : (' ' | '\t')+ -> skip;
ENDL                : (';');
NEWLINE             : ('\r'? '\n' | '\r')+ ;
TEXT                : ('\'') ~[\])]+ ('\'');