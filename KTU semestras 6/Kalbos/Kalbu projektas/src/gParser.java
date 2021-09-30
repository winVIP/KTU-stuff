// Generated from C:/Users/vyten/Desktop/Kalbos/Kalbu projektas\g.g4 by ANTLR 4.8
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class gParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.8", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, BOOL=13, INT=14, WORD=15, PARAMNAME=16, FLOAT=17, 
		OPERATORS=18, LOPS=19, ANDOR=20, WHITESPACE=21, ENDL=22, NEWLINE=23, TEXT=24;
	public static final int
		RULE_program = 0, RULE_line = 1, RULE_operation = 2, RULE_condition = 3, 
		RULE_intervalC = 4, RULE_assign = 5, RULE_whileLoop = 6, RULE_ifas = 7, 
		RULE_printToC = 8, RULE_forCondition = 9, RULE_forBody = 10, RULE_method = 11, 
		RULE_methodCall = 12, RULE_printToF = 13;
	private static String[] makeRuleNames() {
		return new String[] {
			"program", "line", "operation", "condition", "intervalC", "assign", "whileLoop", 
			"ifas", "printToC", "forCondition", "forBody", "method", "methodCall", 
			"printToF"
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

	@Override
	public String getGrammarFileName() { return "g.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public gParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	public static class ProgramContext extends ParserRuleContext {
		public TerminalNode EOF() { return getToken(gParser.EOF, 0); }
		public List<LineContext> line() {
			return getRuleContexts(LineContext.class);
		}
		public LineContext line(int i) {
			return getRuleContext(LineContext.class,i);
		}
		public ProgramContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_program; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).enterProgram(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).exitProgram(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof gVisitor ) return ((gVisitor<? extends T>)visitor).visitProgram(this);
			else return visitor.visitChildren(this);
		}
	}

	public final ProgramContext program() throws RecognitionException {
		ProgramContext _localctx = new ProgramContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_program);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(29); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(28);
				line();
				}
				}
				setState(31); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__1) | (1L << T__5) | (1L << T__6) | (1L << T__7) | (1L << T__8) | (1L << T__10) | (1L << INT) | (1L << WORD) | (1L << FLOAT) | (1L << NEWLINE))) != 0) );
			setState(33);
			match(EOF);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class LineContext extends ParserRuleContext {
		public AssignContext assign() {
			return getRuleContext(AssignContext.class,0);
		}
		public WhileLoopContext whileLoop() {
			return getRuleContext(WhileLoopContext.class,0);
		}
		public IfasContext ifas() {
			return getRuleContext(IfasContext.class,0);
		}
		public PrintToCContext printToC() {
			return getRuleContext(PrintToCContext.class,0);
		}
		public ForBodyContext forBody() {
			return getRuleContext(ForBodyContext.class,0);
		}
		public IntervalCContext intervalC() {
			return getRuleContext(IntervalCContext.class,0);
		}
		public MethodContext method() {
			return getRuleContext(MethodContext.class,0);
		}
		public MethodCallContext methodCall() {
			return getRuleContext(MethodCallContext.class,0);
		}
		public PrintToFContext printToF() {
			return getRuleContext(PrintToFContext.class,0);
		}
		public TerminalNode NEWLINE() { return getToken(gParser.NEWLINE, 0); }
		public LineContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_line; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).enterLine(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).exitLine(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof gVisitor ) return ((gVisitor<? extends T>)visitor).visitLine(this);
			else return visitor.visitChildren(this);
		}
	}

	public final LineContext line() throws RecognitionException {
		LineContext _localctx = new LineContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_line);
		try {
			setState(47);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__1:
			case T__5:
			case T__6:
			case T__7:
			case T__8:
			case T__10:
			case INT:
			case WORD:
			case FLOAT:
				enterOuterAlt(_localctx, 1);
				{
				setState(44);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,1,_ctx) ) {
				case 1:
					{
					setState(35);
					assign();
					}
					break;
				case 2:
					{
					setState(36);
					whileLoop();
					}
					break;
				case 3:
					{
					setState(37);
					ifas();
					}
					break;
				case 4:
					{
					setState(38);
					printToC();
					}
					break;
				case 5:
					{
					setState(39);
					forBody();
					}
					break;
				case 6:
					{
					setState(40);
					intervalC();
					}
					break;
				case 7:
					{
					setState(41);
					method();
					}
					break;
				case 8:
					{
					setState(42);
					methodCall();
					}
					break;
				case 9:
					{
					setState(43);
					printToF();
					}
					break;
				}
				}
				break;
			case NEWLINE:
				enterOuterAlt(_localctx, 2);
				{
				setState(46);
				match(NEWLINE);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class OperationContext extends ParserRuleContext {
		public TerminalNode OPERATORS() { return getToken(gParser.OPERATORS, 0); }
		public List<TerminalNode> WORD() { return getTokens(gParser.WORD); }
		public TerminalNode WORD(int i) {
			return getToken(gParser.WORD, i);
		}
		public List<TerminalNode> INT() { return getTokens(gParser.INT); }
		public TerminalNode INT(int i) {
			return getToken(gParser.INT, i);
		}
		public List<TerminalNode> FLOAT() { return getTokens(gParser.FLOAT); }
		public TerminalNode FLOAT(int i) {
			return getToken(gParser.FLOAT, i);
		}
		public OperationContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_operation; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).enterOperation(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).exitOperation(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof gVisitor ) return ((gVisitor<? extends T>)visitor).visitOperation(this);
			else return visitor.visitChildren(this);
		}
	}

	public final OperationContext operation() throws RecognitionException {
		OperationContext _localctx = new OperationContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_operation);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(49);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << INT) | (1L << WORD) | (1L << FLOAT))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			setState(50);
			match(OPERATORS);
			setState(51);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << INT) | (1L << WORD) | (1L << FLOAT))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ConditionContext extends ParserRuleContext {
		public TerminalNode LOPS() { return getToken(gParser.LOPS, 0); }
		public List<TerminalNode> WORD() { return getTokens(gParser.WORD); }
		public TerminalNode WORD(int i) {
			return getToken(gParser.WORD, i);
		}
		public List<TerminalNode> INT() { return getTokens(gParser.INT); }
		public TerminalNode INT(int i) {
			return getToken(gParser.INT, i);
		}
		public List<TerminalNode> FLOAT() { return getTokens(gParser.FLOAT); }
		public TerminalNode FLOAT(int i) {
			return getToken(gParser.FLOAT, i);
		}
		public ConditionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_condition; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).enterCondition(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).exitCondition(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof gVisitor ) return ((gVisitor<? extends T>)visitor).visitCondition(this);
			else return visitor.visitChildren(this);
		}
	}

	public final ConditionContext condition() throws RecognitionException {
		ConditionContext _localctx = new ConditionContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_condition);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(53);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << INT) | (1L << WORD) | (1L << FLOAT))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			setState(54);
			match(LOPS);
			setState(55);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << INT) | (1L << WORD) | (1L << FLOAT))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class IntervalCContext extends ParserRuleContext {
		public List<TerminalNode> LOPS() { return getTokens(gParser.LOPS); }
		public TerminalNode LOPS(int i) {
			return getToken(gParser.LOPS, i);
		}
		public List<TerminalNode> WORD() { return getTokens(gParser.WORD); }
		public TerminalNode WORD(int i) {
			return getToken(gParser.WORD, i);
		}
		public List<TerminalNode> INT() { return getTokens(gParser.INT); }
		public TerminalNode INT(int i) {
			return getToken(gParser.INT, i);
		}
		public List<TerminalNode> FLOAT() { return getTokens(gParser.FLOAT); }
		public TerminalNode FLOAT(int i) {
			return getToken(gParser.FLOAT, i);
		}
		public IntervalCContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_intervalC; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).enterIntervalC(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).exitIntervalC(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof gVisitor ) return ((gVisitor<? extends T>)visitor).visitIntervalC(this);
			else return visitor.visitChildren(this);
		}
	}

	public final IntervalCContext intervalC() throws RecognitionException {
		IntervalCContext _localctx = new IntervalCContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_intervalC);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(57);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << INT) | (1L << WORD) | (1L << FLOAT))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			setState(58);
			match(LOPS);
			setState(59);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << INT) | (1L << WORD) | (1L << FLOAT))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			setState(60);
			match(LOPS);
			setState(61);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << INT) | (1L << WORD) | (1L << FLOAT))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class AssignContext extends ParserRuleContext {
		public TerminalNode ENDL() { return getToken(gParser.ENDL, 0); }
		public List<TerminalNode> WORD() { return getTokens(gParser.WORD); }
		public TerminalNode WORD(int i) {
			return getToken(gParser.WORD, i);
		}
		public TerminalNode INT() { return getToken(gParser.INT, 0); }
		public OperationContext operation() {
			return getRuleContext(OperationContext.class,0);
		}
		public TerminalNode FLOAT() { return getToken(gParser.FLOAT, 0); }
		public TerminalNode TEXT() { return getToken(gParser.TEXT, 0); }
		public ConditionContext condition() {
			return getRuleContext(ConditionContext.class,0);
		}
		public IntervalCContext intervalC() {
			return getRuleContext(IntervalCContext.class,0);
		}
		public TerminalNode BOOL() { return getToken(gParser.BOOL, 0); }
		public MethodCallContext methodCall() {
			return getRuleContext(MethodCallContext.class,0);
		}
		public AssignContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_assign; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).enterAssign(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).exitAssign(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof gVisitor ) return ((gVisitor<? extends T>)visitor).visitAssign(this);
			else return visitor.visitChildren(this);
		}
	}

	public final AssignContext assign() throws RecognitionException {
		AssignContext _localctx = new AssignContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_assign);
		try {
			enterOuterAlt(_localctx, 1);
			{
			{
			setState(63);
			match(WORD);
			}
			setState(64);
			match(T__0);
			setState(74);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,3,_ctx) ) {
			case 1:
				{
				setState(65);
				match(WORD);
				}
				break;
			case 2:
				{
				setState(66);
				match(INT);
				}
				break;
			case 3:
				{
				setState(67);
				operation();
				}
				break;
			case 4:
				{
				setState(68);
				match(FLOAT);
				}
				break;
			case 5:
				{
				setState(69);
				match(TEXT);
				}
				break;
			case 6:
				{
				setState(70);
				condition();
				}
				break;
			case 7:
				{
				setState(71);
				intervalC();
				}
				break;
			case 8:
				{
				setState(72);
				match(BOOL);
				}
				break;
			case 9:
				{
				setState(73);
				methodCall();
				}
				break;
			}
			setState(76);
			match(ENDL);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class WhileLoopContext extends ParserRuleContext {
		public List<TerminalNode> NEWLINE() { return getTokens(gParser.NEWLINE); }
		public TerminalNode NEWLINE(int i) {
			return getToken(gParser.NEWLINE, i);
		}
		public ConditionContext condition() {
			return getRuleContext(ConditionContext.class,0);
		}
		public IntervalCContext intervalC() {
			return getRuleContext(IntervalCContext.class,0);
		}
		public List<LineContext> line() {
			return getRuleContexts(LineContext.class);
		}
		public LineContext line(int i) {
			return getRuleContext(LineContext.class,i);
		}
		public WhileLoopContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_whileLoop; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).enterWhileLoop(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).exitWhileLoop(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof gVisitor ) return ((gVisitor<? extends T>)visitor).visitWhileLoop(this);
			else return visitor.visitChildren(this);
		}
	}

	public final WhileLoopContext whileLoop() throws RecognitionException {
		WhileLoopContext _localctx = new WhileLoopContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_whileLoop);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(78);
			match(T__1);
			setState(82);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,4,_ctx) ) {
			case 1:
				{
				setState(79);
				condition();
				}
				break;
			case 2:
				{
				setState(80);
				intervalC();
				}
				break;
			case 3:
				{
				}
				break;
			}
			setState(84);
			match(T__2);
			setState(85);
			match(NEWLINE);
			setState(86);
			match(T__3);
			setState(87);
			match(NEWLINE);
			setState(94);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__1:
			case T__5:
			case T__6:
			case T__7:
			case T__8:
			case T__10:
			case INT:
			case WORD:
			case FLOAT:
			case NEWLINE:
				{
				setState(89); 
				_errHandler.sync(this);
				_la = _input.LA(1);
				do {
					{
					{
					setState(88);
					line();
					}
					}
					setState(91); 
					_errHandler.sync(this);
					_la = _input.LA(1);
				} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__1) | (1L << T__5) | (1L << T__6) | (1L << T__7) | (1L << T__8) | (1L << T__10) | (1L << INT) | (1L << WORD) | (1L << FLOAT) | (1L << NEWLINE))) != 0) );
				}
				break;
			case T__4:
				{
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			setState(96);
			match(T__4);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class IfasContext extends ParserRuleContext {
		public List<TerminalNode> NEWLINE() { return getTokens(gParser.NEWLINE); }
		public TerminalNode NEWLINE(int i) {
			return getToken(gParser.NEWLINE, i);
		}
		public ConditionContext condition() {
			return getRuleContext(ConditionContext.class,0);
		}
		public IntervalCContext intervalC() {
			return getRuleContext(IntervalCContext.class,0);
		}
		public List<LineContext> line() {
			return getRuleContexts(LineContext.class);
		}
		public LineContext line(int i) {
			return getRuleContext(LineContext.class,i);
		}
		public IfasContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_ifas; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).enterIfas(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).exitIfas(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof gVisitor ) return ((gVisitor<? extends T>)visitor).visitIfas(this);
			else return visitor.visitChildren(this);
		}
	}

	public final IfasContext ifas() throws RecognitionException {
		IfasContext _localctx = new IfasContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_ifas);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(98);
			match(T__5);
			setState(101);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,7,_ctx) ) {
			case 1:
				{
				setState(99);
				condition();
				}
				break;
			case 2:
				{
				setState(100);
				intervalC();
				}
				break;
			}
			setState(103);
			match(T__2);
			setState(104);
			match(NEWLINE);
			setState(105);
			match(T__3);
			setState(106);
			match(NEWLINE);
			setState(113);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__1:
			case T__5:
			case T__6:
			case T__7:
			case T__8:
			case T__10:
			case INT:
			case WORD:
			case FLOAT:
			case NEWLINE:
				{
				setState(108); 
				_errHandler.sync(this);
				_la = _input.LA(1);
				do {
					{
					{
					setState(107);
					line();
					}
					}
					setState(110); 
					_errHandler.sync(this);
					_la = _input.LA(1);
				} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__1) | (1L << T__5) | (1L << T__6) | (1L << T__7) | (1L << T__8) | (1L << T__10) | (1L << INT) | (1L << WORD) | (1L << FLOAT) | (1L << NEWLINE))) != 0) );
				}
				break;
			case T__4:
				{
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			setState(115);
			match(T__4);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class PrintToCContext extends ParserRuleContext {
		public TerminalNode ENDL() { return getToken(gParser.ENDL, 0); }
		public TerminalNode WORD() { return getToken(gParser.WORD, 0); }
		public TerminalNode FLOAT() { return getToken(gParser.FLOAT, 0); }
		public MethodCallContext methodCall() {
			return getRuleContext(MethodCallContext.class,0);
		}
		public List<TerminalNode> TEXT() { return getTokens(gParser.TEXT); }
		public TerminalNode TEXT(int i) {
			return getToken(gParser.TEXT, i);
		}
		public PrintToCContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_printToC; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).enterPrintToC(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).exitPrintToC(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof gVisitor ) return ((gVisitor<? extends T>)visitor).visitPrintToC(this);
			else return visitor.visitChildren(this);
		}
	}

	public final PrintToCContext printToC() throws RecognitionException {
		PrintToCContext _localctx = new PrintToCContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_printToC);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(117);
			match(T__6);
			setState(126);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,11,_ctx) ) {
			case 1:
				{
				setState(119); 
				_errHandler.sync(this);
				_la = _input.LA(1);
				do {
					{
					{
					setState(118);
					match(TEXT);
					}
					}
					setState(121); 
					_errHandler.sync(this);
					_la = _input.LA(1);
				} while ( _la==TEXT );
				}
				break;
			case 2:
				{
				setState(123);
				match(WORD);
				}
				break;
			case 3:
				{
				setState(124);
				match(FLOAT);
				}
				break;
			case 4:
				{
				setState(125);
				methodCall();
				}
				break;
			}
			setState(128);
			match(T__2);
			setState(129);
			match(ENDL);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ForConditionContext extends ParserRuleContext {
		public List<AssignContext> assign() {
			return getRuleContexts(AssignContext.class);
		}
		public AssignContext assign(int i) {
			return getRuleContext(AssignContext.class,i);
		}
		public TerminalNode ENDL() { return getToken(gParser.ENDL, 0); }
		public ConditionContext condition() {
			return getRuleContext(ConditionContext.class,0);
		}
		public IntervalCContext intervalC() {
			return getRuleContext(IntervalCContext.class,0);
		}
		public ForConditionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_forCondition; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).enterForCondition(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).exitForCondition(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof gVisitor ) return ((gVisitor<? extends T>)visitor).visitForCondition(this);
			else return visitor.visitChildren(this);
		}
	}

	public final ForConditionContext forCondition() throws RecognitionException {
		ForConditionContext _localctx = new ForConditionContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_forCondition);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(131);
			assign();
			setState(134);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,12,_ctx) ) {
			case 1:
				{
				setState(132);
				condition();
				}
				break;
			case 2:
				{
				setState(133);
				intervalC();
				}
				break;
			}
			setState(136);
			match(ENDL);
			setState(137);
			assign();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ForBodyContext extends ParserRuleContext {
		public List<TerminalNode> NEWLINE() { return getTokens(gParser.NEWLINE); }
		public TerminalNode NEWLINE(int i) {
			return getToken(gParser.NEWLINE, i);
		}
		public List<ForConditionContext> forCondition() {
			return getRuleContexts(ForConditionContext.class);
		}
		public ForConditionContext forCondition(int i) {
			return getRuleContext(ForConditionContext.class,i);
		}
		public List<LineContext> line() {
			return getRuleContexts(LineContext.class);
		}
		public LineContext line(int i) {
			return getRuleContext(LineContext.class,i);
		}
		public ForBodyContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_forBody; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).enterForBody(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).exitForBody(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof gVisitor ) return ((gVisitor<? extends T>)visitor).visitForBody(this);
			else return visitor.visitChildren(this);
		}
	}

	public final ForBodyContext forBody() throws RecognitionException {
		ForBodyContext _localctx = new ForBodyContext(_ctx, getState());
		enterRule(_localctx, 20, RULE_forBody);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(139);
			match(T__7);
			setState(144);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,13,_ctx) ) {
			case 1:
				{
				setState(140);
				forCondition();
				}
				break;
			case 2:
				{
				setState(141);
				forCondition();
				setState(142);
				forCondition();
				}
				break;
			}
			setState(146);
			match(T__2);
			setState(147);
			match(NEWLINE);
			setState(148);
			match(T__3);
			setState(149);
			match(NEWLINE);
			setState(156);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__1:
			case T__5:
			case T__6:
			case T__7:
			case T__8:
			case T__10:
			case INT:
			case WORD:
			case FLOAT:
			case NEWLINE:
				{
				setState(151); 
				_errHandler.sync(this);
				_la = _input.LA(1);
				do {
					{
					{
					setState(150);
					line();
					}
					}
					setState(153); 
					_errHandler.sync(this);
					_la = _input.LA(1);
				} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__1) | (1L << T__5) | (1L << T__6) | (1L << T__7) | (1L << T__8) | (1L << T__10) | (1L << INT) | (1L << WORD) | (1L << FLOAT) | (1L << NEWLINE))) != 0) );
				}
				break;
			case T__4:
				{
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			setState(158);
			match(T__4);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class MethodContext extends ParserRuleContext {
		public TerminalNode WORD() { return getToken(gParser.WORD, 0); }
		public TerminalNode ENDL() { return getToken(gParser.ENDL, 0); }
		public List<TerminalNode> NEWLINE() { return getTokens(gParser.NEWLINE); }
		public TerminalNode NEWLINE(int i) {
			return getToken(gParser.NEWLINE, i);
		}
		public List<LineContext> line() {
			return getRuleContexts(LineContext.class);
		}
		public LineContext line(int i) {
			return getRuleContext(LineContext.class,i);
		}
		public MethodContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_method; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).enterMethod(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).exitMethod(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof gVisitor ) return ((gVisitor<? extends T>)visitor).visitMethod(this);
			else return visitor.visitChildren(this);
		}
	}

	public final MethodContext method() throws RecognitionException {
		MethodContext _localctx = new MethodContext(_ctx, getState());
		enterRule(_localctx, 22, RULE_method);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(160);
			match(T__8);
			setState(161);
			match(WORD);
			setState(162);
			match(ENDL);
			setState(163);
			match(NEWLINE);
			setState(164);
			match(T__3);
			setState(165);
			match(NEWLINE);
			setState(172);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__1:
			case T__5:
			case T__6:
			case T__7:
			case T__8:
			case T__10:
			case INT:
			case WORD:
			case FLOAT:
			case NEWLINE:
				{
				setState(167); 
				_errHandler.sync(this);
				_la = _input.LA(1);
				do {
					{
					{
					setState(166);
					line();
					}
					}
					setState(169); 
					_errHandler.sync(this);
					_la = _input.LA(1);
				} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__1) | (1L << T__5) | (1L << T__6) | (1L << T__7) | (1L << T__8) | (1L << T__10) | (1L << INT) | (1L << WORD) | (1L << FLOAT) | (1L << NEWLINE))) != 0) );
				}
				break;
			case T__4:
				{
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			setState(174);
			match(T__4);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class MethodCallContext extends ParserRuleContext {
		public TerminalNode WORD() { return getToken(gParser.WORD, 0); }
		public TerminalNode ENDL() { return getToken(gParser.ENDL, 0); }
		public MethodCallContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_methodCall; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).enterMethodCall(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).exitMethodCall(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof gVisitor ) return ((gVisitor<? extends T>)visitor).visitMethodCall(this);
			else return visitor.visitChildren(this);
		}
	}

	public final MethodCallContext methodCall() throws RecognitionException {
		MethodCallContext _localctx = new MethodCallContext(_ctx, getState());
		enterRule(_localctx, 24, RULE_methodCall);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(176);
			match(WORD);
			setState(177);
			match(T__9);
			setState(178);
			match(ENDL);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class PrintToFContext extends ParserRuleContext {
		public List<TerminalNode> WORD() { return getTokens(gParser.WORD); }
		public TerminalNode WORD(int i) {
			return getToken(gParser.WORD, i);
		}
		public TerminalNode ENDL() { return getToken(gParser.ENDL, 0); }
		public PrintToFContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_printToF; }
		@Override
		public void enterRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).enterPrintToF(this);
		}
		@Override
		public void exitRule(ParseTreeListener listener) {
			if ( listener instanceof gListener ) ((gListener)listener).exitPrintToF(this);
		}
		@Override
		public <T> T accept(ParseTreeVisitor<? extends T> visitor) {
			if ( visitor instanceof gVisitor ) return ((gVisitor<? extends T>)visitor).visitPrintToF(this);
			else return visitor.visitChildren(this);
		}
	}

	public final PrintToFContext printToF() throws RecognitionException {
		PrintToFContext _localctx = new PrintToFContext(_ctx, getState());
		enterRule(_localctx, 26, RULE_printToF);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(180);
			match(T__10);
			setState(181);
			match(WORD);
			setState(182);
			match(T__11);
			setState(183);
			match(WORD);
			setState(184);
			match(T__2);
			setState(185);
			match(ENDL);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3\32\u00be\4\2\t\2"+
		"\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13"+
		"\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\3\2\6\2 \n\2\r\2\16\2!\3\2\3"+
		"\2\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\3\5\3/\n\3\3\3\5\3\62\n\3\3\4\3\4"+
		"\3\4\3\4\3\5\3\5\3\5\3\5\3\6\3\6\3\6\3\6\3\6\3\6\3\7\3\7\3\7\3\7\3\7\3"+
		"\7\3\7\3\7\3\7\3\7\3\7\5\7M\n\7\3\7\3\7\3\b\3\b\3\b\3\b\5\bU\n\b\3\b\3"+
		"\b\3\b\3\b\3\b\6\b\\\n\b\r\b\16\b]\3\b\5\ba\n\b\3\b\3\b\3\t\3\t\3\t\5"+
		"\th\n\t\3\t\3\t\3\t\3\t\3\t\6\to\n\t\r\t\16\tp\3\t\5\tt\n\t\3\t\3\t\3"+
		"\n\3\n\6\nz\n\n\r\n\16\n{\3\n\3\n\3\n\5\n\u0081\n\n\3\n\3\n\3\n\3\13\3"+
		"\13\3\13\5\13\u0089\n\13\3\13\3\13\3\13\3\f\3\f\3\f\3\f\3\f\5\f\u0093"+
		"\n\f\3\f\3\f\3\f\3\f\3\f\6\f\u009a\n\f\r\f\16\f\u009b\3\f\5\f\u009f\n"+
		"\f\3\f\3\f\3\r\3\r\3\r\3\r\3\r\3\r\3\r\6\r\u00aa\n\r\r\r\16\r\u00ab\3"+
		"\r\5\r\u00af\n\r\3\r\3\r\3\16\3\16\3\16\3\16\3\17\3\17\3\17\3\17\3\17"+
		"\3\17\3\17\3\17\2\2\20\2\4\6\b\n\f\16\20\22\24\26\30\32\34\2\3\4\2\20"+
		"\21\23\23\2\u00d2\2\37\3\2\2\2\4\61\3\2\2\2\6\63\3\2\2\2\b\67\3\2\2\2"+
		"\n;\3\2\2\2\fA\3\2\2\2\16P\3\2\2\2\20d\3\2\2\2\22w\3\2\2\2\24\u0085\3"+
		"\2\2\2\26\u008d\3\2\2\2\30\u00a2\3\2\2\2\32\u00b2\3\2\2\2\34\u00b6\3\2"+
		"\2\2\36 \5\4\3\2\37\36\3\2\2\2 !\3\2\2\2!\37\3\2\2\2!\"\3\2\2\2\"#\3\2"+
		"\2\2#$\7\2\2\3$\3\3\2\2\2%/\5\f\7\2&/\5\16\b\2\'/\5\20\t\2(/\5\22\n\2"+
		")/\5\26\f\2*/\5\n\6\2+/\5\30\r\2,/\5\32\16\2-/\5\34\17\2.%\3\2\2\2.&\3"+
		"\2\2\2.\'\3\2\2\2.(\3\2\2\2.)\3\2\2\2.*\3\2\2\2.+\3\2\2\2.,\3\2\2\2.-"+
		"\3\2\2\2/\62\3\2\2\2\60\62\7\31\2\2\61.\3\2\2\2\61\60\3\2\2\2\62\5\3\2"+
		"\2\2\63\64\t\2\2\2\64\65\7\24\2\2\65\66\t\2\2\2\66\7\3\2\2\2\678\t\2\2"+
		"\289\7\25\2\29:\t\2\2\2:\t\3\2\2\2;<\t\2\2\2<=\7\25\2\2=>\t\2\2\2>?\7"+
		"\25\2\2?@\t\2\2\2@\13\3\2\2\2AB\7\21\2\2BL\7\3\2\2CM\7\21\2\2DM\7\20\2"+
		"\2EM\5\6\4\2FM\7\23\2\2GM\7\32\2\2HM\5\b\5\2IM\5\n\6\2JM\7\17\2\2KM\5"+
		"\32\16\2LC\3\2\2\2LD\3\2\2\2LE\3\2\2\2LF\3\2\2\2LG\3\2\2\2LH\3\2\2\2L"+
		"I\3\2\2\2LJ\3\2\2\2LK\3\2\2\2MN\3\2\2\2NO\7\30\2\2O\r\3\2\2\2PT\7\4\2"+
		"\2QU\5\b\5\2RU\5\n\6\2SU\3\2\2\2TQ\3\2\2\2TR\3\2\2\2TS\3\2\2\2UV\3\2\2"+
		"\2VW\7\5\2\2WX\7\31\2\2XY\7\6\2\2Y`\7\31\2\2Z\\\5\4\3\2[Z\3\2\2\2\\]\3"+
		"\2\2\2][\3\2\2\2]^\3\2\2\2^a\3\2\2\2_a\3\2\2\2`[\3\2\2\2`_\3\2\2\2ab\3"+
		"\2\2\2bc\7\7\2\2c\17\3\2\2\2dg\7\b\2\2eh\5\b\5\2fh\5\n\6\2ge\3\2\2\2g"+
		"f\3\2\2\2hi\3\2\2\2ij\7\5\2\2jk\7\31\2\2kl\7\6\2\2ls\7\31\2\2mo\5\4\3"+
		"\2nm\3\2\2\2op\3\2\2\2pn\3\2\2\2pq\3\2\2\2qt\3\2\2\2rt\3\2\2\2sn\3\2\2"+
		"\2sr\3\2\2\2tu\3\2\2\2uv\7\7\2\2v\21\3\2\2\2w\u0080\7\t\2\2xz\7\32\2\2"+
		"yx\3\2\2\2z{\3\2\2\2{y\3\2\2\2{|\3\2\2\2|\u0081\3\2\2\2}\u0081\7\21\2"+
		"\2~\u0081\7\23\2\2\177\u0081\5\32\16\2\u0080y\3\2\2\2\u0080}\3\2\2\2\u0080"+
		"~\3\2\2\2\u0080\177\3\2\2\2\u0081\u0082\3\2\2\2\u0082\u0083\7\5\2\2\u0083"+
		"\u0084\7\30\2\2\u0084\23\3\2\2\2\u0085\u0088\5\f\7\2\u0086\u0089\5\b\5"+
		"\2\u0087\u0089\5\n\6\2\u0088\u0086\3\2\2\2\u0088\u0087\3\2\2\2\u0089\u008a"+
		"\3\2\2\2\u008a\u008b\7\30\2\2\u008b\u008c\5\f\7\2\u008c\25\3\2\2\2\u008d"+
		"\u0092\7\n\2\2\u008e\u0093\5\24\13\2\u008f\u0090\5\24\13\2\u0090\u0091"+
		"\5\24\13\2\u0091\u0093\3\2\2\2\u0092\u008e\3\2\2\2\u0092\u008f\3\2\2\2"+
		"\u0093\u0094\3\2\2\2\u0094\u0095\7\5\2\2\u0095\u0096\7\31\2\2\u0096\u0097"+
		"\7\6\2\2\u0097\u009e\7\31\2\2\u0098\u009a\5\4\3\2\u0099\u0098\3\2\2\2"+
		"\u009a\u009b\3\2\2\2\u009b\u0099\3\2\2\2\u009b\u009c\3\2\2\2\u009c\u009f"+
		"\3\2\2\2\u009d\u009f\3\2\2\2\u009e\u0099\3\2\2\2\u009e\u009d\3\2\2\2\u009f"+
		"\u00a0\3\2\2\2\u00a0\u00a1\7\7\2\2\u00a1\27\3\2\2\2\u00a2\u00a3\7\13\2"+
		"\2\u00a3\u00a4\7\21\2\2\u00a4\u00a5\7\30\2\2\u00a5\u00a6\7\31\2\2\u00a6"+
		"\u00a7\7\6\2\2\u00a7\u00ae\7\31\2\2\u00a8\u00aa\5\4\3\2\u00a9\u00a8\3"+
		"\2\2\2\u00aa\u00ab\3\2\2\2\u00ab\u00a9\3\2\2\2\u00ab\u00ac\3\2\2\2\u00ac"+
		"\u00af\3\2\2\2\u00ad\u00af\3\2\2\2\u00ae\u00a9\3\2\2\2\u00ae\u00ad\3\2"+
		"\2\2\u00af\u00b0\3\2\2\2\u00b0\u00b1\7\7\2\2\u00b1\31\3\2\2\2\u00b2\u00b3"+
		"\7\21\2\2\u00b3\u00b4\7\f\2\2\u00b4\u00b5\7\30\2\2\u00b5\33\3\2\2\2\u00b6"+
		"\u00b7\7\r\2\2\u00b7\u00b8\7\21\2\2\u00b8\u00b9\7\16\2\2\u00b9\u00ba\7"+
		"\21\2\2\u00ba\u00bb\7\5\2\2\u00bb\u00bc\7\30\2\2\u00bc\35\3\2\2\2\24!"+
		".\61LT]`gps{\u0080\u0088\u0092\u009b\u009e\u00ab\u00ae";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}