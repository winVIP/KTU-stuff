import org.antlr.v4.runtime.misc.Pair;
import org.antlr.v4.runtime.tree.TerminalNode;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;
import java.nio.file.Files;
import java.nio.file.StandardOpenOption;
import java.util.*;

public class MyVisitor extends gBaseVisitor<Value>{
    private Map<String, Value> variables = new HashMap<>();
    private Map<String, Method> methods = new HashMap<>();

    @Override
    public Value visitAssign(gParser.AssignContext ctx) {
        List<TerminalNode> words = ctx.WORD();
        if(words.size() > 1){
            variables.put(words.get(0).getText(), variables.get(words.get(1).getText()));
        }
        else if(ctx.INT() != null){
            variables.put(words.get(0).getText(), new Value(Integer.valueOf(ctx.INT().getText())));
        }
        else if(ctx.FLOAT() != null){
            variables.put(words.get(0).getText(), new Value(Float.valueOf(ctx.FLOAT().getText())));
        }
        else if(ctx.TEXT() != null){
            variables.put(words.get(0).getText(), new Value(ctx.TEXT().getText()));
        }
        else if(ctx.BOOL() != null){
            variables.put(words.get(0).getText(), new Value(Boolean.valueOf(ctx.BOOL().getText())));
        }
        else if(ctx.operation() != null){
            variables.put(words.get(0).getText(), this.visit(ctx.operation()));
        }
        else if(ctx.condition() != null){
            variables.put(words.get(0).getText(), this.visit(ctx.condition()));
        }
        else if(ctx.intervalC() != null){
            variables.put(words.get(0).getText(), this.visit(ctx.intervalC()));
        }
        return variables.get(words.get(0).getText());
    }

    @Override
    public Value visitPrintToC(gParser.PrintToCContext ctx) {
        if(ctx.WORD() != null){
            System.out.println(variables.get(ctx.WORD().getText()).value);
        }
        else if(ctx.TEXT() != null){
            String string = "";
            for (TerminalNode letter: ctx.TEXT()) {
                string = string + letter.getText();
            }
            System.out.println(string);
        }
        return null;
    }

    @Override
    public Value visitIfas(gParser.IfasContext ctx) {
        boolean condition = false;
        if(ctx.condition() != null){
            if(Boolean.valueOf(this.visit(ctx.condition()).value.toString())){
                condition = true;
            }
        }
        else if(ctx.intervalC() != null){
            if (Boolean.valueOf(this.visit(ctx.intervalC()).value.toString())){
                condition = true;
            }
        }

        if(condition){
            List<gParser.LineContext> lines = ctx.line();
            for (gParser.LineContext line: lines) {
                this.visit(line);
            }
        }

        return null;
    }

    @Override
    public Value visitForBody(gParser.ForBodyContext ctx) {
        List<gParser.ForConditionContext> forConditionContexts = ctx.forCondition();
        //single for
        if(forConditionContexts.size() == 1){
            this.visit(forConditionContexts.get(0).assign(0));
            if(forConditionContexts.get(0).condition() != null){
                while (Boolean.valueOf(this.visit(forConditionContexts.get(0).condition()).value.toString())){
                    //System.out.println(Boolean.valueOf(this.visit(forConditionContexts.get(0).condition()).value.toString()));
                    List<gParser.LineContext> lines = ctx.line();
                    for (gParser.LineContext line: lines) {
                        this.visit(line);
                    }
                    this.visit(forConditionContexts.get(0).assign(1));
                }
                //System.out.println(Boolean.valueOf(this.visit(forConditionContexts.get(0).condition()).value.toString()));
            }
            else if(forConditionContexts.get(0).intervalC() != null){
                while (Boolean.valueOf(this.visit(forConditionContexts.get(0).intervalC()).value.toString())){
                    List<gParser.LineContext> lines = ctx.line();
                    for (gParser.LineContext line: lines) {
                        this.visit(line);
                    }
                    this.visit(forConditionContexts.get(0).assign(1));
                }
            }
        }
        //double for
        else if(forConditionContexts.size() == 2){
            this.visit(forConditionContexts.get(0).assign(0));

            if(forConditionContexts.get(0).condition() != null){
                while (Boolean.valueOf(this.visit(forConditionContexts.get(0).condition()).value.toString())){
                    this.visit(forConditionContexts.get(1).assign(0));
                    while (Boolean.valueOf(this.visit(forConditionContexts.get(1).condition()).value.toString())){
                        List<gParser.LineContext> lines = ctx.line();
                        for (gParser.LineContext line: lines) {
                            this.visit(line);
                        }
                        this.visit(forConditionContexts.get(1).assign(1));
                    }
                    this.visit(forConditionContexts.get(0).assign(1));
                }
            }

            else if(forConditionContexts.get(0).intervalC() != null){
                while (Boolean.valueOf(this.visit(forConditionContexts.get(0).intervalC()).value.toString())){
                    this.visit(forConditionContexts.get(1).assign(0));
                    while (Boolean.valueOf(this.visit(forConditionContexts.get(1).intervalC()).value.toString())){
                        List<gParser.LineContext> lines = ctx.line();
                        for (gParser.LineContext line: lines) {
                            this.visit(line);
                        }
                        this.visit(forConditionContexts.get(1).assign(1));
                    }
                    this.visit(forConditionContexts.get(0).assign(1));
                }
            }
        }
        return null;
    }

    @Override
    public Value visitMethodCall(gParser.MethodCallContext ctx) {
        if(methods.get(ctx.WORD().getText()) != null){
            List<gParser.LineContext> lines = methods.get(ctx.WORD().getText()).lctx;
            for (gParser.LineContext line: lines) {
                this.visit(line);
            }
        }
        return null;
    }

    @Override
    public Value visitMethod(gParser.MethodContext ctx) {
        if(methods.get(ctx.WORD().getText()) == null){
            methods.put(ctx.WORD().getText(), new Method(ctx.WORD().getText(), ctx, ctx.line()));
        }
        return null;
    }

    @Override
    public Value visitOperation(gParser.OperationContext ctx) {
        List<TerminalNode> ints = ctx.INT();
        List<TerminalNode> floats = ctx.FLOAT();
        List<TerminalNode> words = ctx.WORD();
        TerminalNode ops = ctx.OPERATORS();
        List<Pair<Integer, Object>> comparers = new ArrayList<>();
        for (TerminalNode i:ints) {
            comparers.add(new Pair<>(i.getSymbol().getTokenIndex(), Float.valueOf(i.getText())));
        }
        for (TerminalNode i:floats) {
            comparers.add(new Pair<>(i.getSymbol().getTokenIndex(), Float.valueOf(i.getText())));
        }
        for (TerminalNode i:words) {
            comparers.add(new Pair<>(i.getSymbol().getTokenIndex(), Float.valueOf(variables.get(i.getText()).value.toString())));

        }
        comparers.sort(new Comparator<Pair<Integer, Object>>() {
            @Override
            public int compare(Pair<Integer, Object> o1, Pair<Integer, Object> o2) {
                return o1.a.compareTo(o2.a);
            }
        });
        Float result = 0f;
        if(ops.getText().equals("+")){
            result = (Float) comparers.get(0).b + (Float)comparers.get(1).b;
        }
        else if(ops.getText().equals("*")){
            result = (Float) comparers.get(0).b * (Float)comparers.get(1).b;
        }
        else if(ops.getText().equals("-")){
            result = (Float) comparers.get(0).b - (Float)comparers.get(1).b;
        }
        else if(ops.getText().equals("/")){
            result = (Float) comparers.get(0).b / (Float)comparers.get(1).b;
        }

        return new Value(result);
    }

    String getType(String s){
        if(s.matches("^\\d+$")){
            return "int";
        }
        if(s.matches("[+-]?([0-9]*[.])?[0-9]+")){
            return "float";
        }
        return "boolean";
    }

    @Override
    public Value visitCondition(gParser.ConditionContext ctx) {
        List<TerminalNode> ints = ctx.INT();
        List<TerminalNode> floats = ctx.FLOAT();
        List<TerminalNode> words = ctx.WORD();
        TerminalNode lops = ctx.LOPS();
        List<Pair<Integer, Object>> comparers = new ArrayList<>();
        for (TerminalNode i:ints) {
            comparers.add(new Pair<>(i.getSymbol().getTokenIndex(), i.getText()));
        }
        for (TerminalNode i:floats) {
            comparers.add(new Pair<>(i.getSymbol().getTokenIndex(), i.getText()));
        }
        for (TerminalNode i:words) {
            comparers.add(new Pair<>(i.getSymbol().getTokenIndex(), variables.get(i.getText()).value));
        }
        comparers.sort(new Comparator<Pair<Integer, Object>>() {
            @Override
            public int compare(Pair<Integer, Object> o1, Pair<Integer, Object> o2) {
                return o1.a.compareTo(o2.a);
            }
        });

        String atype = getType(comparers.get(0).b.toString());
        String btype = getType(comparers.get(1).b.toString());

        if(lops.getText().equals("==")){
            return new Value(comparers.get(0).b.toString().equals(comparers.get(1).b.toString()));
        }
        else if(lops.getText().equals("<")){
            if(atype.equals("int") && btype.equals("int")){
                int a = Integer.parseInt(comparers.get(0).b.toString());
                int b = Integer.parseInt(comparers.get(1).b.toString());
                if( a < b){
                    return new Value(true);
                }
                else {
                    return new Value(false);
                }
            }
            else if(atype.equals("float") && btype.equals("float")){
                float a = Float.parseFloat(comparers.get(0).b.toString());
                float b = Float.parseFloat(comparers.get(1).b.toString());
                if( a < b){
                    return new Value(true);
                }
                else
                {
                    return new Value(false);
                }
            }
            else{
                throw new RuntimeException("bad types");
            }

        }
        else if(lops.getText().equals(">")){
            if(atype.equals("int") && btype.equals("int")){
                int a = Integer.parseInt(comparers.get(0).b.toString());
                int b = Integer.parseInt(comparers.get(1).b.toString());
                if( a > b){
                    return new Value(true);
                }
                else {
                    return new Value(false);
                }
            }
            else if(atype.equals("float") && btype.equals("float")){
                float a = Float.parseFloat(comparers.get(0).b.toString());
                float b = Float.parseFloat(comparers.get(1).b.toString());
                if( a > b){
                    return new Value(true);
                }
                else {
                    return new Value(false);
                }
            }
            else{
                throw new RuntimeException("bad types");
            }
        }
        else if(lops.getText().equals("<=")){
            if(atype.equals("int") && btype.equals("int")){
                int a = Integer.parseInt(comparers.get(0).b.toString());
                int b = Integer.parseInt(comparers.get(1).b.toString());
                if( a <= b){
                    return new Value(true);
                }
                else {
                    return new Value(false);
                }
            }
            else if(atype.equals("float") && btype.equals("float")){
                float a = Float.parseFloat(comparers.get(0).b.toString());
                float b = Float.parseFloat(comparers.get(1).b.toString());
                if( a <= b){
                    return new Value(true);
                }
                else {
                    return new Value(false);
                }
            }
            else{
                throw new RuntimeException("bad types");
            }

        }
        else if(lops.getText().equals(">=")){
            if(atype.equals("int") && btype.equals("int")){
                int a = Integer.parseInt(comparers.get(0).b.toString());
                int b = Integer.parseInt(comparers.get(1).b.toString());
                if( a >= b){
                    return new Value(true);
                }
                else {
                    return new Value(false);
                }
            }
            else if(atype.equals("float") && btype.equals("float")){
                float a = Float.parseFloat(comparers.get(0).b.toString());
                float b = Float.parseFloat(comparers.get(1).b.toString());
                if( a >= b){
                    return new Value(true);
                }
                else{
                    return new Value(false);
                }
            }
            else{
                throw new RuntimeException("bad types");
            }
        }
        else {
            if(atype.equals("int") && btype.equals("int")){
                int a = Integer.parseInt(comparers.get(0).b.toString());
                int b = Integer.parseInt(comparers.get(1).b.toString());
                if( a != b){
                    return new Value(true);
                }
                else {
                    return new Value(false);
                }
            }
            else if(atype.equals("float") && btype.equals("float")){
                float a = Float.parseFloat(comparers.get(0).b.toString());
                float b = Float.parseFloat(comparers.get(1).b.toString());
                if( a != b){
                    return new Value(true);
                }
                else {
                    return new Value(false);
                }
            }
            else{
                throw new RuntimeException("bad types");
            }
        }
    }

    @Override
    public Value visitIntervalC(gParser.IntervalCContext ctx) {
        List<TerminalNode> ints = ctx.INT();
        List<TerminalNode> floats = ctx.FLOAT();
        List<TerminalNode> words = ctx.WORD();
        List<TerminalNode> lops = ctx.LOPS();
        List<Pair<Integer, Object>> comparers = new ArrayList<>();
        for (TerminalNode i:ints) {
            comparers.add(new Pair<>(i.getSymbol().getTokenIndex(), i.getText()));
        }
        for (TerminalNode i:floats) {
            comparers.add(new Pair<>(i.getSymbol().getTokenIndex(), i.getText()));
        }
        for (TerminalNode i:words) {
            comparers.add(new Pair<>(i.getSymbol().getTokenIndex(), variables.get(i.getText()).value));
        }
        comparers.sort(new Comparator<Pair<Integer, Object>>() {
            @Override
            public int compare(Pair<Integer, Object> o1, Pair<Integer, Object> o2) {
                return o1.a.compareTo(o2.a);
            }
        });

        boolean result1 = false;
        if(lops.get(0).getText().equals("<")){
            if( comparers.get(0).b.toString().compareTo(comparers.get(1).b.toString()) < 0){
                result1 = true;
            }
            else {
                result1 = false;
            }
        }
        else if(lops.get(0).getText().equals(">")){
            if( comparers.get(0).b.toString().compareTo(comparers.get(1).b.toString()) > 0){
                result1 = true;
            }
            else {
                result1 = false;
            }
        }
        else if(lops.get(0).getText().equals("<=")){
            if( comparers.get(0).b.toString().compareTo(comparers.get(1).b.toString()) <= 0){
                result1 = true;
            }
            else {
                result1 = false;
            }
        }
        else if(lops.get(0).getText().equals(">=")){
            if( comparers.get(0).b.toString().compareTo(comparers.get(1).b.toString()) >= 0){
                result1 = true;
            }
            else {
                result1 = false;
            }
        }

        boolean result2 = false;
        if(lops.get(1).getText().equals("<")){
            if( comparers.get(1).b.toString().compareTo(comparers.get(2).b.toString()) < 0){
                result2 = true;
            }
            else {
                result2 = false;
            }
        }
        else if(lops.get(1).getText().equals(">")){
            if( comparers.get(1).b.toString().compareTo(comparers.get(2).b.toString()) > 0){
                result2 = true;
            }
            else {
                result2 = false;
            }
        }
        else if(lops.get(1).getText().equals("<=")){
            if( comparers.get(1).b.toString().compareTo(comparers.get(2).b.toString()) <= 0){
                result2 = true;
            }
            else {
                result2 = false;
            }
        }
        else if(lops.get(1).getText().equals(">=")){
            if( comparers.get(1).b.toString().compareTo(comparers.get(2).b.toString()) >= 0){
                result2 = true;
            }
            else {
                result2 = false;
            }
        }

        return new Value(result1 && result2);
    }

    @Override
    public Value visitPrintToF(gParser.PrintToFContext ctx) {
        String variable = ctx.WORD(0).toString();
        String text;
        if(variables.get(variable) != null){
            text = variables.get(variable).toString();
        }
        else
        {
            text = variable;
        }
        String fileName = ctx.WORD(1).toString();

        try {
            File file = new File(fileName);
            if (file.createNewFile()) {
                System.out.println("File created: " + file.getName());
            } else {
                System.out.println("File already exists.");
            }
        } catch (IOException e) {
            System.out.println("An error occurred.");
            e.printStackTrace();
        }

        try {
            FileWriter fileWriter = new FileWriter(fileName, true);
            PrintWriter printWriter = new PrintWriter(fileWriter);
            printWriter.println(text);
            printWriter.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
        return null;
    }
}
