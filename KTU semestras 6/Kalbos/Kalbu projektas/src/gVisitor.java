// Generated from C:/Users/vyten/Desktop/Kalbos/Kalbu projektas\g.g4 by ANTLR 4.8
import org.antlr.v4.runtime.tree.ParseTreeVisitor;

/**
 * This interface defines a complete generic visitor for a parse tree produced
 * by {@link gParser}.
 *
 * @param <T> The return type of the visit operation. Use {@link Void} for
 * operations with no return type.
 */
public interface gVisitor<T> extends ParseTreeVisitor<T> {
	/**
	 * Visit a parse tree produced by {@link gParser#program}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitProgram(gParser.ProgramContext ctx);
	/**
	 * Visit a parse tree produced by {@link gParser#line}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitLine(gParser.LineContext ctx);
	/**
	 * Visit a parse tree produced by {@link gParser#operation}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitOperation(gParser.OperationContext ctx);
	/**
	 * Visit a parse tree produced by {@link gParser#condition}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitCondition(gParser.ConditionContext ctx);
	/**
	 * Visit a parse tree produced by {@link gParser#intervalC}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitIntervalC(gParser.IntervalCContext ctx);
	/**
	 * Visit a parse tree produced by {@link gParser#assign}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitAssign(gParser.AssignContext ctx);
	/**
	 * Visit a parse tree produced by {@link gParser#whileLoop}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitWhileLoop(gParser.WhileLoopContext ctx);
	/**
	 * Visit a parse tree produced by {@link gParser#ifas}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitIfas(gParser.IfasContext ctx);
	/**
	 * Visit a parse tree produced by {@link gParser#printToC}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitPrintToC(gParser.PrintToCContext ctx);
	/**
	 * Visit a parse tree produced by {@link gParser#forCondition}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitForCondition(gParser.ForConditionContext ctx);
	/**
	 * Visit a parse tree produced by {@link gParser#forBody}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitForBody(gParser.ForBodyContext ctx);
	/**
	 * Visit a parse tree produced by {@link gParser#method}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitMethod(gParser.MethodContext ctx);
	/**
	 * Visit a parse tree produced by {@link gParser#methodCall}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitMethodCall(gParser.MethodCallContext ctx);
	/**
	 * Visit a parse tree produced by {@link gParser#printToF}.
	 * @param ctx the parse tree
	 * @return the visitor result
	 */
	T visitPrintToF(gParser.PrintToFContext ctx);
}