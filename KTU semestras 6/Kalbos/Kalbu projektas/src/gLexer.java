// Generated from C:/Users/vyten/Desktop/Kalbos/Kalbu projektas\g.g4 by ANTLR 4.8
import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class gLexer extends Lexer {
	static { RuntimeMetaData.checkVersion("4.8", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, BOOL=13, INT=14, WORD=15, PARAMNAME=16, FLOAT=17, 
		OPERATORS=18, LOPS=19, ANDOR=20, WHITESPACE=21, ENDL=22, NEWLINE=23, TEXT=24;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	private static String[] makeRuleNames() {
		return new String[] {
			"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
			"T__9", "T__10", "T__11", "LOWERCASE", "UPPERCASE", "BOOL", "INT", "WORD", 
			"PARAMNAME", "FLOAT", "OPERATORS", "LOPS", "ANDOR", "WHITESPACE", "ENDL", 
			"NEWLINE", "TEXT"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'='", "'while('", "')'", "'{'", "'}'", "'if('", "'print('", "'for('", 
			"'def'", "'()'", "'printf('", "','"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, "BOOL", "INT", "WORD", "PARAMNAME", "FLOAT", "OPERATORS", "LOPS", 
			"ANDOR", "WHITESPACE", "ENDL", "NEWLINE", "TEXT"
		};
	}
	private static final String[] _SYMBOLIC_NAMES = makeSymbolicNames();
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}


	public gLexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "g.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public String[] getChannelNames() { return channelNames; }

	@Override
	public String[] getModeNames() { return modeNames; }

	@Override
	public ATN getATN() { return _ATN; }

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2\32\u00d0\b\1\4\2"+
		"\t\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4"+
		"\13\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22"+
		"\t\22\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31"+
		"\t\31\4\32\t\32\4\33\t\33\3\2\3\2\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\4\3\4"+
		"\3\5\3\5\3\6\3\6\3\7\3\7\3\7\3\7\3\b\3\b\3\b\3\b\3\b\3\b\3\b\3\t\3\t\3"+
		"\t\3\t\3\t\3\n\3\n\3\n\3\n\3\13\3\13\3\13\3\f\3\f\3\f\3\f\3\f\3\f\3\f"+
		"\3\f\3\r\3\r\3\16\3\16\3\17\3\17\3\20\3\20\3\20\3\20\3\20\3\20\3\20\3"+
		"\20\3\20\5\20u\n\20\3\21\6\21x\n\21\r\21\16\21y\3\22\3\22\3\22\5\22\177"+
		"\n\22\3\22\3\22\3\22\3\22\6\22\u0085\n\22\r\22\16\22\u0086\3\22\5\22\u008a"+
		"\n\22\3\23\3\23\3\23\5\23\u008f\n\23\3\23\3\23\3\23\3\23\6\23\u0095\n"+
		"\23\r\23\16\23\u0096\3\24\6\24\u009a\n\24\r\24\16\24\u009b\3\24\3\24\6"+
		"\24\u00a0\n\24\r\24\16\24\u00a1\3\25\3\25\3\26\3\26\3\26\3\26\3\26\3\26"+
		"\3\26\3\26\3\26\5\26\u00af\n\26\3\27\3\27\3\27\3\27\5\27\u00b5\n\27\3"+
		"\30\6\30\u00b8\n\30\r\30\16\30\u00b9\3\30\3\30\3\31\3\31\3\32\5\32\u00c1"+
		"\n\32\3\32\3\32\6\32\u00c5\n\32\r\32\16\32\u00c6\3\33\3\33\6\33\u00cb"+
		"\n\33\r\33\16\33\u00cc\3\33\3\33\2\2\34\3\3\5\4\7\5\t\6\13\7\r\b\17\t"+
		"\21\n\23\13\25\f\27\r\31\16\33\2\35\2\37\17!\20#\21%\22\'\23)\24+\25-"+
		"\26/\27\61\30\63\31\65\32\3\2\t\3\2c|\3\2C\\\3\2\62;\5\2,-//\61\61\4\2"+
		">>@@\4\2\13\13\"\"\4\2++__\2\u00e8\2\3\3\2\2\2\2\5\3\2\2\2\2\7\3\2\2\2"+
		"\2\t\3\2\2\2\2\13\3\2\2\2\2\r\3\2\2\2\2\17\3\2\2\2\2\21\3\2\2\2\2\23\3"+
		"\2\2\2\2\25\3\2\2\2\2\27\3\2\2\2\2\31\3\2\2\2\2\37\3\2\2\2\2!\3\2\2\2"+
		"\2#\3\2\2\2\2%\3\2\2\2\2\'\3\2\2\2\2)\3\2\2\2\2+\3\2\2\2\2-\3\2\2\2\2"+
		"/\3\2\2\2\2\61\3\2\2\2\2\63\3\2\2\2\2\65\3\2\2\2\3\67\3\2\2\2\59\3\2\2"+
		"\2\7@\3\2\2\2\tB\3\2\2\2\13D\3\2\2\2\rF\3\2\2\2\17J\3\2\2\2\21Q\3\2\2"+
		"\2\23V\3\2\2\2\25Z\3\2\2\2\27]\3\2\2\2\31e\3\2\2\2\33g\3\2\2\2\35i\3\2"+
		"\2\2\37t\3\2\2\2!w\3\2\2\2#~\3\2\2\2%\u008e\3\2\2\2\'\u0099\3\2\2\2)\u00a3"+
		"\3\2\2\2+\u00ae\3\2\2\2-\u00b4\3\2\2\2/\u00b7\3\2\2\2\61\u00bd\3\2\2\2"+
		"\63\u00c4\3\2\2\2\65\u00c8\3\2\2\2\678\7?\2\28\4\3\2\2\29:\7y\2\2:;\7"+
		"j\2\2;<\7k\2\2<=\7n\2\2=>\7g\2\2>?\7*\2\2?\6\3\2\2\2@A\7+\2\2A\b\3\2\2"+
		"\2BC\7}\2\2C\n\3\2\2\2DE\7\177\2\2E\f\3\2\2\2FG\7k\2\2GH\7h\2\2HI\7*\2"+
		"\2I\16\3\2\2\2JK\7r\2\2KL\7t\2\2LM\7k\2\2MN\7p\2\2NO\7v\2\2OP\7*\2\2P"+
		"\20\3\2\2\2QR\7h\2\2RS\7q\2\2ST\7t\2\2TU\7*\2\2U\22\3\2\2\2VW\7f\2\2W"+
		"X\7g\2\2XY\7h\2\2Y\24\3\2\2\2Z[\7*\2\2[\\\7+\2\2\\\26\3\2\2\2]^\7r\2\2"+
		"^_\7t\2\2_`\7k\2\2`a\7p\2\2ab\7v\2\2bc\7h\2\2cd\7*\2\2d\30\3\2\2\2ef\7"+
		".\2\2f\32\3\2\2\2gh\t\2\2\2h\34\3\2\2\2ij\t\3\2\2j\36\3\2\2\2kl\7v\2\2"+
		"lm\7t\2\2mn\7w\2\2nu\7g\2\2op\7h\2\2pq\7c\2\2qr\7n\2\2rs\7u\2\2su\7g\2"+
		"\2tk\3\2\2\2to\3\2\2\2u \3\2\2\2vx\t\4\2\2wv\3\2\2\2xy\3\2\2\2yw\3\2\2"+
		"\2yz\3\2\2\2z\"\3\2\2\2{\177\5\33\16\2|\177\5\35\17\2}\177\7a\2\2~{\3"+
		"\2\2\2~|\3\2\2\2~}\3\2\2\2\177\u0089\3\2\2\2\u0080\u0085\5\33\16\2\u0081"+
		"\u0085\5\35\17\2\u0082\u0085\5!\21\2\u0083\u0085\7a\2\2\u0084\u0080\3"+
		"\2\2\2\u0084\u0081\3\2\2\2\u0084\u0082\3\2\2\2\u0084\u0083\3\2\2\2\u0085"+
		"\u0086\3\2\2\2\u0086\u0084\3\2\2\2\u0086\u0087\3\2\2\2\u0087\u008a\3\2"+
		"\2\2\u0088\u008a\3\2\2\2\u0089\u0084\3\2\2\2\u0089\u0088\3\2\2\2\u008a"+
		"$\3\2\2\2\u008b\u008f\5\33\16\2\u008c\u008f\5\35\17\2\u008d\u008f\7a\2"+
		"\2\u008e\u008b\3\2\2\2\u008e\u008c\3\2\2\2\u008e\u008d\3\2\2\2\u008f\u0094"+
		"\3\2\2\2\u0090\u0095\5\33\16\2\u0091\u0095\5\35\17\2\u0092\u0095\5!\21"+
		"\2\u0093\u0095\7a\2\2\u0094\u0090\3\2\2\2\u0094\u0091\3\2\2\2\u0094\u0092"+
		"\3\2\2\2\u0094\u0093\3\2\2\2\u0095\u0096\3\2\2\2\u0096\u0094\3\2\2\2\u0096"+
		"\u0097\3\2\2\2\u0097&\3\2\2\2\u0098\u009a\t\4\2\2\u0099\u0098\3\2\2\2"+
		"\u009a\u009b\3\2\2\2\u009b\u0099\3\2\2\2\u009b\u009c\3\2\2\2\u009c\u009d"+
		"\3\2\2\2\u009d\u009f\7\60\2\2\u009e\u00a0\t\4\2\2\u009f\u009e\3\2\2\2"+
		"\u00a0\u00a1\3\2\2\2\u00a1\u009f\3\2\2\2\u00a1\u00a2\3\2\2\2\u00a2(\3"+
		"\2\2\2\u00a3\u00a4\t\5\2\2\u00a4*\3\2\2\2\u00a5\u00a6\7?\2\2\u00a6\u00af"+
		"\7?\2\2\u00a7\u00af\t\6\2\2\u00a8\u00a9\7>\2\2\u00a9\u00af\7?\2\2\u00aa"+
		"\u00ab\7@\2\2\u00ab\u00af\7?\2\2\u00ac\u00ad\7#\2\2\u00ad\u00af\7?\2\2"+
		"\u00ae\u00a5\3\2\2\2\u00ae\u00a7\3\2\2\2\u00ae\u00a8\3\2\2\2\u00ae\u00aa"+
		"\3\2\2\2\u00ae\u00ac\3\2\2\2\u00af,\3\2\2\2\u00b0\u00b1\7(\2\2\u00b1\u00b5"+
		"\7(\2\2\u00b2\u00b3\7~\2\2\u00b3\u00b5\7~\2\2\u00b4\u00b0\3\2\2\2\u00b4"+
		"\u00b2\3\2\2\2\u00b5.\3\2\2\2\u00b6\u00b8\t\7\2\2\u00b7\u00b6\3\2\2\2"+
		"\u00b8\u00b9\3\2\2\2\u00b9\u00b7\3\2\2\2\u00b9\u00ba\3\2\2\2\u00ba\u00bb"+
		"\3\2\2\2\u00bb\u00bc\b\30\2\2\u00bc\60\3\2\2\2\u00bd\u00be\7=\2\2\u00be"+
		"\62\3\2\2\2\u00bf\u00c1\7\17\2\2\u00c0\u00bf\3\2\2\2\u00c0\u00c1\3\2\2"+
		"\2\u00c1\u00c2\3\2\2\2\u00c2\u00c5\7\f\2\2\u00c3\u00c5\7\17\2\2\u00c4"+
		"\u00c0\3\2\2\2\u00c4\u00c3\3\2\2\2\u00c5\u00c6\3\2\2\2\u00c6\u00c4\3\2"+
		"\2\2\u00c6\u00c7\3\2\2\2\u00c7\64\3\2\2\2\u00c8\u00ca\7)\2\2\u00c9\u00cb"+
		"\n\b\2\2\u00ca\u00c9\3\2\2\2\u00cb\u00cc\3\2\2\2\u00cc\u00ca\3\2\2\2\u00cc"+
		"\u00cd\3\2\2\2\u00cd\u00ce\3\2\2\2\u00ce\u00cf\7)\2\2\u00cf\66\3\2\2\2"+
		"\25\2ty~\u0084\u0086\u0089\u008e\u0094\u0096\u009b\u00a1\u00ae\u00b4\u00b9"+
		"\u00c0\u00c4\u00c6\u00cc\3\b\2\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}