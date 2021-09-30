grammar g;
/*
 * Parser Rules
 */
program             : line+ EOF ;
line                : (declare | assign | whileLoop | ifas | printToC) | NEWLINE;
declare             : TYPE  WORD ENDL;
name                : WORD ;
operation           : (WORD | INT) OPERATORS (WORD | INT);
condition           : (WORD | INT) LOPS (WORD | INT) (ANDOR condition | );
assign              : WORD '=' (WORD | INT | operation | FLOAT | TEXT) ENDL;
whileLoop           : 'while(' (condition | ) ')' NEWLINE '{' NEWLINE (line+ | ) '}';
ifas                  : 'if(' condition ')' NEWLINE '{' NEWLINE (line+ | ) '}' (NEWLINE | ) elseifas (NEWLINE | ) elseas;
elseifas              : (('elseif(' condition ')' NEWLINE '{' (line+ | ) '}' (NEWLINE | ))+ | );
elseas                : (('else' NEWLINE '{' (line+ | ) '}' (NEWLINE | )) | );
printToC            : 'print('(TEXT+ | WORD)')' ENDL;
// for                 : 'for(' declare ENDL condition ENDL assign ')' NEWLINE '{' '}' ;

/*
 * Lexer Rules
 */
fragment LOWERCASE  : [a-z] ;
fragment UPPERCASE  : [A-Z] ;

TYPE                : ('int' | 'string' | 'float');
INT                 : [0-9]+ ;
WORD                : (LOWERCASE | UPPERCASE | '_')+ ;
FLOAT               : [0-9]+ '.' [0-9]+;
OPERATORS           : ('+' | '*' | '-' | '/');
LOPS                : ('==' | '<' | '>' | '<=' | '>=' | '!=');
ANDOR               : ('&&' | '||');
WHITESPACE          : (' ' | '\t')+ -> skip;
ENDL                : (';');
NEWLINE             : ('\r'? '\n' | '\r')+ ;
TEXT                : ('\'') ~[\])]+ ('\'');