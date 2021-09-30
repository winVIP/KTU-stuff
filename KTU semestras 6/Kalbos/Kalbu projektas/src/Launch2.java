import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.CharStreams;
import org.antlr.v4.runtime.CommonTokenStream;
import org.antlr.v4.runtime.tree.ParseTree;
import java.io.IOException;
import static org.antlr.v4.runtime.CharStreams.fromFileName;

public class Launch2 {
    public static void main(String[] args) {
        try {
            String source = "test.txt";
            CharStream cs = fromFileName(source);
            //System.out.println(cs.toString());
            gLexer lexer = new gLexer(cs);
            gParser parser = new gParser(new CommonTokenStream(lexer));
            ParseTree tree = parser.program();
            MyVisitor visitor = new MyVisitor();
            visitor.visit(tree);
        }
        catch(IOException e) {
            e.printStackTrace();
        }
    }
}
