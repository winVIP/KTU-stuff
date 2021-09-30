import java.util.List;

public class Method {
    String name = null;
    gParser.MethodContext ctx;
    List<gParser.LineContext> lctx;

    public Method(String name, gParser.MethodContext ctx, List<gParser.LineContext> lctx){
        this.name = name;
        this.ctx = ctx;
        this.lctx = lctx;
    }
}
