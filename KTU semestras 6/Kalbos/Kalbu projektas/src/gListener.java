// Generated from C:/Users/vyten/Desktop/Kalbos/Kalbu projektas\g.g4 by ANTLR 4.8
import org.antlr.v4.runtime.tree.ParseTreeListener;

/**
 * This interface defines a complete listener for a parse tree produced by
 * {@link gParser}.
 */
public interface gListener extends ParseTreeListener {
	/**
	 * Enter a parse tree produced by {@link gParser#program}.
	 * @param ctx the parse tree
	 */
	void enterProgram(gParser.ProgramContext ctx);
	/**
	 * Exit a parse tree produced by {@link gParser#program}.
	 * @param ctx the parse tree
	 */
	void exitProgram(gParser.ProgramContext ctx);
	/**
	 * Enter a parse tree produced by {@link gParser#line}.
	 * @param ctx the parse tree
	 */
	void enterLine(gParser.LineContext ctx);
	/**
	 * Exit a parse tree produced by {@link gParser#line}.
	 * @param ctx the parse tree
	 */
	void exitLine(gParser.LineContext ctx);
	/**
	 * Enter a parse tree produced by {@link gParser#operation}.
	 * @param ctx the parse tree
	 */
	void enterOperation(gParser.OperationContext ctx);
	/**
	 * Exit a parse tree produced by {@link gParser#operation}.
	 * @param ctx the parse tree
	 */
	void exitOperation(gParser.OperationContext ctx);
	/**
	 * Enter a parse tree produced by {@link gParser#condition}.
	 * @param ctx the parse tree
	 */
	void enterCondition(gParser.ConditionContext ctx);
	/**
	 * Exit a parse tree produced by {@link gParser#condition}.
	 * @param ctx the parse tree
	 */
	void exitCondition(gParser.ConditionContext ctx);
	/**
	 * Enter a parse tree produced by {@link gParser#intervalC}.
	 * @param ctx the parse tree
	 */
	void enterIntervalC(gParser.IntervalCContext ctx);
	/**
	 * Exit a parse tree produced by {@link gParser#intervalC}.
	 * @param ctx the parse tree
	 */
	void exitIntervalC(gParser.IntervalCContext ctx);
	/**
	 * Enter a parse tree produced by {@link gParser#assign}.
	 * @param ctx the parse tree
	 */
	void enterAssign(gParser.AssignContext ctx);
	/**
	 * Exit a parse tree produced by {@link gParser#assign}.
	 * @param ctx the parse tree
	 */
	void exitAssign(gParser.AssignContext ctx);
	/**
	 * Enter a parse tree produced by {@link gParser#whileLoop}.
	 * @param ctx the parse tree
	 */
	void enterWhileLoop(gParser.WhileLoopContext ctx);
	/**
	 * Exit a parse tree produced by {@link gParser#whileLoop}.
	 * @param ctx the parse tree
	 */
	void exitWhileLoop(gParser.WhileLoopContext ctx);
	/**
	 * Enter a parse tree produced by {@link gParser#ifas}.
	 * @param ctx the parse tree
	 */
	void enterIfas(gParser.IfasContext ctx);
	/**
	 * Exit a parse tree produced by {@link gParser#ifas}.
	 * @param ctx the parse tree
	 */
	void exitIfas(gParser.IfasContext ctx);
	/**
	 * Enter a parse tree produced by {@link gParser#printToC}.
	 * @param ctx the parse tree
	 */
	void enterPrintToC(gParser.PrintToCContext ctx);
	/**
	 * Exit a parse tree produced by {@link gParser#printToC}.
	 * @param ctx the parse tree
	 */
	void exitPrintToC(gParser.PrintToCContext ctx);
	/**
	 * Enter a parse tree produced by {@link gParser#forCondition}.
	 * @param ctx the parse tree
	 */
	void enterForCondition(gParser.ForConditionContext ctx);
	/**
	 * Exit a parse tree produced by {@link gParser#forCondition}.
	 * @param ctx the parse tree
	 */
	void exitForCondition(gParser.ForConditionContext ctx);
	/**
	 * Enter a parse tree produced by {@link gParser#forBody}.
	 * @param ctx the parse tree
	 */
	void enterForBody(gParser.ForBodyContext ctx);
	/**
	 * Exit a parse tree produced by {@link gParser#forBody}.
	 * @param ctx the parse tree
	 */
	void exitForBody(gParser.ForBodyContext ctx);
	/**
	 * Enter a parse tree produced by {@link gParser#method}.
	 * @param ctx the parse tree
	 */
	void enterMethod(gParser.MethodContext ctx);
	/**
	 * Exit a parse tree produced by {@link gParser#method}.
	 * @param ctx the parse tree
	 */
	void exitMethod(gParser.MethodContext ctx);
	/**
	 * Enter a parse tree produced by {@link gParser#methodCall}.
	 * @param ctx the parse tree
	 */
	void enterMethodCall(gParser.MethodCallContext ctx);
	/**
	 * Exit a parse tree produced by {@link gParser#methodCall}.
	 * @param ctx the parse tree
	 */
	void exitMethodCall(gParser.MethodCallContext ctx);
	/**
	 * Enter a parse tree produced by {@link gParser#printToF}.
	 * @param ctx the parse tree
	 */
	void enterPrintToF(gParser.PrintToFContext ctx);
	/**
	 * Exit a parse tree produced by {@link gParser#printToF}.
	 * @param ctx the parse tree
	 */
	void exitPrintToF(gParser.PrintToFContext ctx);
}